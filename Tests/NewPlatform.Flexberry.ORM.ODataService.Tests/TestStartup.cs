#if NETCORE
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Security;
    using ICSSoft.STORMNET.Windows.Forms;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Exceptions;
    using NewPlatform.Flexberry.Services;
    using ODataServiceSample.AspNetCore;
    using Unity;

    using LockService = NewPlatform.Flexberry.Services.LockService;

    /// <summary>
    /// Startup for tests.
    /// </summary>
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base (configuration)
        {
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IUnityContainer unityContainer = UnityFactory.GetContainer();
            unityContainer.RegisterInstance(env);

            app.UseMiddleware<ExceptionMiddleware>();
            base.Configure(app, env);
        }

        //public override void ConfigureServices(IServiceCollection services)
        //{
        //    {
        //        IUnityContainer unityContainer = UnityFactory.GetContainer();

        //        IDataService dataService = new PostgresDataService() { CustomizationString = CustomizationString };

        //        unityContainer.RegisterInstance(dataService);
        //        ExternalLangDef.LanguageDef.DataService = dataService;

        //        unityContainer.RegisterInstance<ILockService>(new LockService(dataService));

        //        unityContainer.RegisterInstance<ISecurityManager>(new EmptySecurityManager());
        //    }

        //    services.AddMvcCore(options =>
        //    {
        //        options.Filters.Add<CustomExceptionFilter>();
        //    })
        //        .AddFormatterMappings()
        //        .AddJsonFormatters();

        //    services.AddOData();
        //}
    }
}
#endif
