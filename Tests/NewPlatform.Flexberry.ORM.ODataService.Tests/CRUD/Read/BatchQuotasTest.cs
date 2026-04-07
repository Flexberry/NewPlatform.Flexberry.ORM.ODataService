namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using Xunit;

    /// <summary>
    /// Тесты квот batch-запросов для стандартного Startup.
    /// </summary>
    public class BatchDefaultQuotasTest : BaseODataServiceIntegratedTest
#if NETCOREAPP
        , IClassFixture<CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup>>
#endif
    {
#if NETCOREAPP
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchDefaultQuotasTest"/> class.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public BatchDefaultQuotasTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Проверяет квоты по умолчанию (1000/1000/10485760) и успешную обработку batch > 100 операций.
        /// </summary>
        [Fact]
        public void DefaultStartupShouldUseInternalQuotasAndAcceptBatchOverOneHundredOperations()
        {
            const int operationsInChangesetCount = 500;

            ActODataService(args =>
            {
                Assert.Equal(1000, args.Token.BatchHandler.MessageQuotas.MaxPartsPerBatch);
                Assert.Equal(1000, args.Token.BatchHandler.MessageQuotas.MaxOperationsPerChangeset);
                Assert.Equal(10485760, args.Token.BatchHandler.MessageQuotas.MaxReceivedMessageSize);

                string baseUrl = "http://localhost/odata";
                string countriesSetName = args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name;
                string[] changesets = Enumerable.Range(0, operationsInChangesetCount)
                    .Select(_ => CreateChangeset(
                        $"{baseUrl}/{countriesSetName}",
                        "{}",
                        new Страна()))
                    .ToArray();

                using (HttpRequestMessage request = CreateBatchRequest(baseUrl, changesets))
                using (HttpResponseMessage response = args.HttpClient.SendAsync(request).Result)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Проверяет, что batch-запрос с количеством операций больше 1000 отклоняется в стандартном Startup.
        /// </summary>
        [Fact]
        public void DefaultStartupShouldRejectBatchOverThousandOperationsPerChangeset()
        {
            const int operationsInChangesetCount = 1001;

            ActODataService(args =>
            {
                string baseUrl = "http://localhost/odata";
                string countriesSetName = args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name;
                string[] changesets = Enumerable.Range(0, operationsInChangesetCount)
                    .Select(_ => CreateChangeset(
                        $"{baseUrl}/{countriesSetName}",
                        "{}",
                        new Страна()))
                    .ToArray();

                using (HttpRequestMessage request = CreateBatchRequest(baseUrl, changesets))
                using (HttpResponseMessage response = args.HttpClient.SendAsync(request).Result)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    Assert.False(response.IsSuccessStatusCode);
                }
            });
        }

    }
}
