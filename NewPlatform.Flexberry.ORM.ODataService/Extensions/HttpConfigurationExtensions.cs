namespace NewPlatform.Flexberry.ORM.ODataService.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Routing;
    using ICSSoft.STORMNET.Business;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNet.OData.Formatter.Serialization;
    using Microsoft.AspNet.OData.Routing;
    using Microsoft.AspNet.OData.Routing.Conventions;
    using Microsoft.OData;
    using Microsoft.OData.Edm;
    using NewPlatform.Flexberry.ORM.ODataService.Batch;
    using NewPlatform.Flexberry.ORM.ODataService.Controllers;
    using NewPlatform.Flexberry.ORM.ODataService.Files.Providers;
    using NewPlatform.Flexberry.ORM.ODataService.Formatter;
    using NewPlatform.Flexberry.ORM.ODataService.Handlers;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Routing;

    /// <summary>
    /// Класс, содержащий расширения для сервиса данных.
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// Maps the OData Service DataObject route.
        /// </summary>
        /// <param name="config">The current HTTP configuration.</param>
        /// <param name="builder">The EDM model builder.</param>
        /// <param name="httpServer">HttpServer instance (GlobalConfiguration.DefaultServer).</param>
        /// <param name="routeName">The name of the route (<see cref="DataObjectRoutingConventions.DefaultRouteName"/> is default).</param>
        /// <param name="routePrefix">The route prefix (<see cref="DataObjectRoutingConventions.DefaultRoutePrefix"/> is default).</param>
        /// <returns>A <see cref="ManagementToken"/> instance.</returns>
        public static ManagementToken MapDataObjectRoute(
            this HttpConfiguration config,
            IDataObjectEdmModelBuilder builder,
            HttpServer httpServer,
            string routeName = DataObjectRoutingConventions.DefaultRouteName,
            string routePrefix = DataObjectRoutingConventions.DefaultRoutePrefix)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config), "Contract assertion not met: config != null");
            }

            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), "Contract assertion not met: builder != null");
            }

            if (routeName == null)
            {
                throw new ArgumentNullException(nameof(routeName), "Contract assertion not met: routeName != null");
            }

            if (routeName == string.Empty)
            {
                throw new ArgumentException("Contract assertion not met: routeName != string.Empty", nameof(routeName));
            }

            if (routePrefix == null)
            {
                throw new ArgumentNullException(nameof(routePrefix), "Contract assertion not met: routePrefix != null");
            }

            if (routePrefix == string.Empty)
            {
                throw new ArgumentException("Contract assertion not met: routePrefix != string.Empty", nameof(routePrefix));
            }

            // Model.
            DataObjectEdmModel model = builder.Build();

            // Batch requests support.
            IDataService dataService = (IDataService)config.DependencyResolver.GetService(typeof(IDataService));
            var batchHandler = new DataObjectODataBatchHandler(dataService, httpServer);

            if (dataService == null)
            {
                throw new InvalidOperationException("IDataService is not registered in the dependency scope.");
            }

            // Routing for DataObjects with $batch endpoint and custom formatters.
            var pathHandler = new ExtendedODataPathHandler();
            var routingConventions = DataObjectRoutingConventions.CreateDefault();
            ODataRoute route = config.MapODataServiceRoute(routeName, routePrefix, cb => cb
                .AddService(ServiceLifetime.Singleton, typeof(IEdmModel), sp => model)
                .AddService(ServiceLifetime.Singleton, typeof(IEnumerable<IODataRoutingConvention>), sp => DataObjectRoutingConventions.CreateDefault())
                .AddService(ServiceLifetime.Singleton, typeof(IODataPathHandler), sp => new ExtendedODataPathHandler())
                .AddService(ServiceLifetime.Singleton, typeof(ODataSerializerProvider), sp => new CustomODataSerializerProvider(sp)));

            // Token.
            ManagementToken token = route.CreateManagementToken(model);

            // Controllers.
            var registeredActivator = (IHttpControllerActivator)config.Services.GetService(typeof(IHttpControllerActivator));
            var fallbackActivator = registeredActivator ?? new DefaultHttpControllerActivator();
            config.Services.Replace(typeof(IHttpControllerActivator), new DataObjectControllerActivator(fallbackActivator));

            // Handlers.
            if (config.MessageHandlers.FirstOrDefault(h => h is PostPatchHandler) == null)
            {
                config.MessageHandlers.Add(new PostPatchHandler());
            }

            return token;
        }

        /// <summary>
        /// Maps the OData Service DataObject route.
        /// </summary>
        /// <param name="config">The current HTTP configuration.</param>
        /// <param name="builder">The EDM model builder.</param>
        /// <param name="httpServer">HttpServer instance (GlobalConfiguration.DefaultServer).</param>
        /// <param name="routeName">The name of the route (<see cref="DataObjectRoutingConventions.DefaultRouteName"/> is default).</param>
        /// <param name="routePrefix">The route prefix (<see cref="DataObjectRoutingConventions.DefaultRoutePrefix"/> is default).</param>
        /// <returns>A <see cref="ManagementToken"/> instance.</returns>
        [Obsolete("Use MapDataObjectRoute() method instead.")]
        public static ManagementToken MapODataServiceDataObjectRoute(
            this HttpConfiguration config,
            IDataObjectEdmModelBuilder builder,
            HttpServer httpServer,
            string routeName = DataObjectRoutingConventions.DefaultRouteName,
            string routePrefix = DataObjectRoutingConventions.DefaultRoutePrefix)
        {
            return MapDataObjectRoute(config, builder, httpServer, routeName, routePrefix);
        }

        /// <summary>
        /// Осуществляет регистрацию маршрута для загрузки/скачивания файлов.
        /// </summary>
        /// <param name="httpConfiguration">Используемая конфигурация.</param>
        /// <param name="routeName">Имя регистрируемого маршрута.</param>
        /// <param name="routeTemplate">Шаблон регистрируемого маршрута.</param>
        /// <param name="uploadsDirectoryPath">Пути к каталогу, который предназначен для хранения файлов загружаемых на сервер.</param>
        /// <param name="dataObjectFileProviders">
        /// Провайдеры файловых свойств объектов данных, которые будут использоваться для связывания файлов с объектами данных.
        /// </param>
        /// <param name="dataService">Сервис данных для операций с БД.</param>
        /// <returns>Зарегистрированный маршрут.</returns>
        public static IHttpRoute MapODataServiceFileRoute(
            this HttpConfiguration httpConfiguration,
            string routeName,
            string routeTemplate,
            string uploadsDirectoryPath,
            IEnumerable<IDataObjectFileProvider> dataObjectFileProviders,
            IDataService dataService)
        {
            // Регистрируем маршрут для загрузки/скачивания файлов через отдельный контроллер.
            IHttpRoute route = httpConfiguration.Routes.MapHttpRoute(routeName, routeTemplate, defaults: new { controller = "File" });

            // Регистрируем провайдеры файловых свойств объектов данных.
            List<IDataObjectFileProvider> providers = new List<IDataObjectFileProvider>
            {
                new DataObjectFileProvider(dataService),
                new DataObjectWebFileProvider(dataService)
            };
            providers.AddRange(dataObjectFileProviders ?? new List<IDataObjectFileProvider>());

            foreach (IDataObjectFileProvider provider in providers)
            {
                FileController.RegisterDataObjectFileProvider(provider);
            }

            // Регистрируем имя маршрута.
            FileController.RouteName = routeName;

            // Регистрируем каталог для загрузок.
            FileController.UploadsDirectoryPath = uploadsDirectoryPath;

            // FileController.BaseUrl регистрируется перед обработкой какого-либо запроса (см. Controllers/BaseApiController и Controllers/BaseODataController),
            // т.к. при инициализации приложения нельзя получить корректный URL, по которому будет доступен тот или иной контроллер,
            // это возможно только в контексте обработки какого-либо запроса.
            return route;
        }

        /// <summary>
        /// Осуществляет регистрацию маршрута для загрузки/скачивания файлов.
        /// </summary>
        /// <param name="httpConfiguration">Используемая конфигурация.</param>
        /// <param name="routeName">Имя регистрируемого маршрута.</param>
        /// <param name="routeTemplate">Шаблон регистрируемого маршрута.</param>
        /// <param name="uploadsDirectoryPath">Пути к каталогу, который предназначен для хранения файлов загружаемых на сервер.</param>
        /// <param name="dataService">Сервис данных для операций с БД.</param>
        /// <returns>Зарегистрированный маршрут.</returns>
        public static IHttpRoute MapODataServiceFileRoute(
            this HttpConfiguration httpConfiguration,
            string routeName,
            string routeTemplate,
            string uploadsDirectoryPath,
            IDataService dataService)
        {
            return httpConfiguration.MapODataServiceFileRoute(routeName, routeTemplate, uploadsDirectoryPath, null, dataService);
        }
    }
}
