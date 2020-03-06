namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Windows.Forms;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit;

    /// <summary>
    /// Unit-test class for read inherited classes.
    /// </summary>
    public class InheritanceTest: BaseODataServiceIntegratedTest
    {
        /// <summary>
        /// Testing reading a parent class without copies of child classes.
        /// </summary>
        [Fact]
        public void TestReadParentClass()
        {
            ActODataService(args =>
            {
                ExternalLangDef.LanguageDef.DataService = args.DataService;

                DateTime date = new DateTimeOffset(DateTime.Now).UtcDateTime;
                БазовыйКласс класс = new БазовыйКласс() { Свойство1 = "Базовый" };
                Наследник наследник = new Наследник() { Свойство1 = "Наследник" };
                var objs = new DataObject[] { класс, наследник };
                args.DataService.UpdateObjects(ref objs);

                string requestUrl;

                // Проверка использования в фильтрации функции any.
                requestUrl = string.Format(
                    "http://localhost/odata/{0}?$expand={1}",
                    args.Token.Model.GetEdmEntitySet(typeof(БазовыйКласс)).Name,
                    "Свойство1");

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });

        }
    }
}
