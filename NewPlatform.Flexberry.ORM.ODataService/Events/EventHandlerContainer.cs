namespace NewPlatform.Flexberry.ORM.ODataService.Events
{
    using System;
    using System.Net;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;

    using NewPlatform.Flexberry.ORM.ODataService.Controllers;

    /// <summary>
    /// Default implementation of <see cref="IEventHandlerContainer"/>.
    /// </summary>
    internal class EventHandlerContainer : IEventHandlerContainer
    {
        /// <inheritdoc />
        public ManagementToken Token { get; set; }

        /// <inheritdoc />
        public bool BeforeGet(DataObjectController controller, ref LoadingCustomizationStruct lcs)
        {
            return true;
        }

        /// <inheritdoc />
        public bool BeforeCreate(DataObjectController controller, DataObject obj)
        {
            return true;
        }

        /// <inheritdoc />
        public bool BeforeUpdate(DataObjectController controller, DataObject obj)
        {
            return true;
        }

        /// <inheritdoc />
        public bool BeforeDelete(DataObjectController controller, DataObject obj)
        {
            return true;
        }

        /// <inheritdoc />
        public void AfterGet(DataObjectController controller, ref DataObject[] objs)
        {
        }

        /// <inheritdoc />
        public void AfterCreate(DataObjectController controller, DataObject obj)
        {
        }

        /// <inheritdoc />
        public void AfterUpdate(DataObjectController controller, DataObject obj)
        {
        }

        /// <inheritdoc />
        public void AfterDelete(DataObjectController controller, DataObject obj)
        {
        }

        /// <inheritdoc />
        public Exception AfterInternalServerError(DataObjectController controller, Exception ex, ref HttpStatusCode code)
        {
            return ex;
        }
    }
}