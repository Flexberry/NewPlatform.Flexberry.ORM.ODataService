#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Collections.Generic;
    using DocumentFormat.OpenXml.ExtendedProperties;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using NewPlatform.Flexberry.ORM.ODataService;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.Services;
    using ODataServiceSample.AspNetCore;
    using Unity;
    using Unity.Injection;

    /// <summary>
    /// Startup for testing MasterLightLoad configuration.
    /// Differs from TestStartup that it marks <see cref="Котенок"/> type as data object for which masters should be light-loaded.
    /// </summary>
    public class MasterLightLoadTestStartup : Startup
    {
        /// <summary>
        /// Initialize new instance of MasterLightLoadTestStartup.
        /// </summary>
        /// <param name="configuration">Configuration for new instance.</param>
        public MasterLightLoadTestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IUnityContainer unityContainer = app.ApplicationServices.GetService(typeof(IUnityContainer)) as IUnityContainer;
            if (unityContainer == null)
            {
                throw new InvalidOperationException("Не удалось получить IUnityContainer из ApplicationServices");
            }

            unityContainer.RegisterInstance(env);

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMvc(builder =>
            {
                builder.MapRoute("Lock", "api/lock/{action}/{dataObjectId}", new { controller = "Lock" });
                builder.MapFileRoute();
            });

            app.UseODataService(builder =>
            {
                IUnityContainer container = unityContainer;

                var assemblies = new[]
                {
                    typeof(Котенок).Assembly,
                    typeof(ApplicationLog).Assembly,
                    typeof(UserSetting).Assembly,
                    typeof(Lock).Assembly,
                };

                PseudoDetailDefinitions pseudoDetailDefinitions = (PseudoDetailDefinitions)container.Resolve(typeof(PseudoDetailDefinitions));

                // Set MasterLightLoad property for this DataObject
                var masterLightLoadTypes = new List<Type> { typeof(Котенок) };
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, app.ApplicationServices, false, pseudoDetailDefinitions, masterLightLoadTypes: masterLightLoadTypes);

                var token = builder.MapDataObjectRoute(modelBuilder);

                container.RegisterInstance(typeof(ManagementToken), token);
            });
        }
    }
}
#endif