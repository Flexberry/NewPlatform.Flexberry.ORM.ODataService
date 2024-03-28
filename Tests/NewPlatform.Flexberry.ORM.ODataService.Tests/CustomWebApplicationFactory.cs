#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Unity;
    using Unity.Microsoft.DependencyInjection;

    /// <summary>
    /// Custom web application factory for tests.
    /// </summary>
    /// <typeparam name="TStartup">Startup type.</typeparam>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        /// <summary>
        /// Unity container is created in only one place: <see cref="BaseIntegratedTest"/>.
        /// Further, it autimatically appears in a child <see cref="BaseODataServiceIntegratedTest"/>, where ODataService starts.
        /// The service is started in this class. Therefore, the Unity container must be passed to the running application here,
        /// being initialized earlier.
        /// </summary>
        public static IUnityContainer _unityContainer;

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
