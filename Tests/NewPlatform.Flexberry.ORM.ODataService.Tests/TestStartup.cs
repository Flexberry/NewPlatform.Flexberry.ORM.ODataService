#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using ICSSoft.Services;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using NewPlatform.Flexberry.ORM.ODataService;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.Services;
    using ODataServiceSample.AspNetCore;
    using Unity;

    /// <summary>
    /// Startup for tests.
    /// </summary>
    public class TestStartup : Startup
    {
        public static IUnityContainer _unityContainer;

        /// <summary>
        /// Initialize new instance of TestStartup.
        /// </summary>
        /// <param name="configuration">Configuration for new instance.</param>
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureContainer(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            base.ConfigureContainer(unityContainer);
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;
            _unityContainer.RegisterInstance(env);

            app.UseMiddleware<ExceptionMiddleware>();

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
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, serviceProvider, false, pseudoDetailDefinitions);

                var token = builder.MapDataObjectRoute(modelBuilder);

                _unityContainer.RegisterInstance(typeof(ManagementToken), token);
            });
        }
    }
}
#endif
