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
    /// Unit-test class for filtering data through OData service by master details fields.
    /// </summary>
    public class FilterByMasterDetailFieldTest : BaseODataServiceIntegratedTest
    {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public FilterByMasterDetailFieldTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

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

                Лес лес1 = new Лес() { Название = "Шишкин" };
                Лес лес2 = new Лес() { Название = "Ёжкин" };

                Берлога берлога1 = new Берлога() { Наименование = "Берлога 1", ЛесРасположения = лес1 };
                Берлога берлога2 = new Берлога() { Наименование = "Берлога 2", ЛесРасположения = лес1 };
                Берлога берлога3 = new Берлога() { Наименование = "Берлога 3", ЛесРасположения = лес2 };
                Берлога берлога4 = new Берлога() { Наименование = "Берлога 4", ЛесРасположения = лес2 };

                медведь1.Берлога.AddRange(берлога1, берлога2);
                медведь2.Берлога.AddRange(берлога3, берлога4);

                Блоха блоха1 = new Блоха() { Кличка = "Блоха 1", МедведьОбитания = медведь1 };
                Блоха блоха2 = new Блоха() { Кличка = "Блоха 2", МедведьОбитания = медведь2 };
                Блоха блоха3 = new Блоха() { Кличка = "Блоха 3" };
                Блоха блоха4 = new Блоха() { Кличка = "Блоха 4", МедведьОбитания = медведь1 };

                DataObject[] newDataObjects = new DataObject[] { лес1, лес2, медведь1, медведь2, берлога1, берлога2, берлога3, берлога4, блоха1, блоха2, блоха3, блоха4 };

                args.DataService.UpdateObjects(ref newDataObjects);
                ExternalLangDef.LanguageDef.DataService = args.DataService;

                string лес1Pk = ((KeyGuid)лес1.__PrimaryKey).Guid.ToString("D");

                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}",
                args.Token.Model.GetEdmEntitySet(typeof(Блоха)).Name,
                "МедведьОбитания/Берлога/any(f:(f/Наименование eq 'Берлога 1') and ( ( not(f/ЛесРасположения/Площадь eq 123)) or (f/ЛесРасположения/__PrimaryKey eq " + лес1Pk + ") ))");

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(2, ((JArray)receivedDict["value"]).Count);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            });
        }

        /// <summary>
        /// Tests filtering data by master field with complex predicate.
        /// </summary>
        [Fact]
        public void TestFilterByMasterDetailComplexPredicate()
        {
            ActODataService(args =>
            {
                // Arrange.
                Медведь медведь1 = new Медведь() { ПорядковыйНомер = 1, };
                Медведь медведь2 = new Медведь() { ПорядковыйНомер = 2 };

                Лес лес1 = new Лес() { Название = "Шишкин" };
                Лес лес2 = new Лес() { Название = "Ёжкин" };

                Берлога берлога1 = new Берлога() { Наименование = "Берлога 1", ЛесРасположения = лес1 };
                Берлога берлога2 = new Берлога() { Наименование = "Берлога 2", ЛесРасположения = лес1 };
                Берлога берлога3 = new Берлога() { Наименование = "Берлога 3", ЛесРасположения = лес2 };
                Берлога берлога4 = new Берлога() { Наименование = "Берлога 4", ЛесРасположения = лес2 };

                медведь1.Берлога.AddRange(берлога1, берлога2);
                медведь2.Берлога.AddRange(берлога3, берлога4);

                Блоха блоха1 = new Блоха() { Кличка = "Блоха 1", МедведьОбитания = медведь1 };
                Блоха блоха2 = new Блоха() { Кличка = "Блоха 2", МедведьОбитания = медведь2 };
                Блоха блоха3 = new Блоха() { Кличка = "Блоха 3" };
                Блоха блоха4 = new Блоха() { Кличка = "Блоха 4", МедведьОбитания = медведь1 };

                DataObject[] newDataObjects = new DataObject[] { лес1, лес2, медведь1, медведь2, берлога1, берлога2, берлога3, берлога4, блоха1, блоха2, блоха3, блоха4 };

                args.DataService.UpdateObjects(ref newDataObjects);
                ExternalLangDef.LanguageDef.DataService = args.DataService;

                string лес1Pk = ((KeyGuid)лес1.__PrimaryKey).Guid.ToString("D");
                string медведь1Pk = ((KeyGuid)медведь1.__PrimaryKey).Guid.ToString("D");

                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}",
                args.Token.Model.GetEdmEntitySet(typeof(Блоха)).Name,
                "(МедведьОбитания/Берлога/any(f:(f/Наименование eq 'Берлога 1') and ( ( not(f/ЛесРасположения/Площадь eq 123)) or (f/ЛесРасположения/__PrimaryKey eq " + лес1Pk + ") )) ) and (МедведьОбитания/__PrimaryKey eq " + медведь1Pk + ")");

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(2, ((JArray)receivedDict["value"]).Count);
                }
            });
        }

        /// <summary>
        /// Tests filtering data by detail field with complex predicate.
        /// </summary>
        [Fact]
        public void TestFilterByDetailMaster()
        {
            ActODataService(args =>
            {
                // Arrange.
                Медведь медведь1 = new Медведь() { ПорядковыйНомер = 1, };
                Медведь медведь2 = new Медведь() { ПорядковыйНомер = 2 };
                Медведь медведь3 = new Медведь() { ПорядковыйНомер = 3 };

                Лес лес1 = new Лес() { Название = "Шишкин" };
                Лес лес2 = new Лес() { Название = "Ёжкин" };
                Лес лес3 = new Лес() { Название = "Пыжкин" };

                Берлога берлога1 = new Берлога() { Наименование = "Берлога 1", ЛесРасположения = лес1, Заброшена = true };
                Берлога берлога2 = new Берлога() { Наименование = "Берлога 2", ЛесРасположения = лес1, Заброшена = false };
                Берлога берлога3 = new Берлога() { Наименование = "Берлога 3", ЛесРасположения = лес2, Заброшена = false };
                Берлога берлога4 = new Берлога() { Наименование = "Берлога 4", ЛесРасположения = лес2, Заброшена = false };
                Берлога берлога5 = new Берлога() { Наименование = "Берлога 5", ЛесРасположения = лес3, Заброшена = false };
                Берлога берлога6 = new Берлога() { Наименование = "Берлога 6", ЛесРасположения = лес3, Заброшена = true};

                медведь1.Берлога.AddRange(берлога1, берлога2);
                медведь2.Берлога.AddRange(берлога3, берлога4);
                медведь3.Берлога.AddRange(берлога5, берлога6);

                DataObject[] newDataObjects = new DataObject[] { лес1, лес2, лес3, медведь1, медведь2, берлога1, берлога2, берлога3, берлога4, берлога5, берлога6 };

                args.DataService.UpdateObjects(ref newDataObjects);
                ExternalLangDef.LanguageDef.DataService = args.DataService;

                // Act.
                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}",
                args.Token.Model.GetEdmEntitySet(typeof(Берлога)).Name,
                "(Медведь/Берлога/any(f:(f/Заброшена eq true)))");

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(4, ((JArray)receivedDict["value"]).Count);
                }
            });
        }

        /// <summary>
        /// Tests filtering data by detail enum field with complex predicate.
        /// </summary>
        [Fact]
        public void TestFilterByEnumDetailMaster()
        {
            ActODataService(args =>
            {
                // Arrange.
                Driver driver1 = new Driver { CarCount = 2, Documents = true, Name = "Driver1" };
                Driver driver2 = new Driver { CarCount = 2, Documents = true, Name = "Driver2" };
                Driver driver3 = new Driver { CarCount = 2, Documents = true, Name = "Driver3" };

                Car car1d1 = new Car { Model = "ВАЗ", TipCar = tTip.sedan };
                Car car2d1 = new Car { Model = "ГАЗ", TipCar = tTip.sedan };

                Car car1d2 = new Car { Model = "BMW", TipCar = tTip.crossover };
                Car car2d2 = new Car { Model = "Porsche", TipCar = tTip.sedan };

                Car car1d3 = new Car { Model = "Lamborghini", TipCar = tTip.crossover };
                Car car2d3 = new Car { Model = "Subaru", TipCar = tTip.sedan };

                driver1.Car.AddRange(car1d1, car2d1);
                driver2.Car.AddRange(car1d2, car2d2);
                driver3.Car.AddRange(car1d3, car2d3);

                DataObject[] newDataObjects = new DataObject[] { driver1, driver2, driver3, car1d1, car2d1, car1d2, car2d2, car1d3, car2d3 };

                args.DataService.UpdateObjects(ref newDataObjects);
                ExternalLangDef.LanguageDef.DataService = args.DataService;

                // Act.
                string requestUrl = string.Format(
                "http://localhost/odata/{0}?$filter={1}",
                args.Token.Model.GetEdmEntitySet(typeof(Car)).Name,
                "(Driver/Car/any(f:(f/TipCar eq NewPlatform.Flexberry.ORM.ODataService.Tests.tTip'crossover')))");

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Assert.
                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(4, ((JArray)receivedDict["value"]).Count);
                }
            });
        }
    }
}
