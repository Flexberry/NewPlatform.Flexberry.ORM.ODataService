#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Collections.Generic;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NewPlatform.Flexberry.ORM.ODataService;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.Services;
    using ODataServiceSample.AspNetCore;
    using Unity;

    /// <summary>
    /// Startup for testing UpdateView configuration - a view that is used instead of a default view during data object updates.
    /// Differs from TestStartup that it sets UpdateView for <see cref="Берлога"/> and <see cref="Медведь"/> data objects.
    /// </summary>
    public class UpdateViewsTestStartup : Startup
    {
        /// <summary>
        /// Initialize new instance of TestStartup.
        /// </summary>
        /// <param name="configuration">Configuration for new instance.</param>
        public UpdateViewsTestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;
            IUnityContainer unityContainer = serviceProvider.GetRequiredService<IUnityContainer>();
            unityContainer.RegisterInstance(env);

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
                var updateViews = new Dictionary<Type, View>() // setting updateViews for testing
                {
                    { typeof(Медведь), Медведь.Views.МедведьUpdateView },
                    { typeof(Берлога), Берлога.Views.БерлогаUpdateView },
                };
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, serviceProvider, false, pseudoDetailDefinitions, updateViews: updateViews);

                var token = builder.MapDataObjectRoute(modelBuilder);

                unityContainer.RegisterInstance(typeof(ManagementToken), token);
            });
        }
    }
}
#endif
