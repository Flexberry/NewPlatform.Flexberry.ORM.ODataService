#if NETCORE
namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.TestHost;
    using ODataServiceSample.AspNetCore;

    public class TestingMvcFunctionalTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TestingMvcFunctionalTests(CustomWebApplicationFactory<Startup> factory)
        {
            //_factory = factory.WithWebHostBuilder(builder =>
            //{
            //    builder.UseWebRoot()
            //    //.UseSolutionRelativeContentRoot("MediaGallery");

            //    builder.ConfigureTestServices(services =>
            //    {
            //        services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            //    });

            //});

            _factory = factory;

        }

        [Fact]
        public async Task GetMetadataTest()
        {
            HttpClient client = _factory
            //    .WithWebHostBuilder(builder => {
            //    builder.UseSolutionRelativeContentRoot("/tests/NewPlatform.Flexberry.ORM.ODataService.Tests/");
            //})
                .CreateClient();

            // Arrange & Act
            var response = await client.GetAsync("/odata/$metadata");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(Skip = "Use common factory")]
        public async Task SelfHostedNetCoreTest()
        {
            HttpClient client = _factory.CreateClient();
            var responsePersons = await client.GetAsync("/odata/Persons");

            // Assert
            Assert.Equal(HttpStatusCode.OK, responsePersons.StatusCode);
        }

    }
}
#endif
