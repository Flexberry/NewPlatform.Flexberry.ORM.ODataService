#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System.IO;
    using ICSSoft.Services;
    using Microsoft.AspNetCore.Hosting;
    using ODataServiceSample.AspNetCore;
    using Unity.Microsoft.DependencyInjection;

    /// <summary>
    /// Custom web application factory for batch quotas tests.
    /// </summary>
    public class QuotasCustomWebApplicationFactory : CustomWebApplicationFactory<Startup>
    {
        /// <inheritdoc/>
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            string contentRootDirectory = Directory.GetCurrentDirectory();
            var container = UnityFactory.GetContainer();

            return new WebHostBuilder()
                .UseUnityServiceProvider(container)
                .UseContentRoot(contentRootDirectory)
                .UseStartup<QuotasTestStartup>();
        }
    }
}
#endif
