namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Read
{
    using System.Collections.Generic;
    using System.Net;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.KeyGen;
    using ICSSoft.STORMNET.Windows.Forms;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit;

    /// <summary>
    /// Unit-test class for filtering data through OData service by master master details fields.
    /// </summary>
    public class FilterByMasterMasterDetailFieldTest : BaseODataServiceIntegratedTest
    {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public FilterByMasterMasterDetailFieldTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Tests filtering data by master field.
        /// </summary>
        [Fact]
        public void TestFilterByMasterMasterDetailField()
        {
            ActODataService(args =>
            {
                // Arrange.
                DetailsClass2 detailsClass2 = new DetailsClass2() { DetailCl2Name = "DetailsClass2" };
                DetailsClass1 detailsClass1 = new DetailsClass1() { DetailCl1Name = "DetailsClass1", DetailsClass2 = detailsClass2 };

                AgrClass1 agrClass1 = new AgrClass1() { AgrCl1Name = "AgrCl1Name" };
                AgrClass2 agrClass2 = new AgrClass2() { AgrCl2Name = "AgrCl2Name" };

                agrClass1.DetailsClass1.Add(detailsClass1);
                agrClass2.DetailsClass2.Add(detailsClass2);

                MainClass mainClass = new MainClass() { AgrClass1 = agrClass1 };

                DataObject[] newDataObjects = new DataObject[] { detailsClass2, detailsClass1, agrClass1, agrClass2, mainClass };

                args.DataService.UpdateObjects(ref newDataObjects);
                ExternalLangDef.LanguageDef.DataService = args.DataService;

                string agrClass2Pk = ((KeyGuid)agrClass2.__PrimaryKey).Guid.ToString("D");

                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}",
                args.Token.Model.GetEdmEntitySet(typeof(MainClass)).Name,
                "AgrClass1/DetailsClass1/any(f:f/DetailsClass2/AgrClass2/__PrimaryKey eq " + agrClass2Pk + ")");

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
