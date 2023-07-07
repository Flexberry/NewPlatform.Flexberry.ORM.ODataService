namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System;
    using System.Net;
    using System.Net.Http;

    using ICSSoft.STORMNET;

    using Xunit;

    /// <summary>
    /// Unit-test class for reading a non-existent data through OData service.
    /// </summary>
    public class GetByIdTest : BaseODataServiceIntegratedTest
    {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public GetByIdTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Tests that a request with a non-existent key returns a 404 error.
        /// </summary>
        [Fact]
        public void TestGetById()
        {
            ActODataService(args =>
            {
                string requestUrl = string.Format("http://localhost/odata/{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name, "8dcd3aa3-11c2-456d-902c-03323e1ae635");

                using (HttpResponseMessage response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                }
            });
        }
    }
}
