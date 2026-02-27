namespace NewPlatform.Flexberry.ORM.ODataServiceCore.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ICSSoft.STORMNET;

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
                return string.Empty;

            var rootNode = new ExpandNode();
            BuildExpandTreeForObject(dataObject, rootNode, getEdmPropertyName);

            string expandQuery = BuildExpandQuery(rootNode);
            return !string.IsNullOrEmpty(expandQuery) ? "$expand=" + expandQuery : string.Empty;
        }

        /// <summary>
        /// Рекурсивно строит дерево expand для объекта и его загруженных мастеров.
        /// </summary>
        private static void BuildExpandTreeForObject(
            DataObject dataObject,
            ExpandNode parentNode,
            Func<Type, string, string> getEdmPropertyName)
        {
            if (dataObject == null)
                return;

            string[] loadedProps = dataObject.GetLoadedProperties();
            if (loadedProps == null || loadedProps.Length == 0)
                return;

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
                        continue;

                    // Получаем EDM имя
                    string edmName = getEdmPropertyName(objectType, propName);
                    if (string.IsNullOrEmpty(edmName))
                        continue;

                    // Ищем или создаем узел
                    var node = parentNode.Children.FirstOrDefault(n => n.EdmName == edmName);
                    if (node == null)
                    {
                        node = new ExpandNode { EdmName = edmName, PropertyType = propType };
                        parentNode.Children.Add(node);
                    }

                    // Рекурсивно обрабатываем вложенный мастер
                    object propValue = Information.GetPropValueByName(dataObject, propName);
                    if (propValue is DataObject nestedMaster)
                    {
                        BuildExpandTreeForObject(nestedMaster, node, getEdmPropertyName);
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
                return string.Empty;

            var parts = new List<string>();
            foreach (var child in node.Children)
            {
                string nestedExpand = BuildExpandQuery(child);
                if (!string.IsNullOrEmpty(nestedExpand))
                    parts.Add($"{child.EdmName}($expand={nestedExpand})");
                else
                    parts.Add(child.EdmName);
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
