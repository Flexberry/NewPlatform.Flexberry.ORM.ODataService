namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using Xunit;

    /// <summary>
    /// Проверки квоты MaxPartsPerBatch на top-level parts.
    /// </summary>
    public class BatchTopLevelPartsQuotaTest : BaseODataServiceIntegratedTest
#if NETCOREAPP
        , IClassFixture<SmallPartsQuotasCustomWebApplicationFactory>
#endif
    {
#if NETCOREAPP
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchTopLevelPartsQuotaTest"/> class.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public BatchTopLevelPartsQuotaTest(SmallPartsQuotasCustomWebApplicationFactory factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Проверяет, что batch с количеством top-level parts в пределах квоты обрабатывается успешно.
        /// </summary>
        [Fact]
        public void SmallPartsQuotaStartupShouldAcceptBatchAtLimit()
        {
            const int topLevelPartsCount = 5;

            ActODataService(args =>
            {
                Assert.Equal(5, args.Token.BatchHandler.MessageQuotas.MaxPartsPerBatch);

                string baseUrl = "http://localhost/odata";
                string countriesSetName = args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name;
                string[] changesets = Enumerable.Range(0, topLevelPartsCount)
                    .Select(_ => CreateChangeset(
                        $"{baseUrl}/{countriesSetName}",
                        "{}",
                        new Страна()))
                    .ToArray();

                using (HttpRequestMessage request = CreateBatchRequestWithMultipleTopLevelChangesets(baseUrl, changesets))
                using (HttpResponseMessage response = args.HttpClient.SendAsync(request).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Проверяет, что batch с превышением top-level parts отклоняется.
        /// </summary>
        [Fact]
        public void SmallPartsQuotaStartupShouldRejectBatchOverLimit()
        {
            const int topLevelPartsCount = 6;

            ActODataService(args =>
            {
                string baseUrl = "http://localhost/odata";
                string countriesSetName = args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name;
                string[] changesets = Enumerable.Range(0, topLevelPartsCount)
                    .Select(_ => CreateChangeset(
                        $"{baseUrl}/{countriesSetName}",
                        "{}",
                        new Страна()))
                    .ToArray();

                using (HttpRequestMessage request = CreateBatchRequestWithMultipleTopLevelChangesets(baseUrl, changesets))
                using (HttpResponseMessage response = args.HttpClient.SendAsync(request).Result)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    Assert.False(response.IsSuccessStatusCode);
                }
            });
        }
    }
}
