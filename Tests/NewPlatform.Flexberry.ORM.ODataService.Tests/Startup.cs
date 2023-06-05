#if NETCOREAPP
namespace ODataServiceSample.AspNetCore
{
    using System;
    using System.Linq;
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Interfaces;
    using ICSSoft.STORMNET.Security;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using NewPlatform.Flexberry;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Files;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Tests;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Exceptions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Extensions;
    using NewPlatform.Flexberry.Services;
    using Unity;
    using Unity.Injection;
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

        public string CustomizationString => "";

        /// <summary>
        /// Method for Unity container configuring.
        /// </summary>
        /// <param name="unityContainer">Unity container.</param>
        public virtual void ConfigureContainer(IUnityContainer unityContainer)
        {
            // Configure Flexberry services via Unity.
            var securityManager = new EmptySecurityManager();
            Mock<IAuditService> mockAuditService = new Mock<IAuditService>();
            IBusinessServerProvider businessServerProvider = unityContainer.Resolve<IBusinessServerProvider>();
            IDataService dataService = new PostgresDataService(securityManager, mockAuditService.Object, businessServerProvider) { CustomizationString = CustomizationString };

            unityContainer.RegisterType<DataObjectEdmModelDependencies>(
                new InjectionConstructor(
                    unityContainer.IsRegistered<IExportService>() ? unityContainer.Resolve<IExportService>() : null,
                    unityContainer.IsRegistered<IExportService>("Export") ? unityContainer.Resolve<IExportService>("Export") : null,
                    unityContainer.IsRegistered<IExportStringedObjectViewService>() ? unityContainer.Resolve<IExportStringedObjectViewService>() : null,
                    unityContainer.IsRegistered<IExportStringedObjectViewService>("ExportStringedObjectView") ? unityContainer.Resolve<IExportStringedObjectViewService>("ExportStringedObjectView") : null,
                    unityContainer.IsRegistered<IODataExportService>() ? unityContainer.Resolve<IODataExportService>() : null,
                    unityContainer.IsRegistered<IODataExportService>("Export") ? unityContainer.Resolve<IODataExportService>("Export") : null));
            unityContainer.RegisterInstance(dataService);
            unityContainer.RegisterInstance<ILockService>(new LockService(dataService));
            unityContainer.RegisterInstance<ISecurityManager>(new EmptySecurityManager());
        }

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
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, app.ApplicationServices, false);

                var token = builder.MapDataObjectRoute(modelBuilder);
            });
        }
    }
}
#endif
