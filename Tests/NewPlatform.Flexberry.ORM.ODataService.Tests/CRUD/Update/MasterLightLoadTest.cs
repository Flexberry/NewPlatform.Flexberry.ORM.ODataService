#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Update
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using ICSSoft.STORMNET.KeyGen;
    using NewPlatform.Flexberry.ORM.ODataService.Batch;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers;

    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Тесты для проверки работы MasterLightLoad. Для запуска OData backend используется модифицированная версия Startup - <see cref="MasterLightLoadTestStartup"/>,
    /// которая задаёт флаг экономной загрузки мастеров для <see cref="Котенок"/>.
    /// </summary>
    public class MasterLightLoadTest : BaseODataServiceIntegratedTest<MasterLightLoadTestStartup>
    {
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод диагностической информации по тестам.</param>
        public MasterLightLoadTest(CustomWebApplicationFactory<MasterLightLoadTestStartup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        /// <summary>
        /// Проверка экономной загрузки мастера при активной настройке MasterLightLoad при смене мастера.
        /// </summary>
        [Fact]
        public void MasterChangedTest()
        {
            ActODataService(args =>
            {
                // Создаем объекты данных, которые потом будем обновлять, и добавляем в базу обычным сервисом данных.
                Порода порода = new Порода { Название = "Сиамская" };
                Кошка кошка1 = new Кошка { Кличка = "Болтушка", Агрессивная = true, Порода = порода };
                Кошка кошка2 = new Кошка { Кличка = "Петрушка", Агрессивная = false, Порода = порода };
                args.DataService.UpdateObject(порода);
                args.DataService.UpdateObject(кошка1);
                args.DataService.UpdateObject(кошка2);

                Котенок котенок = new Котенок { Кошка = кошка1, КличкаКотенка = "Котенок Гав", Глупость = 10 };
                args.DataService.UpdateObject(котенок);

                // Обновляем ссылку на мастера.
                котенок.Кошка = кошка2;

                // Представление, по которому будем обновлять.
                string[] котенокPropertiesNames =
                {
                    Information.ExtractPropertyPath<Котенок>(x => x.__PrimaryKey),
                };
                var котенокDynamicView = new View(new ViewAttribute("котенокDynamicView", котенокPropertiesNames), typeof(Котенок));

                // Преобразуем объект в JSON-строку.
                string requestJsonData = котенок.ToJson(котенокDynamicView, args.Token.Model);

                // Добавляем в payload информацию о том, что поменяли ссылку на мастера.
                requestJsonData = ODataTestHelper.AddEntryRelationship(requestJsonData, котенокDynamicView, args.Token.Model, кошка2, nameof(Котенок.Кошка));

                // Формируем URL запроса к OData-сервису (с идентификатором изменяемой сущности).
                var requestUrl = ODataTestHelper.GetRequestUrl(args.Token.Model, котенок);

                using (HttpResponseMessage response = args.HttpClient.PatchAsJsonStringAsync(requestUrl, requestJsonData).Result)
                {
                    // Если приходит код 200, значит, настройка не ломает загрузку.
                    // Фактическую проверку того, что кошка загрузилась в LightLoaded надо делать через отладчик.
                    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                    // TODO: проверка на экономную загрузку мастера.
                }
            });
        }

        /// <summary>
        /// Проверка экономной загрузки мастера при активной настройке MasterLightLoad при смене значения поля у мастера.
        /// </summary>
        [Fact]
        public void MasterPropsChangedBatchTest()
        {
            ActODataService(async args =>
            {
                // Создаем объекты данных, которые потом будем обновлять, и добавляем в базу обычным сервисом данных.
                Порода порода = new Порода { Название = "Сиамская" };
                Кошка кошка = new Кошка { Кличка = "Болтушка", Агрессивная = true, Порода = порода };
                args.DataService.UpdateObject(порода);
                args.DataService.UpdateObject(кошка);

                Котенок котенок = new Котенок { Кошка = кошка, КличкаКотенка = "Котенок Гав", Глупость = 10 };
                args.DataService.UpdateObject(котенок);

                // Обновляем атрибут объекта.
                котенок.Глупость = 1;

                // Обновляем атрибут мастера
                котенок.Кошка.Кличка = "Петрушка";

                // Представление, по которому будем обновлять объект.
                string[] котенокPropertiesNames =
                {
                    Information.ExtractPropertyPath<Котенок>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Котенок>(x => x.Глупость),
                };
                var котенокDynamicView = new View(new ViewAttribute("котенокDynamicView", котенокPropertiesNames), typeof(Котенок));

                // Представление, по которому будем обновлять мастер.
                string[] кошкаPropertiesNames =
                {
                    Information.ExtractPropertyPath<Кошка>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Кошка>(x => x.Кличка),
                };
                var кошкаDynamicView = new View(new ViewAttribute("кошкаDynamicView", кошкаPropertiesNames), typeof(Кошка));

                // Преобразуем объект в JSON-строку.
                string котенокJsonData = котенок.ToJson(котенокDynamicView, args.Token.Model);

                // Добавляем в payload информацию о ссылке на мастера.
                котенокJsonData = ODataTestHelper.AddEntryRelationship(котенокJsonData, котенокDynamicView, args.Token.Model, котенок.Кошка, nameof(Котенок.Кошка));

                const string baseUrl = "http://localhost/odata";
                string[] changesets = new[] // Важно, чтобы сначала шёл мастер, потом объект, имеющий на него ссылку.
                {
                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Кошка)).Name}",
                        кошка.ToJson(кошкаDynamicView, args.Token.Model),
                        кошка),
                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Котенок)).Name}",
                        котенокJsonData,
                        котенок),
                };

                // Act.
                HttpRequestMessage batchRequest = CreateBatchRequest(baseUrl, changesets);
                using (HttpResponseMessage response = args.HttpClient.SendAsync(batchRequest).Result)
                {
                    // Assert.
                    CheckODataBatchResponseStatusCode(response, new HttpStatusCode[] { HttpStatusCode.OK, HttpStatusCode.OK });
                    Котенок котенокLoaded = args.DataService.Query<Котенок>(котенокDynamicView).FirstOrDefault(x => x.__PrimaryKey == котенок.__PrimaryKey);
                    Кошка кошкаLoaded = args.DataService.Query<Кошка>(кошкаDynamicView).FirstOrDefault(x => x.__PrimaryKey == кошка.__PrimaryKey);
                    Assert.NotNull(котенокLoaded);
                    Assert.NotNull(кошкаLoaded);
                    Assert.Equal(1, котенокLoaded.Глупость);
                    Assert.Equal("Петрушка", кошкаLoaded.Кличка);

                    // TODO: проверка на экономную загрузку мастера.
                }
            });
        }

        /// <summary>
        /// Проверка экономной загрузки мастера при активной настройке MasterLightLoad при смене значения поля у мастера.
        /// </summary>
        [Fact]
        public void MasterChangedBatchTest()
        {
            ActODataService(async args =>
            {
                // Создаем объекты данных, которые потом будем обновлять, и добавляем в базу обычным сервисом данных.
                Порода порода = new Порода { Название = "Сиамская" };
                Кошка кошка1 = new Кошка { Кличка = "Болтушка", Агрессивная = true, Порода = порода };
                Кошка кошка2 = new Кошка { Кличка = "Петрушка", Агрессивная = false, Порода = порода };
                args.DataService.UpdateObject(порода);
                args.DataService.UpdateObject(кошка1);
                args.DataService.UpdateObject(кошка2);

                Котенок котенок = new Котенок { Кошка = кошка1, КличкаКотенка = "Котенок Гав", Глупость = 10 };
                args.DataService.UpdateObject(котенок);

                // Обновляем атрибут объекта.
                котенок.Глупость = 1;

                // Обновляем мастера
                котенок.Кошка = кошка2;

                // Представление, по которому будем обновлять объект.
                string[] котенокPropertiesNames =
                {
                    Information.ExtractPropertyPath<Котенок>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Котенок>(x => x.Глупость),
                };
                var котенокDynamicView = new View(new ViewAttribute("котенокDynamicView", котенокPropertiesNames), typeof(Котенок));

                // Преобразуем объект в JSON-строку.
                string котенокJsonData = котенок.ToJson(котенокDynamicView, args.Token.Model);

                // Добавляем в payload информацию о ссылке на мастера
                котенокJsonData = ODataTestHelper.AddEntryRelationship(котенокJsonData, котенокDynamicView, args.Token.Model, котенок.Кошка, nameof(Котенок.Кошка));

                const string baseUrl = "http://localhost/odata";
                string[] changesets = new[] // Важно, чтобы сначала шёл мастер, потом объект, имеющий на него ссылку.
                {
                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Котенок)).Name}",
                        котенокJsonData,
                        котенок),
                };

                // Act.
                HttpRequestMessage batchRequest = CreateBatchRequest(baseUrl, changesets);
                using (HttpResponseMessage response = args.HttpClient.SendAsync(batchRequest).Result)
                {
                    // Assert.
                    CheckODataBatchResponseStatusCode(response, new HttpStatusCode[] { HttpStatusCode.OK });

                    string[] котенокPropertiesNamesMaster =
                    {
                        Information.ExtractPropertyPath<Котенок>(x => x.__PrimaryKey),
                        Information.ExtractPropertyPath<Котенок>(x => x.Глупость),
                        Information.ExtractPropertyPath<Котенок>(x => x.Кошка),
                    };
                    var котенокDynamicViewMaster = new View(new ViewAttribute("котенокDynamicView", котенокPropertiesNamesMaster), typeof(Котенок));
                    Котенок котенокLoaded = args.DataService.Query<Котенок>(котенокDynamicViewMaster).FirstOrDefault(x => x.Кошка.__PrimaryKey == кошка2.__PrimaryKey);
                    Assert.NotNull(котенокLoaded);
                    Assert.Equal(кошка2.__PrimaryKey, котенокLoaded.Кошка.__PrimaryKey);
                    Assert.Equal(1, котенокLoaded.Глупость);

                    // TODO: проверка на экономную загрузку мастера.
                }
            });
        }
    }
}
#endif
