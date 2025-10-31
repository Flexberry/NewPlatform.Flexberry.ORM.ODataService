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

    /// <summary>
    /// Класс для проверки ограничения размеров запроса.
    /// Реализация под NETCOREAPP через <see cref="RequestSizeLimitMiddleware"/>.
    /// </summary>
    public class RequestSizeLimitTests : BaseODataServiceIntegratedTest<TestStartup>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public RequestSizeLimitTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        /// <summary>
        /// Проверяет, что middleware корректно отрабатывает и возвращает ошибку 413,
        /// если запрос превышает максимально допустимый размер.
        /// </summary>
        [Fact]
        public void CheckMaxQueryLenTest()
        {
            ActODataService(async args =>
            {
                const long oversizedLength = NewPlatform.Flexberry.ORM.ODataService.Handlers.RequestSizeLimitMiddleware.MaxRequestSize + 1024;
                var tooLargeBody = new string('a', (int)oversizedLength);

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