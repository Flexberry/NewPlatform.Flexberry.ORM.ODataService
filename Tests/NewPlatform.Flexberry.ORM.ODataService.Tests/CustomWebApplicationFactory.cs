#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using ODataServiceSample.AspNetCore;
    using Unity;
    using Unity.Microsoft.DependencyInjection;

    /// <summary>
    /// Custom web application factory for tests.
    /// </summary>
    /// <typeparam name="TStartup">Startup type.</typeparam>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : Startup
    {
        /// <inheritdoc/>
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            string contentRootDirectory = Directory.GetCurrentDirectory();
            var webHostBuilder = new WebHostBuilder()
                            .UseUnityServiceProvider()
                            .UseContentRoot(contentRootDirectory)
                            .UseStartup<TStartup>();
            return webHostBuilder;
        }

        /// <inheritdoc/>
        /// <remarks>https://github.com/dotnet/AspNetCore.Docs/issues/7063 .</remarks>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
        }
    }
}
#endif
