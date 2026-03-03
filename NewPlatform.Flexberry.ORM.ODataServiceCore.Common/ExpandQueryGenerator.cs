namespace NewPlatform.Flexberry.ORM.ODataServiceCore.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ICSSoft.STORMNET;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Extensions;

    /// <summary>
    /// Класс для автоматического разворачивания мастеров в OData ответах.
    /// </summary>
    public static class ExpandQueryGenerator
    {
        /// <summary>
        /// Строит OData $expand запрос из загруженных свойств-мастеров DataObject.
        /// Поддерживает вложенные мастера (master inside a master).
        /// </summary>
        /// <param name="dataObject">DataObject для анализа.</param>
        /// <param name="getEdmPropertyName">Функция для конвертации имени свойства в EDM имя.</param>
        /// <returns>Строка вида "$expand=Master1,Master2($expand=NestedMaster)" или пустая строка, если мастера не загружены.</returns>
        public static string GetQueryByLoadedProps(
            DataObject dataObject,
            Func<Type, string, string> getEdmPropertyName)
        {
            if (dataObject == null || getEdmPropertyName == null)
            {
                return string.Empty;
            }

            ExpandNode rootNode = new ExpandNode();
            HashSet<TypeKeyTuple> processedDataObjects = new HashSet<TypeKeyTuple>();
            BuildExpandTreeForObject(dataObject, rootNode, getEdmPropertyName, processedDataObjects);

            string expandQuery = BuildExpandQuery(rootNode);
            if (string.IsNullOrEmpty(expandQuery))
            {
                return string.Empty;
            }

            return "$expand=" + expandQuery;
        }

        /// <summary>
        /// Рекурсивно строит дерево expand для объекта и его загруженных мастеров.
        /// </summary>
        private static void BuildExpandTreeForObject(
            DataObject dataObject,
            ExpandNode parentNode,
            Func<Type, string, string> getEdmPropertyName,
            HashSet<TypeKeyTuple> processedDataObjects)
        {
            if (dataObject == null)
            {
                return;
            }

            // Защита от циклических ссылок: проверяем, не обрабатывали ли уже этот объект
            TypeKeyTuple dataForHash = new TypeKeyTuple(dataObject.GetType(), dataObject.__PrimaryKey);
            if (!processedDataObjects.Add(dataForHash))
            {
                return; // Найдена ссылка в цепочке объектов на ранее отсмотренный. Чтобы предотвратить рекурсию, далее не нужно загружать.
            }

            string[] loadedProps = dataObject.GetLoadedProperties();
            if (loadedProps == null || loadedProps.Length == 0)
            {
                return;
            }

            Type objectType = dataObject.GetType();

            foreach (string propName in loadedProps)
            {
                try
                {
                    // Проверяем, является ли свойство мастером
                    Type propType = Information.GetPropertyType(objectType, propName);
                    if (propType == null ||
                        !propType.IsSubclassOf(typeof(DataObject)) ||
                        propType.IsSubclassOf(typeof(DetailArray)))
                    {
                        continue;
                    }

                    // Получаем значение свойства и проверяем на циклическую ссылку
                    object propValue = Information.GetPropValueByName(dataObject, propName);
                    if (propValue is DataObject nestedMaster)
                    {
                        // Пропускаем циклические ссылки: если объект уже обрабатывался, не добавляем его
                        TypeKeyTuple nestedDataForHash = new TypeKeyTuple(nestedMaster.GetType(), nestedMaster.__PrimaryKey);
                        if (processedDataObjects.Contains(nestedDataForHash))
                        {
                            continue;
                        }
                    }

                    // Получаем EDM имя
                    string edmName = getEdmPropertyName(objectType, propName);
                    if (string.IsNullOrEmpty(edmName))
                    {
                        continue;
                    }

                    // Ищем или создаем узел
                    ExpandNode node = parentNode.Children.FirstOrDefault(n => n.EdmName == edmName);
                    if (node == null)
                    {
                        node = new ExpandNode { EdmName = edmName, PropertyType = propType };
                        parentNode.Children.Add(node);
                    }

                    // Рекурсивно обрабатываем вложенный мастер
                    if (propValue is DataObject master)
                    {
                        BuildExpandTreeForObject(master, node, getEdmPropertyName, processedDataObjects);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Строит строку $expand из дерева узлов.
        /// </summary>
        private static string BuildExpandQuery(ExpandNode node)
        {
            if (node.Children.Count == 0)
            {
                return string.Empty;
            }

            List<string> parts = new List<string>();
            foreach (ExpandNode child in node.Children)
            {
                string nestedExpand = BuildExpandQuery(child);
                if (!string.IsNullOrEmpty(nestedExpand))
                {
                    parts.Add($"{child.EdmName}($expand={nestedExpand})");
                }
                else
                {
                    parts.Add(child.EdmName);
                }
            }

            return string.Join(",", parts);
        }

        /// <summary>
        /// Узел дерева для построения $expand запроса.
        /// </summary>
        private class ExpandNode
        {
            public string EdmName { get; set; }
            public Type PropertyType { get; set; }
            public List<ExpandNode> Children { get; } = new List<ExpandNode>();
        }
    }
}
