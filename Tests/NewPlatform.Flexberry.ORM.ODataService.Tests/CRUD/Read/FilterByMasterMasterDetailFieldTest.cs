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
#if NETFRAMEWORK
    public class FilterByMasterMasterDetailFieldTest : BaseODataServiceIntegratedTest
#endif
#if NETCOREAPP
    public class FilterByMasterMasterDetailFieldTest : BaseODataServiceIntegratedTest<TestStartup>
#endif
    {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public FilterByMasterMasterDetailFieldTest(CustomWebApplicationFactory<TestStartup> factory, Xunit.Abstractions.ITestOutputHelper output)
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

                string agrClass2Pk = ((KeyGuid)agrClass2.__PrimaryKey).Guid.ToString("D");

                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}",
                args.Token.Model.GetEdmEntitySet(typeof(MainClass)).Name,
                "AgrClass1/DetailsClass1/any(f:f/DetailsClass2/AgrClass2/__PrimaryKey eq " + agrClass2Pk + ")");

                // Act.
                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Tests filtering data by masters field in details.
        /// </summary>
        [Fact]
        public void TestFilterByDetailTwinMasterFields()
        {
            ActODataService(args =>
            {
                // Arrange.
                Порода breed = new Порода() { Название = "Бурый" };
                Лес forest = new Лес() { Название = "Тёмный" };
                Берлога den = new Берлога() { Наименование = "Под ёлкой", ПодходитДляПороды = breed, ЛесРасположения = forest };

                Медведь bear = new Медведь() { ПорядковыйНомер = 1 };
                bear.Берлога.Add(den);

                DataObject[] newDataObjects = new DataObject[] { breed, forest, den, bear };

                args.DataService.UpdateObjects(ref newDataObjects);
                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}",
                args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name,
                "Берлога/any(f:(contains(f/ЛесРасположения/Название,'Тёмный') and contains(f/ПодходитДляПороды/Название,'Бурый')))");

                // Act.
                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Tests filtering data by detail and select.
        /// </summary>
        [Fact]
        public void TestFilterByDetailAndSelect()
        {
            ActODataService(args =>
            {
                // Arrange.
                Порода breed = new Порода() { Название = "Бурый" };
                Лес forest = new Лес() { Название = "Тёмный" };
                string forestPkString = ((KeyGuid)forest.__PrimaryKey).Guid.ToString("D");
                Берлога den = new Берлога() { Наименование = "Под ёлкой", ПодходитДляПороды = breed, ЛесРасположения = forest };

                Медведь bear = new Медведь() { ПорядковыйНомер = 1 };
                bear.Берлога.Add(den);

                DataObject[] newDataObjects = new DataObject[] { breed, forest, den, bear };

                args.DataService.UpdateObjects(ref newDataObjects);
                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}&$select={2}",
                args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name,
                "Берлога/any(f:f/ЛесРасположения/__PrimaryKey%20eq%20" + forestPkString + ")",
                "__PrimaryKey,ПорядковыйНомер");

                // Act.
                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Tests complex filtering data by detail and select.
        /// </summary>
        [Fact]
        public void TestComplexFilterByDetailAndSelect()
        {
            ActODataService(args =>
            {
                // Arrange.
                Порода breed = new Порода() { Название = "Бурый" };
                Лес forest = new Лес() { Название = "Тёмный" };
                string forestPkString = ((KeyGuid)forest.__PrimaryKey).Guid.ToString("D");
                Берлога den = new Берлога() { Наименование = "Под ёлкой", ПодходитДляПороды = breed, ЛесРасположения = forest };

                Медведь bear = new Медведь() { ПорядковыйНомер = 1 };
                bear.Берлога.Add(den);

                DataObject[] newDataObjects = new DataObject[] { breed, forest, den, bear };

                args.DataService.UpdateObjects(ref newDataObjects);

                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}&$select={2}",
                args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name,
                "Берлога/any(f:(f/ЛесРасположения/__PrimaryKey%20eq%20" + forestPkString + " and contains(f/ПодходитДляПороды/Название,'Бурый')))",
                "__PrimaryKey,ПорядковыйНомер");

                // Act.
                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Tests filtering details by master master.
        /// </summary>
        [Fact]
        public void TestFilterByDetailMasterMaster()
        {
            ActODataService(args =>
            {
                // Arrange.
                Порода breed = new Порода() { Название = "Бурый" };
                Страна country = new Страна() { Название = "Гана" };
                string countryPkString = ((KeyGuid)country.__PrimaryKey).Guid.ToString("D");
                Лес forest = new Лес() { Название = "Тёмный", Страна = country };
                Берлога den = new Берлога() { Наименование = "Под ёлкой", ПодходитДляПороды = breed, ЛесРасположения = forest };

                Медведь bear = new Медведь() { ПорядковыйНомер = 1 };
                bear.Берлога.Add(den);

                DataObject[] newDataObjects = new DataObject[] { breed, forest, den, bear };

                args.DataService.UpdateObjects(ref newDataObjects);

                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}&$select={2}",
                args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name,
                "Берлога/any(f:f/ЛесРасположения/Страна/__PrimaryKey%20eq%20" + countryPkString + ")",
                "__PrimaryKey,ПорядковыйНомер");

                // Act.
                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Tests filtering details by master master property.
        /// </summary>
        [Fact]
        public void TestFilterByDetailMasterMasterProperty()
        {
            ActODataService(args =>
            {
                // Arrange.
                Порода breed = new Порода() { Название = "Бурый" };
                Страна country = new Страна() { Название = "Гана" };
                Лес forest = new Лес() { Название = "Тёмный", Страна = country };
                Берлога den = new Берлога() { Наименование = "Под ёлкой", ПодходитДляПороды = breed, ЛесРасположения = forest };

                Медведь bear = new Медведь() { ПорядковыйНомер = 1 };
                bear.Берлога.Add(den);

                DataObject[] newDataObjects = new DataObject[] { breed, forest, den, bear };

                args.DataService.UpdateObjects(ref newDataObjects);

                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}&$select={2}",
                args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name,
                "Берлога/any(f:f/ЛесРасположения/Страна/Название%20eq%20'Гана')",
                "__PrimaryKey,ПорядковыйНомер");

                // Act.
                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(1, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }
    }
}
