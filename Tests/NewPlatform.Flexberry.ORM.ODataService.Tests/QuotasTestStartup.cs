#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
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
    /// Startup for batch quotas tests.
    /// </summary>
    public class QuotasTestStartup : Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuotasTestStartup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration for new instance.</param>
        public QuotasTestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IUnityContainer unityContainer = UnityFactory.GetContainer();
            unityContainer.RegisterInstance(env);

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMvc(builder =>
            {
                builder.MapRoute("Lock", "api/lock/{action}/{dataObjectId}", new { controller = "Lock" });
                builder.MapFileRoute();
            });

            app.UseODataService(builder =>
            {
                IUnityContainer container = UnityFactory.GetContainer();

                var assemblies = new[]
                {
                    typeof(Медведь).Assembly,
                    typeof(ApplicationLog).Assembly,
                    typeof(UserSetting).Assembly,
                    typeof(Lock).Assembly,
                };

                PseudoDetailDefinitions pseudoDetailDefinitions = (PseudoDetailDefinitions)container.Resolve(typeof(PseudoDetailDefinitions));
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, false, pseudoDetailDefinitions);

                var token = builder.MapDataObjectRoute(
                    modelBuilder,
                    messageQuotasMaxPartsPerBatch: 2000,
                    messageQuotasMaxOperationsPerChangeset: 2000,
                    messageQuotasMaxReceivedMessageSize: 10485760 * 2);

                container.RegisterInstance(typeof(ManagementToken), token);
            });
        }
    }
}
#endif
