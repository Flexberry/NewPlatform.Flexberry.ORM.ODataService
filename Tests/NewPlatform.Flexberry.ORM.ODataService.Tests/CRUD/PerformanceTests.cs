namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    using ICSSoft.STORMNET;

    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;

    using Xunit;

    /// <summary>
    /// Тесты производительности CRUD операций.
    /// </summary>
    public class PerformanceTests : BaseODataServiceIntegratedTest
    {
        private const int ObjectCount = 1000;

#if NETFRAMEWORK
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="output">Вывод отладочной информации.</param>
        public PerformanceTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _output = output;
        }
#endif
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public PerformanceTests(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Тест создания.
        /// </summary>
        [Fact]
        public void CreateTest()
        {
            ActODataService(
                async args =>
                {
                    // Arrange.
                    List<HttpRequestMessage> requestMessages = Enumerable.Range(0, ObjectCount)
                        .Select(i => GetCreateMessage(args, nameof(CreateTest), i))
                        .ToList();

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    for (int i = 0; i < ObjectCount; i++)
                    {
                        using HttpRequestMessage request = requestMessages[i];

                        using HttpResponseMessage response = await args.HttpClient.SendAsync(request).ConfigureAwait(false);

                        // Убедимся, что запрос завершился успешно.
                        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                    }

                    stopwatch.Stop();

                    _output.WriteLine($"{nameof(CreateTest)}@{args.DataService.GetType().Name}: elapsed {stopwatch.ElapsedMilliseconds}");
                });
        }

        /// <summary>
        /// Тест чтения.
        /// </summary>
        [Fact]
        public void ReadTest()
        {
            ActODataService(
                async args =>
                {
                    // Arrange.
                    // Создаем объекты и кладем их в базу данных.
                    List<Страна> countries = Enumerable.Range(0, ObjectCount)
                        .Select(i => GetCountry(nameof(ReadTest), i))
                        .ToList();
                    var dobj = countries.Cast<DataObject>().ToArray();
                    args.DataService.UpdateObjects(ref dobj);

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    for (int i = 0; i < ObjectCount; i++)
                    {
                        var country = countries[i];

                        // Формируем URL запроса к OData-сервису.
                        string requestUrl = string.Format("http://localhost/odata/{0}?$filter={1}&$count=true", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name, $"Название eq '{country.Название}'");

                        using HttpResponseMessage response = await args.HttpClient.GetAsync(requestUrl).ConfigureAwait(false);

                        // Убедимся, что запрос завершился успешно.
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    }

                    stopwatch.Stop();

                    _output.WriteLine($"{nameof(ReadTest)}@{args.DataService.GetType().Name}: elapsed {stopwatch.ElapsedMilliseconds}");
                });
        }

        /// <summary>
        /// Тест обновления.
        /// </summary>
        [Fact]
        public void UpdateTest()
        {
            ActODataService(
                async args =>
                {
                    // Arrange.
                    // Создаем объекты и кладем их в базу данных.
                    List<Страна> countries = Enumerable.Range(0, ObjectCount)
                        .Select(i => GetCountry(nameof(UpdateTest), i))
                        .ToList();
                    var dobj = countries.Cast<DataObject>().ToArray();
                    args.DataService.UpdateObjects(ref dobj);

                    // Представление, по которому будем обновлять.
                    string[] updateProps =
                    {
                        Information.ExtractPropertyPath<Страна>(x => x.__PrimaryKey),
                        Information.ExtractPropertyPath<Страна>(x => x.Название),
                    };
                    var странаDynamicView = new View(new ViewAttribute("странаDynamicView", updateProps), typeof(Страна));

                    List<HttpRequestMessage> requestMessages = Enumerable.Range(0, ObjectCount)
                        .Select(i => GetUpdateMessage(args, странаDynamicView, countries[i], i))
                        .ToList();

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    for (int i = 0; i < ObjectCount; i++)
                    {
                        using var request = requestMessages[i];

                        using HttpResponseMessage response = await args.HttpClient.SendAsync(request).ConfigureAwait(false);

                        // Убедимся, что запрос завершился успешно.
                        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                    }

                    stopwatch.Stop();

                    _output.WriteLine($"{nameof(UpdateTest)}@{args.DataService.GetType().Name}: elapsed {stopwatch.ElapsedMilliseconds}");
                });
        }

        /// <summary>
        /// Тест удаления.
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            ActODataService(
                async args =>
                {
                    // Arrange.
                    // Создаем объекты и кладем их в базу данных.
                    List<Страна> countries = Enumerable.Range(0, ObjectCount)
                        .Select(i => GetCountry(nameof(DeleteTest), i))
                        .ToList();
                    var dobj = countries.Cast<DataObject>().ToArray();
                    args.DataService.UpdateObjects(ref dobj);

                    List<HttpRequestMessage> requestMessages = countries
                        .Select(c => GetDeleteMessage(args, c))
                        .ToList();

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    for (int i = 0; i < ObjectCount; i++)
                    {
                        using var request = requestMessages[i];

                        using HttpResponseMessage response = await args.HttpClient.SendAsync(request).ConfigureAwait(false);

                        // Убедимся, что запрос завершился успешно.
                        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                    }

                    stopwatch.Stop();

                    _output.WriteLine($"{nameof(DeleteTest)}@{args.DataService.GetType().Name}: elapsed {stopwatch.ElapsedMilliseconds}");
                });
        }

        private static Страна GetCountry(string testName, int i)
        {
            string countryName = $"{nameof(PerformanceTests)}.{testName}.{i}";
            return new Страна { Название = countryName };
        }

        private static HttpRequestMessage GetCreateMessage(TestArgs args, string testName, int i)
        {
            var страна = GetCountry(testName, i);

            // Формируем URL запроса к OData-сервису.
            string requestUrl = string.Format("http://localhost/odata/{0}", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name);

            // Преобразуем объект данных в JSON-строку.
            string json = страна.ToJson(Страна.Views.СтранаE, args.Token.Model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return new HttpRequestMessage(new HttpMethod("POST"), requestUrl) { Content = content };
        }

        private static HttpRequestMessage GetUpdateMessage(TestArgs args, View странаDynamicView, Страна country, int i)
        {
            country.Название = $"{nameof(PerformanceTests)}.Updated.{i}";

            // Формируем URL запроса к OData-сервису.
            string requestUrl = string.Format("http://localhost/odata/{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name, PKHelper.GetGuidByObject(country)?.ToString("D"));

            // Преобразуем объект данных в JSON-строку.
            string json = country.ToJson(странаDynamicView, args.Token.Model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return new HttpRequestMessage(new HttpMethod("PATCH"), requestUrl) { Content = content };
        }

        private static HttpRequestMessage GetDeleteMessage(TestArgs args, Страна country)
        {
            // Формируем URL запроса к OData-сервису.
            string requestUrl = string.Format("http://localhost/odata/{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name, PKHelper.GetGuidByObject(country)?.ToString("D"));

            return new HttpRequestMessage(new HttpMethod("DELETE"), requestUrl);
        }
    }
}
