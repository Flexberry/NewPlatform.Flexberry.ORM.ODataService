#if NETCOREAPP
namespace ODataServiceSample.AspNetCore
{
    using System;
    using System.Linq;
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Interfaces;
    using ICSSoft.STORMNET.Windows.Forms;
    using ICSSoft.STORMNET.Security;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Files;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Tests;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Exceptions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Extensions;
    using NewPlatform.Flexberry.Services;
    using System;
    using System.Linq;
    using Unity;
    using LockService = NewPlatform.Flexberry.Services.LockService;
    using AdvLimit.ExternalLangDef;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IApplicationBuilder ApplicationBuilder { get; set; }
        private IServerAddressesFeature ServerAddressesFeature { get; set; }
        public IConfiguration Configuration { get; }

        public string CustomizationString => "";

        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Регистрируем MVC с отключённым endpoint routing, добавляем фильтр
            services.AddMvcCore(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
                options.EnableEndpointRouting = false;
            })
            .AddFormatterMappings();

            // Регистрируем OData сервисы
            services.AddODataService();

            // Регистрация IDataObjectFileAccessor с использованием ServerAddressesFeature
            services.AddSingleton<IDataObjectFileAccessor>(provider =>
            {
                Uri baseUri = new Uri("http://localhost");

                if (ServerAddressesFeature != null && ServerAddressesFeature.Addresses != null)
                {
                    baseUri = new Uri(ServerAddressesFeature.Addresses.Single());
                }

                var env = provider.GetRequiredService<IHostingEnvironment>();

                return new DefaultDataObjectFileAccessor(baseUri, "api/File", "Uploads");
            });

            // --- ВАЖНО: Build IServiceProvider для получения настроенных сервисов ---
            var serviceProvider = services.BuildServiceProvider();

            // Получаем сконфигурированный экземпляр IOptions<MvcOptions> из стандартного DI
            var mvcOptions = serviceProvider.GetRequiredService<IOptions<MvcOptions>>();

            // Получаем Unity контейнер
            IUnityContainer unityContainer = UnityFactory.GetContainer();

            // Регистрируем в Unity его же экземпляр IOptions<MvcOptions> для корректной работы MVC через Unity
            unityContainer.RegisterInstance<IOptions<MvcOptions>>(mvcOptions);

            // Регистрируем Flexberry-сервисы через Unity
            var ds = unityContainer.Resolve<IDataService>() as PostgresDataService;
            ds.CustomizationString = CustomizationString;
            DataServiceProvider.DataService = ds;
            unityContainer.RegisterType<IDataService, PostgresDataService>();
            ExternalLangDef.LanguageDef = new ExternalLangDef(ds);

            unityContainer.RegisterInstance<ILockService>(new LockService(ds));
            unityContainer.RegisterInstance<ISecurityManager>(new EmptySecurityManager());
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApplicationBuilder = app;
            ServerAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
// Используем старый стиль маршрутизации MVC (нужен Disable Endpoint Routing выше)
            app.UseMvc(builder =>
            {
                builder.MapRoute("Lock", "api/lock/{action}/{dataObjectId}", new { controller = "Lock" });
                builder.MapFileRoute();
            });

            app.UseODataService(builder =>
            {
                var assemblies = new[]
                {
                    typeof(Медведь).Assembly,
                    typeof(ApplicationLog).Assembly,
                    typeof(UserSetting).Assembly,
                    typeof(Lock).Assembly,
                };

                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, app.ApplicationServices);
                var token = builder.MapDataObjectRoute(modelBuilder);
            });
        }
    }
}
#endif