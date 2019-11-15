namespace NewPlatform.Flexberry.ORM.ODataService.Events
{
    using System;
    using System.Net;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;

    using NewPlatform.Flexberry.ORM.ODataService.Controllers;

    /// <summary>
    /// Interface of container with OData Service event handlers.
    /// </summary>
    public interface IEventHandlerContainer
    {
        /// <summary>
        /// The OData Service token.
        /// </summary>
        ManagementToken Token { get; set; }

        /// <summary>
        /// Обработчик, вызываемый перед выполнением запроса.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="lcs">Структура загрузки.</param>
        /// <returns><see langword="true" /> для продолжения операции.</returns>
        bool BeforeGet(DataObjectController controller, ref LoadingCustomizationStruct lcs);

        /// <summary>
        /// Обработчик, вызываемый перед созданием объекта.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="obj">Объект.</param>
        /// <returns><see langword="true" /> для продолжения операции.</returns>
        bool BeforeCreate(DataObjectController controller, DataObject obj);

        /// <summary>
        /// Обработчик, вызываемый перед изменением объекта.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="obj">Объект.</param>
        /// <returns><see langword="true" /> для продолжения операции.</returns>
        bool BeforeUpdate(DataObjectController controller, DataObject obj);

        /// <summary>
        /// Обработчик, вызываемый перед удалением объекта.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="obj">Объект.</param>
        /// <returns><see langword="true" /> для продолжения операции.</returns>
        bool BeforeDelete(DataObjectController controller, DataObject obj);

        /// <summary>
        /// Обработчик, вызываемый после вычитывания объектов.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="objs">Вычитанные объекты.</param>
        void AfterGet(DataObjectController controller, ref DataObject[] objs);

        /// <summary>
        /// Обработчик, вызываемый после создания объекта.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="obj">Объект после создания.</param>
        void AfterCreate(DataObjectController controller, DataObject obj);

        /// <summary>
        /// Обработчик, вызываемый после обновления объекта.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="obj">Объект после обновления.</param>
        void AfterUpdate(DataObjectController controller, DataObject obj);

        /// <summary>
        /// Обработчик, вызываемый после удаления объекта.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="obj">Объект перед удалением.</param>
        void AfterDelete(DataObjectController controller, DataObject obj);

        /// <summary>
        /// Обработчик, вызываемый после возникновения исключения.
        /// </summary>
        /// <param name="controller">Контроллер OData.</param>
        /// <param name="ex">Исключение, которое возникло внутри ODataService.</param>
        /// <param name="code">Возвращаемый код HTTP. По-умолчанияю 500.</param>
        /// <returns>Исключение, которое будет отправлено клиенту.</returns>
        Exception AfterInternalServerError(DataObjectController controller, Exception ex, ref HttpStatusCode code);
    }
}
