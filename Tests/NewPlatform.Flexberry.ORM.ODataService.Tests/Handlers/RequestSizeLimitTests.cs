#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Handlers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers;
    using Xunit;
    using Xunit.Abstractions;

    public class RequestSizeLimitTests : BaseODataServiceIntegratedTest<TestStartup>
    {
        public RequestSizeLimitTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public void CheckMaxQueryLenTest()
        {
            ActODataService(async args =>
            {
                const long maxLength = 10 * 1024 * 1024; // 10 МБ
                var tooLargeBody = new string('a', (int)(maxLength + 1024));

                var request = new HttpRequestMessage(HttpMethod.Post, "odata/Медведь")
                {
                    Content = new StringContent(tooLargeBody),
                };

                request.Content.Headers.ContentLength = tooLargeBody.Length;

                HttpResponseMessage message = await args.HttpClient.SendAsync(request);

                Assert.Equal((HttpStatusCode)413, message.StatusCode);
            });
        }
    }
}
#else //NETFRAMEWORK
public class RequestSizeLimitTests
{
    
}
#endif