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
    /// Unit-test class for filtering data through OData service by master fields.
    /// </summary>
    public class FilterByMasterFieldTest : BaseODataServiceIntegratedTest
    {
        /// <summary>
        /// Tests filtering data by master field.
        /// </summary>
        [Fact]
        public void TestFilterByMasterField()
        {
            ActODataService(args =>
            {
                var driver1 = new Driver { CarCount = 3, Documents = true, Name = "Driver1" };
                var car1d1 = new Car { Driver = driver1, Model = "ВАЗ" };
                var car2d1 = new Car { Driver = driver1, Model = "ГАЗ" };
                var car3d1 = new Car { Driver = driver1, Model = "УАЗ" };

                var driver2 = new Driver { CarCount = 4, Documents = false, Name = "Driver2" };
                var car1d2 = new Car { Driver = driver2, Model = "BMW" };
                var car2d2 = new Car { Driver = driver2, Model = "Porsche" };
                var car3d2 = new Car { Driver = driver2, Model = "Lamborghini" };
                var car4d2 = new Car { Driver = driver2, Model = "Subaru" };

                var objs = new DataObject[]
                {
                    driver1, car1d1, car2d1, car3d1,
                    driver2, car1d2, car2d2, car3d2, car4d2
                };
                args.DataService.UpdateObjects(ref objs);

                string requestUrl = string.Format(
                    "http://localhost/odata/{0}?$filter={1}",
                    args.Token.Model.GetEdmEntitySet(typeof(Car)).Name,
                    "Driver/Name eq 'Driver2'");

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    Dictionary<string, object> receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);
                    Assert.Equal(4, ((JArray)receivedDict["value"]).Count);
                }
            });
        }

        /// <summary>
        /// Tests filtering data by master of master field.
        /// </summary>
        [Fact]
        public void TestFilterByMasterOfMasterField()
        {
            ActODataService(args =>
            {
                var страна1 = new Страна { Название = "Россия" };
                var страна2 = new Страна { Название = "Китай" };
                var медведь1 = new Медведь { СтранаРождения = страна1, Вес = 300 };
                var медведь2 = new Медведь { СтранаРождения = страна2, Вес = 301 };
                var медведь3 = new Медведь { СтранаРождения = страна1, Вес = 302 };
                var берлога1 = new Берлога { Медведь = медведь1, Комфортность = 1 };
                var берлога2 = new Берлога { Медведь = медведь2, Комфортность = 2 };
                var берлога3 = new Берлога { Медведь = медведь3, Комфортность = 3 };

                var objs = new DataObject[]
                {
                    страна1,
                    страна2,
                    медведь1,
                    медведь2,
                    медведь3,
                    берлога1,
                    берлога2,
                    берлога3
                };
                args.DataService.UpdateObjects(ref objs);

                // так норм
                string requestUrl =
                    $"http://localhost/odata/{args.Token.Model.GetEdmEntitySet(typeof(Берлога)).Name}?$filter=Медведь/СтранаРождения/Название eq 'Россия'&$select= __PrimaryKey,Комфортность";

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    var receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);

                    Assert.Equal(2, ((JArray)receivedDict["value"]).Count);
                }

                // так норм
                requestUrl =
                    $"http://localhost/odata/{args.Token.Model.GetEdmEntitySet(typeof(Берлога)).Name}?$filter=Медведь/СтранаРождения/__PrimaryKey eq {PKHelper.GetGuidByObject(страна1)?.ToString("D")}";

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    var receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);

                    Assert.Equal(2, ((JArray)receivedDict["value"]).Count);
                }

                // и так норм
                requestUrl =
                    $"http://localhost/odata/{args.Token.Model.GetEdmEntitySet(typeof(Берлога)).Name}?$filter=Медведь/СтранаРождения/__PrimaryKey eq {PKHelper.GetGuidByObject(страна1)?.ToString("D")}&$select= __PrimaryKey,Комфортность,Медведь";

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    var receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);

                    Assert.Equal(2, ((JArray)receivedDict["value"]).Count);
                }

                // а так упадет
                requestUrl =
                    $"http://localhost/odata/{args.Token.Model.GetEdmEntitySet(typeof(Берлога)).Name}?$filter=Медведь/СтранаРождения/__PrimaryKey eq {PKHelper.GetGuidByObject(страна1)?.ToString("D")}&$select= __PrimaryKey,Комфортность";

                using (var response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string receivedStr = response.Content.ReadAsStringAsync().Result.Beautify();
                    var receivedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedStr);

                    Assert.Equal(2, ((JArray)receivedDict["value"]).Count);
                }
            });
        }
    }
}
