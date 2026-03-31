using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{

    /// <summary>
    /// Тесты квот batch-запросов для кастомного QuotasStartup.
    /// </summary>
    public class BatchOverrideQuotasTest : BaseODataServiceIntegratedTest
#if NETCOREAPP
        , IClassFixture<QuotasCustomWebApplicationFactory>
#endif
    {
#if NETCOREAPP
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchOverrideQuotasTest"/> class.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public BatchOverrideQuotasTest(QuotasCustomWebApplicationFactory factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Проверяет применение кастомных квот и успешную обработку batch между 1000 и 2000 операций.
        /// </summary>
        [Fact]
        public void QuotasStartupShouldApplyOverrideAndAcceptBatchUpToTwoThousandOperations()
        {
            const int operationsInChangesetCount = 1500;

            ActODataService(args =>
            {
                Assert.Equal(2000, args.Token.BatchHandler.MessageQuotas.MaxPartsPerBatch);
                Assert.Equal(2000, args.Token.BatchHandler.MessageQuotas.MaxOperationsPerChangeset);
                Assert.Equal(10485760 * 2, args.Token.BatchHandler.MessageQuotas.MaxReceivedMessageSize);

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
        /// Проверяет, что batch-запрос с количеством операций больше 2000 отклоняется в QuotasStartup.
        /// </summary>
        [Fact]
        public void QuotasStartupShouldRejectBatchOverTwoThousandOperationsPerChangeset()
        {
            const int operationsInChangesetCount = 2001;

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
