namespace NewPlatform.Flexberry.ORM.ODataServiceCore.Common
{
    using System;
    using System.Collections.Generic;
    using ICSSoft.STORMNET;

    /// <summary>
    /// Класс для автоматического разворачивания мастеров в OData ответах.
    /// </summary>
    public static class ExpandQueryGenerator
    {
        /// <summary>
        /// Строит OData $expand запрос из загруженных свойств-мастеров DataObject.
        /// </summary>
        /// <param name="dataObject">DataObject для анализа.</param>
        /// <param name="getEdmPropertyName">Функция для конвертации имени свойства в EDM имя.</param>
        /// <returns>Строка вида "$expand=Master1,Master2" или пустая строка, если мастера не загружены.</returns>
        public static string GetQueryByLoadedProps(
            DataObject dataObject,
            Func<Type, string, string> getEdmPropertyName)
        {
            if (dataObject == null || getEdmPropertyName == null)
                return string.Empty;

            string[] loadedProps = dataObject.GetLoadedProperties();
            if (loadedProps == null || loadedProps.Length == 0)
                return string.Empty;

            var masters = new List<string>();
            Type objectType = dataObject.GetType();

            foreach (string propName in loadedProps)
            {
                try
                {
                    Type propType = Information.GetPropertyType(objectType, propName);
                    if (propType != null &&
                        propType.IsSubclassOf(typeof(DataObject)) &&
                        !propType.IsSubclassOf(typeof(DetailArray)))
                    {
                        string edmName = getEdmPropertyName(objectType, propName);
                        if (!string.IsNullOrEmpty(edmName))
                            masters.Add(edmName);
                    }
                }
                catch
                {
                    continue;
                }
            }

            return masters.Count > 0 ? "$expand=" + string.Join(",", masters) : string.Empty;
        }
    }
}
