#if NETCOREAPP
// TODO: разобраться с HttpContext.Current под netframework.
namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;

    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.FunctionalLanguage;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Unity;

    using Xunit;

    /// <summary>
    /// Тесты CRUD операций с множеством пользователей.
    /// </summary>
    public class MultiThreadTests : BaseODataServiceIntegratedTest
    {
        private const int ThreadCount = 50;

#if NETFRAMEWORK
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="output">Вывод отладочной информации.</param>
        public MultiThreadTests(Xunit.Abstractions.ITestOutputHelper output)
            : base()
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
        public MultiThreadTests(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Многопоточный тест создания.
        /// </summary>
        [Fact]
        public void CreateTest()
        {
            ActODataService(
                args =>
                {
                    // Arrange.
#if NETFRAMEWORK
                    RegisterCustomUser(UnityFactory.GetContainer());
#elif NETCOREAPP
                    RegisterCustomUser(args.UnityContainer);
#endif

                    // Регистрация событий.
                    args.Token.Events.CallbackBeforeCreate = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;
                        return dobj is Страна country && country.Название == login;
                    };

                    args.Token.Events.CallbackAfterCreate = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;
                        if (!(dobj is Страна country && country.Название == login))
                        {
                            throw new Exception("Incorrect data");
                        }
                    };

                    // Act.
                    var multiThreadingTestTool = new MultiThreadingTestTool(MultiThreadMethodCreate);
                    multiThreadingTestTool.StartThreads(ThreadCount, args);

                    // Assert.
                    var exception = multiThreadingTestTool.GetExceptions();
                    if (exception != null)
                    {
                        foreach (var item in exception.InnerExceptions)
                        {
                            _output.WriteLine($"Thread {item.Key}: {item.Value}");
                        }

                        // Пусть так.
                        Assert.Empty(exception.InnerExceptions);
                    }
                });
        }

        /// <summary>
        /// Многопоточный тест чтения.
        /// </summary>
        [Fact]
        public void ReadTest()
        {
            ActODataService(
                args =>
                {
                    // Arrange.
#if NETFRAMEWORK
                    RegisterCustomUser(UnityFactory.GetContainer());
#elif NETCOREAPP
                    RegisterCustomUser(args.UnityContainer);
#endif

                    // Создаем объекты и кладем их в базу данных.
                    DataObject[] countries = new DataObject[ThreadCount];
                    for (int i = 0; i < ThreadCount; i++)
                    {
                        countries[i] = new Страна { Название = $"Read_Th_{i}" };
                    }

                    args.DataService.UpdateObjects(ref countries);

                    // Act.
                    var multiThreadingTestTool = new MultiThreadingTestTool(MultiThreadMethodRead);
                    multiThreadingTestTool.StartThreads(ThreadCount, args);

                    // Assert.
                    var exception = multiThreadingTestTool.GetExceptions();
                    if (exception != null)
                    {
                        foreach (var item in exception.InnerExceptions)
                        {
                            _output.WriteLine($"Thread {item.Key}: {item.Value}");
                        }

                        // Пусть так.
                        Assert.Empty(exception.InnerExceptions);
                    }
                });
        }

        /// <summary>
        /// Многопоточный тест обновления.
        /// </summary>
        [Fact]
        public void UpdateTest()
        {
            ActODataService(
                args =>
                {
                    // Arrange.
#if NETFRAMEWORK
                    RegisterCustomUser(UnityFactory.GetContainer());
#elif NETCOREAPP
                    RegisterCustomUser(args.UnityContainer);
#endif

                    // Создаем объекты и кладем их в базу данных.
                    DataObject[] countries = new DataObject[ThreadCount];
                    for (int i = 0; i < ThreadCount; i++)
                    {
                        countries[i] = new Страна { Название = $"Update_Th_{i}" };
                    }

                    args.DataService.UpdateObjects(ref countries);

                    // Регистрация событий.
                    args.Token.Events.CallbackBeforeUpdate = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;
                        return dobj is Страна country && country.Название == login;
                    };

                    args.Token.Events.CallbackAfterUpdate = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;
                        if (!(dobj is Страна country && country.Название == login))
                        {
                            throw new Exception("Incorrect data");
                        }
                    };

                    // Act.
                    var multiThreadingTestTool = new MultiThreadingTestTool(MultiThreadMethodUpdate);
                    multiThreadingTestTool.StartThreads(ThreadCount, args);

                    // Assert.
                    var exception = multiThreadingTestTool.GetExceptions();
                    if (exception != null)
                    {
                        foreach (var item in exception.InnerExceptions)
                        {
                            _output.WriteLine($"Thread {item.Key}: {item.Value}");
                        }

                        // Пусть так.
                        Assert.Empty(exception.InnerExceptions);
                    }
                });
        }

        /// <summary>
        /// Многопоточный тест удаления.
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            ActODataService(
                args =>
                {
                    // Arrange.
#if NETFRAMEWORK
                    RegisterCustomUser(UnityFactory.GetContainer());
#elif NETCOREAPP
                    RegisterCustomUser(args.UnityContainer);
#endif

                    // Создаем объекты и кладем их в базу данных.
                    DataObject[] countries = new DataObject[ThreadCount];
                    for (int i = 0; i < ThreadCount; i++)
                    {
                        countries[i] = new Страна { Название = $"Delete_Th_{i}" };
                    }

                    args.DataService.UpdateObjects(ref countries);

                    // Регистрация событий.
                    args.Token.Events.CallbackBeforeDelete = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;

                        // Возьмем объект из эталона.
                        var preparedCountry = countries.FirstOrDefault(x => PKHelper.EQDataObject(dobj, x, true));
                        return preparedCountry is Страна country && country.Название == login;
                    };

                    args.Token.Events.CallbackAfterDelete = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;

                        // Возьмем объект из эталона.
                        var preparedCountry = countries.FirstOrDefault(x => PKHelper.EQDataObject(dobj, x, true));
                        if (!(preparedCountry is Страна country && country.Название == login))
                        {
                            throw new Exception("Incorrect data");
                        }
                    };

                    // Act.
                    var multiThreadingTestTool = new MultiThreadingTestTool(MultiThreadMethodDelete);
                    multiThreadingTestTool.StartThreads(ThreadCount, args);

                    // Assert.
                    var exception = multiThreadingTestTool.GetExceptions();
                    if (exception != null)
                    {
                        foreach (var item in exception.InnerExceptions)
                        {
                            _output.WriteLine($"Thread {item.Key}: {item.Value}");
                        }

                        // Пусть так.
                        Assert.Empty(exception.InnerExceptions);
                    }
                });
        }

        /// <summary>
        /// Многопоточный тест порционного запроса.
        /// </summary>
        [Fact]
        public void BatchTest()
        {
            ActODataService(
                args =>
                {
                    // Arrange.
#if NETFRAMEWORK
                    RegisterCustomUser(UnityFactory.GetContainer());
#elif NETCOREAPP
                    RegisterCustomUser(args.UnityContainer);
#endif

                    // Регистрация событий.
                    args.Token.Events.CallbackBeforeUpdate = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;
                        return dobj is Кошка cat && cat.Кличка == login;
                    };

                    args.Token.Events.CallbackAfterUpdate = dobj =>
                    {
                        string login = CurrentUserService.CurrentUser.Login;
                        if (!(dobj is Кошка cat && cat.Кличка == login))
                        {
                            throw new Exception("Incorrect data");
                        }
                    };

                    // Act.
                    var multiThreadingTestTool = new MultiThreadingTestTool(MultiThreadMethodBatch);
                    multiThreadingTestTool.StartThreads(ThreadCount, args);

                    // Assert.
                    var exception = multiThreadingTestTool.GetExceptions();
                    if (exception != null)
                    {
                        foreach (var item in exception.InnerExceptions)
                        {
                            _output.WriteLine($"Thread {item.Key}: {item.Value}");
                        }

                        // Пусть так.
                        Assert.Empty(exception.InnerExceptions);
                    }
                });
        }

        private static void RegisterCustomUser(IUnityContainer container)
        {
            container.RegisterType<IHttpContextAccessor, HttpContextAccessor>();
#if NETCOREAPP
            container.RegisterType<IActionContextAccessor, ActionContextAccessor>();
  #endif
            container.RegisterType<CurrentUserService.IUser, WebHttpUser>();
        }

        private static void MultiThreadMethodCreate(object sender)
        {
            var parametersDictionary = (Dictionary<string, object>)sender;
            var args = (TestArgs)parametersDictionary[MultiThreadingTestTool.ParamNameSender];
            var exceptions = (ConcurrentDictionary<string, Exception>)parametersDictionary[MultiThreadingTestTool.ParamNameExceptions];
            var threadName = (string)parametersDictionary[MultiThreadingTestTool.ParamNameThreadName];

            try
            {
                var countryName = $"Create_{threadName}";
                var страна = new Страна { Название = countryName };

                // Формируем URL запроса к OData-сервису.
                string requestUrl = string.Format("http://localhost/odata/{0}", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name);

                // Преобразуем объект данных в JSON-строку.
                string json = страна.ToJson(Страна.Views.СтранаE, args.Token.Model);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using var request = new HttpRequestMessage(new HttpMethod("POST"), requestUrl) { Content = content };
                request.Headers.Add("username", new[] { countryName });

                using HttpResponseMessage response = args.HttpClient.SendAsync(request).Result;

                // Убедимся, что запрос завершился успешно.
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
            catch (Exception exception)
            {
                exceptions.TryAdd(Thread.CurrentThread.Name, exception);
                parametersDictionary[MultiThreadingTestTool.ParamNameWorking] = false;
            }
        }

        private static void MultiThreadMethodRead(object sender)
        {
            var parametersDictionary = (Dictionary<string, object>)sender;
            var args = (TestArgs)parametersDictionary[MultiThreadingTestTool.ParamNameSender];
            var exceptions = (ConcurrentDictionary<string, Exception>)parametersDictionary[MultiThreadingTestTool.ParamNameExceptions];
            var threadName = (string)parametersDictionary[MultiThreadingTestTool.ParamNameThreadName];

            try
            {
                // Формируем URL запроса к OData-сервису.
                string requestUrl = string.Format("http://localhost/odata/{0}?$filter={1}&$count=true", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name, $"Название eq 'Read_{threadName}'");

                using HttpResponseMessage response = args.HttpClient.GetAsync(requestUrl).Result;

                // Убедимся, что запрос завершился успешно.
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Получим строку с ответом.
                string receivedJsonCountries = response.Content.ReadAsStringAsync().Result.Beautify();

                // Преобразуем полученный объект в словарь.
                Dictionary<string, object> receivedCountries = JsonConvert.DeserializeObject<Dictionary<string, object>>(receivedJsonCountries);

                // Убедимся, что объекты получены и их нужное количество.
                Assert.True(receivedCountries.ContainsKey("@odata.count"));
                Assert.Equal(receivedCountries["@odata.count"], 1L);
                Assert.True(receivedCountries.ContainsKey("value"));
                Assert.Equal(((JArray)receivedCountries["value"]).Count, 1);
            }
            catch (Exception exception)
            {
                exceptions.TryAdd(Thread.CurrentThread.Name, exception);
                parametersDictionary[MultiThreadingTestTool.ParamNameWorking] = false;
            }
        }

        private static void MultiThreadMethodUpdate(object sender)
        {
            var parametersDictionary = (Dictionary<string, object>)sender;
            var args = (TestArgs)parametersDictionary[MultiThreadingTestTool.ParamNameSender];
            var exceptions = (ConcurrentDictionary<string, Exception>)parametersDictionary[MultiThreadingTestTool.ParamNameExceptions];
            var threadName = (string)parametersDictionary[MultiThreadingTestTool.ParamNameThreadName];

            try
            {
                // Представление, по которому будем обновлять.
                string[] updateProps =
                {
                    Information.ExtractPropertyPath<Страна>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Страна>(x => x.Название),
                };
                var странаDynamicView = new View(new ViewAttribute("странаDynamicView", updateProps), typeof(Страна));

                var lcs = LoadingCustomizationStruct.GetSimpleStruct(typeof(Страна), странаDynamicView);
                lcs.LimitFunction = FunctionBuilder.BuildEquals<Страна>(x => x.Название, $"Update_{threadName}");
                lcs.ReturnTop = 1;
                var страна = args.DataService.LoadObjects(lcs).Cast<Страна>().FirstOrDefault();

                Assert.NotNull(страна);

                var countryName = $"Updated_{threadName}";
                страна.Название = countryName;

                // Формируем URL запроса к OData-сервису.
                string requestUrl = string.Format("http://localhost/odata/{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name, PKHelper.GetGuidByObject(страна)?.ToString("D"));

                // Преобразуем объект данных в JSON-строку.
                string json = страна.ToJson(странаDynamicView, args.Token.Model);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUrl) { Content = content };
                request.Headers.Add("username", new[] { countryName });

                using HttpResponseMessage response = args.HttpClient.SendAsync(request).Result;

                // Убедимся, что запрос завершился успешно.
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
            catch (Exception exception)
            {
                exceptions.TryAdd(Thread.CurrentThread.Name, exception);
                parametersDictionary[MultiThreadingTestTool.ParamNameWorking] = false;
            }
        }

        private static void MultiThreadMethodDelete(object sender)
        {
            var parametersDictionary = (Dictionary<string, object>)sender;
            var args = (TestArgs)parametersDictionary[MultiThreadingTestTool.ParamNameSender];
            var exceptions = (ConcurrentDictionary<string, Exception>)parametersDictionary[MultiThreadingTestTool.ParamNameExceptions];
            var threadName = (string)parametersDictionary[MultiThreadingTestTool.ParamNameThreadName];

            try
            {
                var countryName = $"Delete_{threadName}";

                // Ищем объект в БД.
                var lcs = LoadingCustomizationStruct.GetSimpleStruct(typeof(Страна), new View(typeof(Страна), View.ReadType.OnlyThatObject));
                lcs.LimitFunction = FunctionBuilder.BuildEquals<Страна>(x => x.Название, countryName);
                lcs.ReturnTop = 1;
                var страна = args.DataService.LoadObjects(lcs).Cast<Страна>().FirstOrDefault();

                Assert.NotNull(страна);

                // Формируем URL запроса к OData-сервису.
                string requestUrl = string.Format("http://localhost/odata/{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(Страна)).Name, PKHelper.GetGuidByObject(страна)?.ToString("D"));

                using var request = new HttpRequestMessage(new HttpMethod("DELETE"), requestUrl);
                request.Headers.Add("username", new[] { countryName });

                using HttpResponseMessage response = args.HttpClient.SendAsync(request).Result;

                // Убедимся, что запрос завершился успешно.
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
            catch (Exception exception)
            {
                exceptions.TryAdd(Thread.CurrentThread.Name, exception);
                parametersDictionary[MultiThreadingTestTool.ParamNameWorking] = false;
            }
        }

        private static void MultiThreadMethodBatch(object sender)
        {
            var parametersDictionary = (Dictionary<string, object>)sender;
            var args = (TestArgs)parametersDictionary[MultiThreadingTestTool.ParamNameSender];
            var exceptions = (ConcurrentDictionary<string, Exception>)parametersDictionary[MultiThreadingTestTool.ParamNameExceptions];
            var threadName = (string)parametersDictionary[MultiThreadingTestTool.ParamNameThreadName];

            try
            {
                // Arrange.
                var nickname = $"Batch_{threadName}";

                var порода = new Порода { Название = "Первая" };
                var кошка = new Кошка { Кличка = nickname, Порода = порода };
                var стараяЛапа = new Лапа { Номер = 1 };
                кошка.Лапа.Add(стараяЛапа);

                args.DataService.UpdateObject(кошка);

                стараяЛапа.SetStatus(ObjectStatus.Deleted);
                var новаяЛапа = new Лапа { Номер = 2 };
                кошка.Лапа.Add(новаяЛапа);

                // Представление, по которому будем обновлять.
                string[] updateProps =
                {
                    Information.ExtractPropertyPath<Лапа>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Лапа>(x => x.Номер),
                    Information.ExtractPropertyPath<Лапа>(x => x.Кошка),
                };
                var лапаDynamicView = new View(new ViewAttribute("лапаDynamicView", updateProps), typeof(Лапа));

                const string baseUrl = "http://localhost/odata";

                string[] changesets = new[]
                {
                    BatchHelper.CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Кошка)).Name}",
                        "{}",
                        кошка),
                    BatchHelper.CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Лапа)).Name}",
                        null,
                        стараяЛапа),
                    BatchHelper.CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Лапа)).Name}",
                        новаяЛапа.ToJson(лапаDynamicView, args.Token.Model),
                        новаяЛапа),
                };

                using HttpRequestMessage batchRequest = BatchHelper.CreateBatchRequest(baseUrl, changesets);
                batchRequest.Headers.Add("username", new[] { nickname });

                // Act.
                using HttpResponseMessage response = args.HttpClient.SendAsync(batchRequest).Result;

                // Assert.
                BatchHelper.CheckODataBatchResponseStatusCode(response, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent, HttpStatusCode.Created });

                var view = Кошка.Views.КошкаE;
                view.AddDetailInView(nameof(Кошка.Лапа), Лапа.Views.ЛапаE, true);
                args.DataService.LoadObject(view, кошка);

                var лапы = кошка.Лапа.Cast<Лапа>().ToList();

                Assert.Single(лапы);
            }
            catch (Exception exception)
            {
                exceptions.TryAdd(Thread.CurrentThread.Name, exception);
                parametersDictionary[MultiThreadingTestTool.ParamNameWorking] = false;
            }
        }
    }
}
#endif
