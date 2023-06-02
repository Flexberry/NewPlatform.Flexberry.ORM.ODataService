#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.IO;
    using ICSSoft.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Unity.Microsoft.DependencyInjection;

    /// <summary>
    /// Custom web application factory for tests.
    /// </summary>
    /// <typeparam name="TStartup">Startup type.</typeparam>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public static Unity.IUnityContainer _unityContainer;

        /// <inheritdoc/>
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            if (_unityContainer == null)
            {
                throw new Exception("Unity.IUnityContainer is not defined");
            }

            string contentRootDirectory = Directory.GetCurrentDirectory();
            var webHostBuilder = new WebHostBuilder()
                            .UseUnityServiceProvider(_unityContainer)
                            .UseContentRoot(contentRootDirectory)
                            .UseStartup<TestStartup>();
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
