﻿namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using NewPlatform.Flexberry.ORM.ODataService.Files;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers;

    using Unity;
    using Xunit;
    using Xunit.Abstractions;

#if NETFRAMEWORK
    using System.Web.Http;
    using System.Web.Http.Cors;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using Unity.AspNet.WebApi;
    using Unity.Injection;
#endif
#if NETCOREAPP
    using NewPlatform.Flexberry.ORM.ODataService.Routing;
#endif

    /// <summary>
    /// Базовый класс для тестирования работы с данными через ODataService.
    /// </summary>
    public class BaseODataServiceIntegratedTest : BaseIntegratedTest
    {
        protected IDataObjectEdmModelBuilder _builder;

        public class TestArgs
        {
            public IUnityContainer UnityContainer { get; set; }

            public ManagementToken Token { get; set; }

            public IDataService DataService { get; set; }

            public HttpClient HttpClient { get; set; }
        }

        /// <summary>
        /// Имена сборок с объектами данных.
        /// </summary>
        public Assembly[] DataObjectsAssembliesNames { get; protected set; }

        /// <summary>
        /// Флаг, показывающий нужно ли добавлять пространства имен типов, к именам соответствующих им наборов сущностей.
        /// </summary>
        public bool UseNamespaceInEntitySetName { get; protected set; }

#if NETFRAMEWORK
        public BaseODataServiceIntegratedTest(
            string stageCasePath = @"РТЦ Тестирование и документирование\Модели для юнит-тестов\Flexberry ORM\NewPlatform.Flexberry.ORM.ODataService.Tests\",
            bool useNamespaceInEntitySetName = false,
            bool useGisDataService = false,
            PseudoDetailDefinitions pseudoDetailDefinitions = null)
            : base("ODataDB", useGisDataService)
        {
            Init(useNamespaceInEntitySetName, pseudoDetailDefinitions);
        }
#endif
#if NETCOREAPP
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseODataServiceIntegratedTest"/> class.
        /// </summary>
        /// <param name="factory">Factory for application.</param>
        /// <param name="output">Debug information output.</param>
        /// <param name="useNamespaceInEntitySetName">Flag indicating whether type namespaces should be added to the names of their corresponding entity sets.</param>
        /// <param name="useGisDataService">Flag indicating whether GisDataService be used.</param>
        /// <param name="pseudoDetailDefinitions">OData definition of the link from master to pseudodetail (pseudoproperty).</param>
        public BaseODataServiceIntegratedTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, ITestOutputHelper output = null, bool useNamespaceInEntitySetName = false,  bool useGisDataService = false, PseudoDetailDefinitions pseudoDetailDefinitions = null)
            : base(factory, output, "ODataDB", useGisDataService)
        {
            Init(useNamespaceInEntitySetName, pseudoDetailDefinitions);
        }
#endif

        private void Init(
            bool useNamespaceInEntitySetName = false,
            PseudoDetailDefinitions pseudoDetailDefinitions = null)
        {
            DataObjectsAssembliesNames = new[]
            {
                typeof(Car).Assembly,
            };
            UseNamespaceInEntitySetName = useNamespaceInEntitySetName;

            _container.RegisterType<DataObjectEdmModelDependencies>(
                new InjectionConstructor(
                    _container.IsRegistered<IExportService>() ? _container.Resolve<IExportService>() : null,
                    _container.IsRegistered<IExportService>("Export") ? _container.Resolve<IExportService>("Export") : null,
                    _container.IsRegistered<IExportStringedObjectViewService>() ? _container.Resolve<IExportStringedObjectViewService>() : null,
                    _container.IsRegistered<IExportStringedObjectViewService>("ExportStringedObjectView") ? _container.Resolve<IExportStringedObjectViewService>("ExportStringedObjectView") : null,
                    _container.IsRegistered<IODataExportService>() ? _container.Resolve<IODataExportService>() : null,
                    _container.IsRegistered<IODataExportService>("Export") ? _container.Resolve<IODataExportService>("Export") : null));

            _builder = new DefaultDataObjectEdmModelBuilder(DataObjectsAssembliesNames, _serviceProvider, UseNamespaceInEntitySetName, pseudoDetailDefinitions);
        }

        /// <summary>
        /// Метод вызываемый после возникновения исключения.
        /// </summary>
        /// <param name="e">Исключение, которое возникло внутри ODataService.</param>
        /// <param name="code">Возвращаемый код HTTP. По-умолчанияю 500.</param>
        /// <returns>Исключение, которое будет отправлено клиенту.</returns>
        public Exception AfterInternalServerError(Exception e, ref HttpStatusCode code)
        {
            if (_container.IsRegistered<ITestOutputHelper>())
            {
                ITestOutputHelper output = _container.Resolve<ITestOutputHelper>();
                output.WriteLine(e.ToString());
            }

            Assert.Null(e);
            code = HttpStatusCode.InternalServerError;
            return e;
        }

#if NETFRAMEWORK
        /// <summary>
        /// Осуществляет перебор тестовых сервисов данных из <see cref="BaseIntegratedTest"/>, и вызывает переданный делегат
        /// для каждого сервиса данных, передав в него <see cref="HttpClient"/> для осуществления запросов к OData-сервису.
        /// </summary>
        /// <param name="action">Действие, выполняемое для каждого сервиса данных из <see cref="BaseIntegratedTest"/>.</param>
        public virtual void ActODataService(Action<TestArgs> action)
        {
            if (action == null)
                return;

            foreach (IDataService dataService in DataServices)
            {
                using (var config = new HttpConfiguration())
                using (var server = new HttpServer(config))
                using (var client = new HttpClient(server, false) { BaseAddress = new Uri("http://localhost/odata/") })
                {
                    _container.RegisterInstance(dataService);

                    server.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
                    config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
                    config.DependencyResolver = new UnityDependencyResolver(_container);

                    const string fileControllerPath = "api/File";
                    config.MapODataServiceFileRoute("File", fileControllerPath);
                    var fileAccessor = new DefaultDataObjectFileAccessor(new Uri("http://localhost/"), fileControllerPath, "Uploads");
                    _container.RegisterInstance<IDataObjectFileAccessor>(fileAccessor);

                    var token = config.MapDataObjectRoute(_builder, server, "odata", "odata", true);
                    token.Events.CallbackAfterInternalServerError = AfterInternalServerError;
                    var args = new TestArgs { UnityContainer = _container, DataService = dataService, HttpClient = client, Token = token };
                    action(args);
                }
            }
        }
#endif
#if NETCOREAPP
        /// <summary>
        /// Осуществляет перебор тестовых сервисов данных из <see cref="BaseIntegratedTest"/>, и вызывает переданный делегат
        /// для каждого сервиса данных, передав в него <see cref="HttpClient"/> для осуществления запросов к OData-сервису.
        /// </summary>
        /// <param name="action">Действие, выполняемое для каждого сервиса данных из <see cref="BaseIntegratedTest"/>.</param>
        public virtual void ActODataService(Action<TestArgs> action)
        {
            if (action == null)
                return;

            foreach (IDataService dataService in DataServices)
            {
                // Инициализация сервера и клиента.
                HttpClient client = _factory.CreateClient();

                // Add "/odata/" postfix.
                client.BaseAddress = new Uri(client.BaseAddress, DataObjectRoutingConventions.DefaultRouteName + "/");

                IUnityContainer container = TestStartup._unityContainer; // При создании odata-приложения оригинальный контейнер не изменяется.
                ManagementToken token = (ManagementToken)container.Resolve(typeof(ManagementToken));
                container.RegisterInstance(dataService);
                token.Events.CallbackAfterInternalServerError = AfterInternalServerError;

                var fileAccessor = (IDataObjectFileAccessor)_factory.Services.GetService(typeof(IDataObjectFileAccessor));
                container.RegisterInstance(fileAccessor);

                var args = new TestArgs { UnityContainer = container, DataService = dataService, HttpClient = client, Token = token };
                action(args);
            }
        }
#endif

        protected void CheckODataBatchResponseStatusCode(HttpResponseMessage response, HttpStatusCode[] statusCodes)
        {
            BatchHelper.CheckODataBatchResponseStatusCode(response, statusCodes);
        }

        protected HttpRequestMessage CreateBatchRequest(string url, string[] changesets)
        {
            return BatchHelper.CreateBatchRequest(url, changesets);
        }

        /// <summary>
        /// Проверка наличия поддержки Gis текущей реализацией <see cref="IDataService"/>.
        /// </summary>
        /// <param name="dataService">Сервис данных.</param>
        /// <returns>Значение true, если текущая реализация <see cref="IDataService"/> поддерживает Gis.</returns>
        protected bool GisIsAvailable(IDataService dataService)
        {
            return dataService is GisPostgresDataService || dataService is GisMSSQLDataService;
        }

        protected string CreateChangeset(string url, string body, DataObject dataObject)
        {
            return BatchHelper.CreateChangeset(url, body, dataObject);
        }

        /*
        private bool PropertyFilter(PropertyInfo propertyInfo)
        {
            return Information.ExtractPropertyInfo<Agent>(x => x.Pwd) != propertyInfo;
        }
        */
    }
}
