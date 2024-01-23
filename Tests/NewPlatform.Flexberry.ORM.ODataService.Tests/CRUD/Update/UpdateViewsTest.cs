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
    /// Тесты для проверки работы UpdateViews.
    /// </summary>
    public class UpdateViewsTest : BaseODataServiceIntegratedTest<UpdateViewsTestStartup>
    {
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод диагностической информации по тестам.</param>
        public UpdateViewsTest(CustomWebApplicationFactory<UpdateViewsTestStartup> factory, ITestOutputHelper output)
            : base(factory, output)
        {

        }

        /// <summary>
        /// Проверка работы UpdateView - случай когда в UpdateView не включен мастер.
        /// </summary>
        [Fact]
        public void UpdateViewNoMastersTest()
        {
            ActODataService(args =>
            {
                // Создаем объекты данных, которые потом будем обновлять, и добавляем в базу обычным сервисом данных.
                Медведь медведь1 = new Медведь { ПорядковыйНомер = 1 };
                Медведь медведь2 = new Медведь { ПорядковыйНомер = 2 };
                args.DataService.UpdateObject(медведь1);
                args.DataService.UpdateObject(медведь2);

                Берлога берлога1 = new Берлога { Наименование = "Берлога1", Медведь = медведь1 };
                Берлога берлога2 = new Берлога { Наименование = "Берлога2", Медведь = медведь1 };
                Берлога берлога3 = new Берлога { Наименование = "Берлога3", Медведь = медведь1 };
                args.DataService.UpdateObject(берлога1);
                args.DataService.UpdateObject(берлога2);
                args.DataService.UpdateObject(берлога3);

                // Обновляем ссылку.
                берлога1.Медведь = медведь2;

                // Представление, по которому будем обновлять.
                string[] берлогаPropertiesNames =
                {
                    Information.ExtractPropertyPath<Берлога>(x => x.__PrimaryKey),
                };
                var берлогаDynamicView = new View(new ViewAttribute("берлогаDynamicView", берлогаPropertiesNames), typeof(Берлога));

                // Преобразуем объект данных в JSON-строку.
                string requestJsonData = берлога1.ToJson(берлогаDynamicView, args.Token.Model);

                // Добавляем в payload информацию, что поменяли ссылку на медведя.
                requestJsonData = ODataHelper.AddEntryRelationship(requestJsonData, берлогаDynamicView, args.Token.Model, медведь2, nameof(Берлога.Медведь));

                // Формируем URL запроса к OData-сервису (с идентификатором изменяемой сущности).
                var requestUrl = ODataHelper.GetRequestUrl(args.Token.Model, берлога1);

                // Сейчас обновление мастеров не поддерживается.
                Assert.ThrowsAsync<Exception>(() => args.HttpClient.PatchAsJsonStringAsync(requestUrl, requestJsonData)); // Если падает Exception, значит представление поменялось и работает.
            });
        }
    }
}
#endif
