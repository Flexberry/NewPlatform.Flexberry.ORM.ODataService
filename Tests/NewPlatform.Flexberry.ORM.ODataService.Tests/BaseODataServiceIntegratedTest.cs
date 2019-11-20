﻿namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using ICSSoft.STORMNET.Business;

    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Model;

    using Unity;
    using Unity.AspNet.WebApi;

    /// <summary>
    /// Базовый класс для тестирования работы с данными через ODataService.
    /// </summary>
    public class BaseODataServiceIntegratedTest : BaseIntegratedTest
    {
        protected readonly IDataObjectEdmModelBuilder _builder;

        public class TestArgs
        {
            private readonly Lazy<ManagementToken> _token;

            public IUnityContainer UnityContainer { get; set; }

            public ManagementToken Token => _token.Value;

            public IDataService DataService { get; set; }

            public HttpClient HttpClient { get; set; }

            public TestArgs(Func<ManagementToken> func)
            {
                _token = new Lazy<ManagementToken>(func);
            }
        }

        /// <summary>
        /// Имена сборок с объектами данных.
        /// </summary>
        public Assembly[] DataObjectsAssembliesNames { get; protected set; }

        /// <summary>
        /// Флаг, показывающий нужно ли добавлять пространства имен типов, к именам соответствующих им наборов сущностей.
        /// </summary>
        public bool UseNamespaceInEntitySetName { get; protected set; }

        public BaseODataServiceIntegratedTest(
            string stageCasePath = @"РТЦ Тестирование и документирование\Модели для юнит-тестов\Flexberry ORM\NewPlatform.Flexberry.ORM.ODataService.Tests\",
            bool useNamespaceInEntitySetName = false, bool useGisDataService = false)
            : base("ODataDB", useGisDataService)
        {
            DataObjectsAssembliesNames = new[]
            {
                typeof(Car).Assembly,
                //typeof(Agent).Assembly
            };
            UseNamespaceInEntitySetName = useNamespaceInEntitySetName;
            var builder = new DefaultDataObjectEdmModelBuilder(DataObjectsAssembliesNames, UseNamespaceInEntitySetName);
            //builder.PropertyFilter = PropertyFilter;
            _builder = builder;
        }

        /// <summary>
        /// Осуществляет перебор тестовых сервисов данных из <see cref="BaseOrmIntegratedTest"/>, и вызывает переданный делегат
        /// для каждого сервиса данных, передав в него <see cref="HttpClient"/> для осуществления запросов к OData-сервису.
        /// </summary>
        /// <param name="action">Действие, выполняемое для каждого сервиса данных из <see cref="BaseOrmIntegratedTest"/>.</param>
        public virtual void ActODataService(Action<TestArgs> action)
        {
            if (action == null)
                return;

            foreach (IDataService dataService in DataServices)
            {
                var container = new UnityContainer();
                container.RegisterInstance(dataService);

                using (var config = new HttpConfiguration())
                using (var server = new HttpServer(config))
                using (var client = new HttpClient(server, false) { BaseAddress = new Uri("http://localhost/odata/") })
                {
                    server.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
                    config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
                    config.DependencyResolver = new UnityDependencyResolver(container);

                    var args = new TestArgs(() => config.MapODataServiceDataObjectRoute(_builder, new HttpServer())) { UnityContainer = container, DataService = dataService, HttpClient = client };
                    action(args);
                }
            }
        }

        /*
        private bool PropertyFilter(PropertyInfo propertyInfo)
        {
            return Information.ExtractPropertyInfo<Agent>(x => x.Pwd) != propertyInfo;
        }
        */
    }
}
