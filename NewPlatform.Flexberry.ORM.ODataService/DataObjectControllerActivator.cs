﻿namespace NewPlatform.Flexberry.ORM.ODataService
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Routing;

    using ICSSoft.STORMNET.Business;

    using NewPlatform.Flexberry.ORM.ODataService.Controllers;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Offline;

    /// <summary>
    /// Controller activator class for instantiating <see cref="DataObjectController"/> with parameters.
    /// </summary>
    /// <seealso cref="System.Web.Http.Dispatcher.IHttpControllerActivator" />
    public class DataObjectControllerActivator : IHttpControllerActivator
    {
        /// <summary>
        /// Activator for all controllers except <see cref="DataObjectController"/>.
        /// </summary>
        private readonly IHttpControllerActivator _fallbackActivator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectControllerActivator"/> class.
        /// </summary>
        /// <param name="fallbackActivator">Activator for all controllers except <see cref="DataObjectController"/>.</param>
        public DataObjectControllerActivator(IHttpControllerActivator fallbackActivator)
        {
            Contract.Requires<ArgumentNullException>(fallbackActivator != null);

            _fallbackActivator = fallbackActivator;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Web.Http.Controllers.IHttpController" /> object.
        /// Creates <see cref="DataObjectController"/> using current <see cref="IDependencyScope"/> and <see cref="IHttpRoute"/>.
        /// </summary>
        /// <param name="request">The message request.</param>
        /// <param name="controllerDescriptor">The HTTP controller descriptor.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        /// An <see cref="T:System.Web.Http.Controllers.IHttpController" /> object for specified arguments.
        /// </returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            if (controllerDescriptor.ControllerName == "DataObject")
            {
                var dr = request.GetConfiguration().DependencyResolver;
                var ds = (IDataService)dr.GetService(typeof(IDataService));
                if (ds == null)
                {
                    throw new InvalidOperationException("IDataService is not registered in the dependency scope.");
                }

                var token = request.GetODataServiceToken();

                var controller = new DataObjectController(ds, token.Model, token.Events, token.Functions);

                var offlineManager = (BaseOfflineManager)dr.GetService(typeof(BaseOfflineManager));
                if (offlineManager != null)
                {
                    controller.OfflineManager = offlineManager;
                }

                return controller;
            }

            return _fallbackActivator.Create(request, controllerDescriptor, controllerType);
        }
    }
}
