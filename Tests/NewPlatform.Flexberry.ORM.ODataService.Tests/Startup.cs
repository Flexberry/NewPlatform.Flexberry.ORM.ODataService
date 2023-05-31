#if NETCOREAPP
namespace ODataServiceSample.AspNetCore
{
    using System;
    using System.IO;
    using System.Linq;
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Interfaces;
    using ICSSoft.STORMNET.Security;
    using ICSSoft.STORMNET.Windows.Forms;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Files;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Tests;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Exceptions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Extensions;
    using NewPlatform.Flexberry.Services;
    using Unity;

    using LockService = NewPlatform.Flexberry.Services.LockService;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IApplicationBuilder ApplicationBuilder { get; set; }

        private IServerAddressesFeature ServerAddressesFeature { get; set; }

        public IConfiguration Configuration { get; }

        protected IServiceProvider _serviceProvider;

        public string CustomizationString => "";

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            /*
            // Configure Flexberry services (LockService and IDataService) via native DI.
            {
                services.AddSingleton<IDataService>(provider =>
                {
                    IDataService dataService = new PostgresDataService() { CustomizationString = CustomizationString };
                    ExternalLangDef.LanguageDef.DataService = dataService;

                    return dataService;
                });
                
                services.AddSingleton<ILockService, LockService>();
            }
            */

            // Configure Flexberry services via Unity.
            IUnityContainer unityContainer = new UnityContainer();
            IServiceProvider serviceProvider = new UnityServiceProvider(unityContainer);
            var securityManager = new EmptySecurityManager();
            Mock<IAuditService> mockAuditService = new Mock<IAuditService>();
            Mock<IBusinessServerProvider> mockBusinessServerProvider = new Mock<IBusinessServerProvider>();
            IDataService dataService = new PostgresDataService(securityManager, mockAuditService.Object, mockBusinessServerProvider.Object) { CustomizationString = CustomizationString };

            var assemblies = new[]
            {
                    typeof(Медведь).Assembly,
                    typeof(ApplicationLog).Assembly,
                    typeof(UserSetting).Assembly,
                    typeof(Lock).Assembly,
            };

            unityContainer.RegisterType<DataObjectEdmModelDependencies>();
            unityContainer.RegisterInstance(dataService);
            unityContainer.RegisterInstance<ILockService>(new LockService(dataService));
            unityContainer.RegisterInstance<ISecurityManager>(new EmptySecurityManager());
            unityContainer.RegisterFactory<IDataObjectEdmModelBuilder>(
                new Func<IUnityContainer, object>(o => new DefaultDataObjectEdmModelBuilder(assemblies, new UnityServiceProvider(o), false)), FactoryLifetime.Singleton);

            _serviceProvider = serviceProvider;

            services.AddMvcCore(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
                options.EnableEndpointRouting = false;
            })
                .AddFormatterMappings();

            services.AddODataService();

            services.AddSingleton<IDataObjectFileAccessor>(provider =>
            {
                Uri baseUri = new Uri("http://localhost");

                if (ServerAddressesFeature != null && ServerAddressesFeature.Addresses != null)
                {
                    // This works with pure self-hosted service only.
                    baseUri = new Uri(ServerAddressesFeature.Addresses.Single());
                }

                var env = provider.GetRequiredService<IHostingEnvironment>();

                return new DefaultDataObjectFileAccessor(baseUri, "api/File", "Uploads");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Save reference to IApplicationBuilder instance.
            ApplicationBuilder = app;

            // Save reference to IServerAddressesFeature instance.
            ServerAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();

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
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, _serviceProvider, false);

                var token = builder.MapDataObjectRoute(modelBuilder);
            });
        }
    }
}
#endif
