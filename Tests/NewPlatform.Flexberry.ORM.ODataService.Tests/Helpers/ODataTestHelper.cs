namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers
{
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.KeyGen;
    using NewPlatform.Flexberry.ORM.ODataService.Model;

    /// <summary>
    /// Вспомогательные утилиты для тестирования OData.
    /// </summary>
    public static class ODataTestHelper
    {
        /// <summary>
        /// Добавить запись об изменении ссылки у объекта в тело запроса к OData.
        /// </summary>
        /// <param name="requestJsonData">Исходное тело запроса.</param>
        /// <param name="view">Представление исходного объекта.</param>
        /// <param name="model">EDM модель.</param>
        /// <param name="dataObject">Новый объект данных (по ссылке).</param>
        /// <param name="relationName">Ссылка на новый объект данных.</param>
        /// <returns>Новое тело запроса к OData.</returns>
        public static string AddEntryRelationship(string requestJsonData, View view, DataObjectEdmModel model, DataObject dataObject, string relationName)
        {
            DataObjectDictionary objJsonМедв = DataObjectDictionary.Parse(requestJsonData, view, model);

            objJsonМедв.Add(
                $"{relationName}@odata.bind",
                string.Format(
                    "{0}({1})",
                    model.GetEdmEntitySet(dataObject.GetType()).Name,
                    ((KeyGuid)dataObject.__PrimaryKey).Guid.ToString("D")));

            var result = objJsonМедв.Serialize();
            return result;
        }

        /// <summary>
        /// Получить URL для запроса к OData.
        /// </summary>
        /// <param name="model">EDM модель.</param>
        /// <param name="dataObject">Объект (по которому выполняется запрос).</param>
        /// <returns>URL запроса к OData.</returns>
        public static string GetRequestUrl(DataObjectEdmModel model, DataObject dataObject)
            => string.Format("http://localhost/odata/{0}({1})", model.GetEdmEntitySet(dataObject.GetType()).Name, ((KeyGuid)dataObject.__PrimaryKey).Guid.ToString());
    }
}
