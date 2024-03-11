namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.UserDataTypes;
    using ICSSoft.STORMNET.Windows.Forms;

    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit;

    /// <summary>
    /// Класс тестов для проверки корректной обработки Get-запросов.
    /// </summary>
#if NETFRAMEWORK
    public class GetTest : BaseODataServiceIntegratedTest
#endif
#if NETCOREAPP
    public class GetTest : BaseODataServiceIntegratedTest<TestStartup>
#endif
    {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public GetTest(CustomWebApplicationFactory<TestStartup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Проверка получения данных для классов, в которых есть нехранимые поля, который не содержат setter'ов.
        /// (Такие варианты присутствуют в старом коде).
        /// </summary>
        [Fact]
        public void TestGetNotStored()
        {
            ActODataService(args =>
            {
                LegoBlock block = new LegoBlock { Name = "Легосити" };
                var objs = new DataObject[] { block };
                args.DataService.UpdateObjects(ref objs);

                string requestUrl = string.Format(
                    "http://localhost/odata/{0}?$select=__PrimaryKey,AssocType",
                    args.Token.Model.GetEdmEntitySet(typeof(LegoBlock)).Name);

                // Обращаемся к OData-сервису и обрабатываем ответ.
                using (HttpResponseMessage response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Убедимся, что запрос завершился успешно.
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    // Получим строку с ответом.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();

                    // Преобразуем полученный объект в словарь.
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);

                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                }
            });
        }

        /// <summary>
        /// Проверка значение в атрибуте @odata.type.
        /// </summary>
        [Fact]
        public void TestGetWithMaster()
        {
            ActODataService(args =>
            {
                LegoBlock block = new LegoBlock { Name = "Легосити" };
                LegoPatent patent = new LegoPatent { Name = "ZeroM", BaseLegoBlock = block, Date = DateTime.Now };
                var objs = new DataObject[] { block, patent };
                args.DataService.UpdateObjects(ref objs);

                string requestUrl = string.Format(
                    "http://localhost/odata/{0}?$select=__PrimaryKey,Name&$expand=BaseLegoBlock($select=__PrimaryKey,Name)",
                    args.Token.Model.GetEdmEntitySet(typeof(LegoPatent)).Name);

                // Обращаемся к OData-сервису и обрабатываем ответ.
                using (HttpResponseMessage response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Убедимся, что запрос завершился успешно.
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    // Получим строку с ответом.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();

                    // Преобразуем полученный объект в словарь.
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);

                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Contains("@odata.type", receivedStr);
                    Assert.DoesNotContain("____", receivedStr);
                }
            });
        }
    }
}
