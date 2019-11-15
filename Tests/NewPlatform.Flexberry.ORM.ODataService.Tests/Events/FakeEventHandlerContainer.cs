namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Events
{
    using System;
    using System.Net;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;

    using NewPlatform.Flexberry.ORM.ODataService.Controllers;
    using NewPlatform.Flexberry.ORM.ODataService.Events;

    public class FakeEventHandlerContainer : IEventHandlerContainer
    {
        /// <inheritdoc />
        public ManagementToken Token { get; set; }

        /// <summary>
        /// Делегат для вызова логики перед выполнением запроса.
        /// </summary>
        public DelegateBeforeGet CallbackBeforeGet { get; set; }

        /// <summary>
        /// Делегат для вызова логики перед изменением объекта.
        /// </summary>
        public DelegateBeforeUpdate CallbackBeforeUpdate { get; set; }

        /// <summary>
        /// Делегат для вызова логики перед созданием объекта.
        /// </summary>
        public DelegateBeforeCreate CallbackBeforeCreate { get; set; }

        /// <summary>
        /// Делегат для вызова логики перед удалением объекта.
        /// </summary>
        public DelegateBeforeDelete CallbackBeforeDelete { get; set; }

        /// <summary>
        /// Делегат для вызова логики после вычитывания объектов.
        /// </summary>
        public DelegateAfterGet CallbackAfterGet { get; set; }

        /// <summary>
        /// Делегат для вызова логики после сохранения объекта.
        /// </summary>
        public DelegateAfterCreate CallbackAfterCreate { get; set; }

        /// <summary>
        /// Делегат для вызова логики после обновления объекта.
        /// </summary>
        public DelegateAfterUpdate CallbackAfterUpdate { get; set; }

        /// <summary>
        /// Делегат для вызова логики после удаления объекта.
        /// </summary>
        public DelegateAfterDelete CallbackAfterDelete { get; set; }

        /// <summary>
        /// Делегат, вызываемый после возникновения исключения.
        /// </summary>
        public DelegateAfterInternalServerError CallbackAfterInternalServerError { get; set; }

        /// <inheritdoc />
        public bool BeforeGet(DataObjectController controller, ref LoadingCustomizationStruct lcs)
        {
            return CallbackBeforeGet == null || CallbackBeforeGet(controller, ref lcs);
        }

        /// <inheritdoc />
        public bool BeforeCreate(DataObjectController controller, DataObject obj)
        {
            return CallbackBeforeCreate == null || CallbackBeforeCreate(controller, obj);
        }

        /// <inheritdoc />
        public bool BeforeUpdate(DataObjectController controller, DataObject obj)
        {
            return CallbackBeforeUpdate == null || CallbackBeforeUpdate(controller, obj);
        }

        /// <inheritdoc />
        public bool BeforeDelete(DataObjectController controller, DataObject obj)
        {
            return CallbackBeforeDelete == null || CallbackBeforeDelete(controller, obj);
        }

        /// <inheritdoc />
        public void AfterGet(DataObjectController controller, ref DataObject[] objs)
        {
            CallbackAfterGet?.Invoke(controller, ref objs);
        }

        /// <inheritdoc />
        public void AfterCreate(DataObjectController controller, DataObject obj)
        {
            CallbackAfterCreate?.Invoke(controller, obj);
        }

        /// <inheritdoc />
        public void AfterUpdate(DataObjectController controller, DataObject obj)
        {
            CallbackAfterUpdate?.Invoke(controller, obj);
        }

        /// <inheritdoc />
        public void AfterDelete(DataObjectController controller, DataObject obj)
        {
            CallbackAfterDelete?.Invoke(controller, obj);
        }

        /// <inheritdoc />
        public Exception AfterInternalServerError(DataObjectController controller, Exception ex, ref HttpStatusCode code)
        {
            if (CallbackAfterInternalServerError == null)
            {
                return ex;
            }

            return CallbackAfterInternalServerError(controller, ex, ref code);
        }
    }
}