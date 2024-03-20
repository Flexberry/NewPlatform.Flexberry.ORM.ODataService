#if NETCOREAPP
namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Update
{
    using System;
    using System.Net;
    using System.Net.Http;
    using ICSSoft.STORMNET;
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
        public void MasterLightLoadSettingTest()
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

                // Обновляем ссылку на мастера
                котенок.Кошка = кошка2;

                // Представление, по которому будем обновлять.
                string[] котенокPropertiesNames =
                {
                    Information.ExtractPropertyPath<Котенок>(x => x.__PrimaryKey),
                };
                var котенокDynamicView = new View(new ViewAttribute("котенокDynamicView", котенокPropertiesNames), typeof(Котенок));

                // Преобразуем объект в JSON-строку.
                string requestJsonData = котенок.ToJson(котенокDynamicView, args.Token.Model);

                // Добавляем в payload информацию о том, что поменяли ссылку на мастера
                requestJsonData = ODataTestHelper.AddEntryRelationship(requestJsonData, котенокDynamicView, args.Token.Model, кошка2, nameof(Котенок.Кошка));

                // Формируем URL запроса к OData-сервису (с идентификатором изменяемой сущности).
                var requestUrl = ODataTestHelper.GetRequestUrl(args.Token.Model, котенок);

                using (HttpResponseMessage response = args.HttpClient.PatchAsJsonStringAsync(requestUrl, requestJsonData).Result)
                {
                    // Если приходит код 200, значит, настройка не ломает загрузку. Фактическую проверку того, что кошка загрузилась в LightLoaded надо делать через отладчик.
                    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                }
            });
        }
    }
}
#endif
