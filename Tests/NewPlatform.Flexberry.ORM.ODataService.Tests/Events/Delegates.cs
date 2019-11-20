namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Events
{
    using System;
    using System.Net;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;

    using NewPlatform.Flexberry.ORM.ODataService.Controllers;

    /// <summary>
    /// Тип делегата, вызываемого перед выполнением запроса.
    /// </summary>
    /// <param name="lcs">.</param>
    /// <returns></returns>
    public delegate bool DelegateBeforeGet(DataObjectController controller, ref LoadingCustomizationStruct lcs);

    /// <summary>
    /// Тип делегата, вызываемого перед созданием объекта.
    /// </summary>
    /// <param name="obj">Объект.</param>
    /// <returns></returns>
    public delegate bool DelegateBeforeCreate(DataObjectController controller, DataObject obj);

    /// <summary>
    /// Тип делегата, вызываемого перед изменением объекта.
    /// </summary>
    /// <param name="obj">Объект.</param>
    /// <returns></returns>
    public delegate bool DelegateBeforeUpdate(DataObjectController controller, DataObject obj);

    /// <summary>
    /// Тип делегата, вызываемого перед удалением объекта.
    /// </summary>
    /// <param name="obj">Объект.</param>
    /// <returns></returns>
    public delegate bool DelegateBeforeDelete(DataObjectController controller, DataObject obj);

    /// <summary>
    /// Тип делегата, вызываемого после вычитывания объектов.
    /// </summary>
    /// <param name="objs">Вычитанные объекты.</param>
    public delegate void DelegateAfterGet(DataObjectController controller, ref DataObject[] objs);

    /// <summary>
    /// Тип делегата, вызываемого после создания объекта.
    /// </summary>
    /// <param name="obj">Объект после создания.</param>
    public delegate void DelegateAfterCreate(DataObjectController controller, DataObject obj);

    /// <summary>
    /// Тип делегата, вызываемого после обновления объекта.
    /// </summary>
    /// <param name="obj">Объект после обновления.</param>
    public delegate void DelegateAfterUpdate(DataObjectController controller, DataObject obj);

    /// <summary>
    /// Тип делегата, вызываемого после удаления объекта.
    /// </summary>
    /// <param name="obj">Объект перед удалением.</param>
    public delegate void DelegateAfterDelete(DataObjectController controller, DataObject obj);

    /// <summary>
    /// Тип делегата, вызываемого после возникновения исключения.
    /// </summary>
    /// <param name="ex">Исключение, которое возникло внутри ODataService.</param>
    /// <param name="code">Возвращаемый код HTTP. По-умолчанияю 500.</param>
    /// <returns>Исключение, которое будет отправлено клиенту.</returns>
    public delegate Exception DelegateAfterInternalServerError(DataObjectController controller, Exception ex, ref HttpStatusCode code);

}
