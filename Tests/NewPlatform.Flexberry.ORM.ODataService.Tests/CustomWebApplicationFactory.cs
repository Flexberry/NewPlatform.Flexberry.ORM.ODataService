#if NETCORE
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;

    /// <summary>
    /// Custom web application factory for tests.
    /// </summary>
    /// <typeparam name="TStartup">Startup type.</typeparam>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        /// <inheritdoc/>
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            string contentRootDirectory = Directory.GetCurrentDirectory();
            var webHostBuilder = new WebHostBuilder()
                            .UseContentRoot(contentRootDirectory)
                            .UseStartup<TestStartup>();
            return webHostBuilder;
        }
    }
}
#endif
