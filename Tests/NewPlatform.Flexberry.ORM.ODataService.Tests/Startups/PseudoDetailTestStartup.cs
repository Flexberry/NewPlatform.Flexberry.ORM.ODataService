#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using ICSSoft.STORMNET;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NewPlatform.Flexberry.ORM.ODataService.Model;

    /// <summary>
    /// Startup for testing PseudoDetails.
    /// Differs from TestStartup that it registers sample <see cref="PseudoDetailDefinitions"/>.
    /// </summary>
    public class PseudoDetailTestStartup : TestStartup
    {
        /// <summary>
        /// Initialize new instance of TestStartup.
        /// </summary>
        /// <param name="configuration">Configuration for new instance.</param>
        public PseudoDetailTestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <summary>
        /// Хук для конфигурации (регистрации) сервисов.
        /// </summary>
        /// <param name="services">Сервисная коллекция.</param>
        public override void ConfigureServices(IServiceCollection services)
        {
            var pseudoDetailDefinitions = new PseudoDetailDefinitions
            {
                new DefaultPseudoDetailDefinition<Медведь, Блоха>(
                    Блоха.Views.PseudoDetailView,
                    Information.ExtractPropertyPath<Блоха>(x => x.МедведьОбитания),
                    "Блохи"),
            };
            services.AddSingleton(pseudoDetailDefinitions);
            base.ConfigureServices(services);
        }
    }
}
#endif
