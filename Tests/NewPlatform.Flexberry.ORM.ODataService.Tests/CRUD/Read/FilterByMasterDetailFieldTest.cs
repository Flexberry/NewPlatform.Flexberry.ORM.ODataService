namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System.Collections.Generic;
    using System.Net;

    using ICSSoft.STORMNET;

    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit;

    /// <summary>
    /// Unit-test class for filtering data through OData service by master details fields.
    /// </summary>
    public class FilterByMasterDetailFieldTest : BaseODataServiceIntegratedTest
    {
        /// <summary>
        /// Tests filtering data by master field.
        /// </summary>
        [Fact]
        public void TestFilterByMasterDetailField()
        {
            ActODataService(args =>
            {
                // Arrange.
                Медведь медведь1 = new Медведь() { ПорядковыйНомер = 1 };
                Медведь медведь2 = new Медведь() { ПорядковыйНомер = 2 };

                Берлога берлога1 = new Берлога() { Наименование = "Берлога 1" };
                Берлога берлога2 = new Берлога() { Наименование = "Берлога 2" };
                Берлога берлога3 = new Берлога() { Наименование = "Берлога 3" };
                Берлога берлога4 = new Берлога() { Наименование = "Берлога 4" };

                медведь1.Берлога.AddRange(берлога1, берлога2);
                медведь2.Берлога.AddRange(берлога3, берлога4);

                Блоха блоха1 = new Блоха() { Кличка = "Блоха 1", МедведьОбитания = медведь1 };
                Блоха блоха2 = new Блоха() { Кличка = "Блоха 2", МедведьОбитания = медведь2 };
                Блоха блоха3 = new Блоха() { Кличка = "Блоха 3" };
                Блоха блоха4 = new Блоха() { Кличка = "Блоха 4", МедведьОбитания = медведь1 };

                DataObject[] newDataObjects = new DataObject[] { медведь1, медведь2, берлога1, берлога2, берлога3, берлога4, блоха1, блоха2, блоха3, блоха4 };

                args.DataService.UpdateObjects(ref newDataObjects);

                string requestUrl = "http://localhost/odata/Блохаs?$filter=МедведьОбитания/Берлога/any(f:f/Наименование eq 'Берлога 1')";
                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(2, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }
    }
}
