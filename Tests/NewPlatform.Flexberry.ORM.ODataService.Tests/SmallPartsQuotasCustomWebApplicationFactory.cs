#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System.IO;
    using ICSSoft.Services;
    using Microsoft.AspNetCore.Hosting;
    using ODataServiceSample.AspNetCore;
    using Unity.Microsoft.DependencyInjection;

    /// <summary>
    /// Custom web application factory for small max-parts quota tests.
    /// </summary>
    public class SmallPartsQuotasCustomWebApplicationFactory : CustomWebApplicationFactory<Startup>
    {
        /// <inheritdoc/>
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            string contentRootDirectory = Directory.GetCurrentDirectory();
            var container = UnityFactory.GetContainer();

            return new WebHostBuilder()
                .UseUnityServiceProvider(container)
                .UseContentRoot(contentRootDirectory)
                .UseStartup<SmallPartsQuotasTestStartup>();
        }
    }
}
#endif
