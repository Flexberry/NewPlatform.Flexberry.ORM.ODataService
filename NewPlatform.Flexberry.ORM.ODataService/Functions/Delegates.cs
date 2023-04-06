namespace NewPlatform.Flexberry.ORM.ODataService.Functions
{
    using System.Collections.Generic;

    /// <summary>
    /// Тип делегата пользовательской функции.
    /// </summary>
    /// <param name="queryParameters">Параметры запроса OData.</param>
    /// <param name="parameters">Аргументы функции.</param>
    /// <returns>Результат выполнения пользовательской функции.</returns>
    public delegate object DelegateODataFunction(QueryParameters queryParameters, IDictionary<string, object> parameters);

    /// <summary>
    /// Тип делегата пользовательскоuj экшена, который ничего не возвращает.
    /// </summary>
    /// <param name="queryParameters">Параметры запроса OData.</param>
    /// <param name="parameters">Аргументы экшена.</param>
    public delegate void DelegateODataNoReplyFunction(QueryParameters queryParameters, IDictionary<string, object> parameters);
}
