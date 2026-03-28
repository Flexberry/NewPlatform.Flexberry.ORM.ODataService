#if NETFRAMEWORK
#else
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Net.Http;
    using ICSSoft.Services;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NewPlatform.Flexberry.ORM.ODataService;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Handlers;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.Services;
    using ODataServiceSample.AspNetCore;
    using Unity;
    using Unity.Injection;

    /// <summary>
    /// Startup for tests.
    /// </summary>
    public class TestStartup : Startup
    {
        /// <summary>
        /// Initialize new instance of TestStartup.
        /// </summary>
        /// <param name="configuration">Configuration for new instance.</param>
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;
            IUnityContainer unityContainer = serviceProvider.GetRequiredService<IUnityContainer>();
            unityContainer.RegisterInstance(env);
            unityContainer.RegisterType<DataObjectEdmModelDependencies>(
            new InjectionConstructor(
                unityContainer.IsRegistered<IExportService>() ? unityContainer.Resolve<IExportService>() : null,
                unityContainer.IsRegistered<IExportService>("Export") ? unityContainer.Resolve<IExportService>("Export") : null,
                unityContainer.IsRegistered<IExportStringedObjectViewService>() ? unityContainer.Resolve<IExportStringedObjectViewService>() : null,
                unityContainer.IsRegistered<IExportStringedObjectViewService>("ExportStringedObjectView") ? unityContainer.Resolve<IExportStringedObjectViewService>("ExportStringedObjectView") : null,
                unityContainer.IsRegistered<IODataExportService>() ? unityContainer.Resolve<IODataExportService>() : null,
                unityContainer.IsRegistered<IODataExportService>("Export") ? unityContainer.Resolve<IODataExportService>("Export") : null));

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<NewPlatform.Flexberry.ORM.ODataService.Handlers.RequestSizeLimitMiddleware>();

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

                PseudoDetailDefinitions pseudoDetailDefinitions = (PseudoDetailDefinitions)serviceProvider.GetService(typeof(PseudoDetailDefinitions));
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, false, pseudoDetailDefinitions, dataObjectEdmModelDependencies: unityContainer.Resolve<DataObjectEdmModelDependencies>());

                var token = builder.MapDataObjectRoute(modelBuilder);

                unityContainer.RegisterInstance(typeof(ManagementToken), token);
            });
        }
    }
}
#endif
