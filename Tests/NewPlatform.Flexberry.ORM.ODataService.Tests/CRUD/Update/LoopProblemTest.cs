namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Update
{
    using System;
    using System.Net;
    using System.Net.Http;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.KeyGen;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers;
    using Xunit;

    /// <summary>
    /// Тест проблемы зацикливания при патче.
    /// </summary>
#if NETFRAMEWORK
    public class LoopProblemTest : BaseODataServiceIntegratedTest
#endif
#if NETCOREAPP
    public class LoopProblemTest : BaseODataServiceIntegratedTest<TestStartup>
#endif
     {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public LoopProblemTest(CustomWebApplicationFactory<TestStartup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Проверка ситуации, когда в мастере есть ссылка на самого себя.
        /// </summary>
        [Fact]
        public void CheckRecursionTest()
        {
            ActODataService(args =>
            {
                // Представление, по которому будет производиться обновление.
                string[] blohaPropertiesNames =
                {
                    Information.ExtractPropertyPath<Блоха>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Блоха>(x => x.Кличка),
                };

                var blohaDynamicView = new View(new ViewAttribute("blohaDynamicView", blohaPropertiesNames), typeof(Блоха));

                // Создание объектов данных сервисом данных.
                Медведь bear3 = new Медведь { ПорядковыйНомер = 3, __PrimaryKey = new Guid("43db4b16-a109-4de3-8ae9-908a56a3e5dd") }; // Медведь со ссылкой на самого себя.
                bear3.Мама = bear3;
                args.DataService.UpdateObject(bear3);

                Блоха bloha = new Блоха() { Кличка = "Проверка рекурсии", МедведьОбитания = bear3 };
                args.DataService.UpdateObject(bloha);

                bloha.МедведьОбитания = bear3; // Установка того же значения, чтобы корректно осуществлять работу с кэшем и воспроизводить ситуацию неполностью загруженного мастера.
                bloha.Кличка = "Другое значение";

                // Преобразование объекта данных в JSON-строку.
                string requestJsonData = bloha.ToJson(blohaDynamicView, args.Token.Model);
                DataObjectDictionary objJson = DataObjectDictionary.Parse(requestJsonData, blohaDynamicView, args.Token.Model);

                objJson.Add(
                    "МедведьОбитания@odata.bind",
                    string.Format("{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name, ((KeyGuid)bear3.__PrimaryKey).Guid.ToString("D")));

                string requestJsonDataBloha = objJson.Serialize();
                string requestUrl = string.Format("http://localhost/odata/{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(Блоха)).Name, ((KeyGuid)bloha.__PrimaryKey).Guid.ToString());

                using (HttpResponseMessage response = args.HttpClient.PatchAsJsonStringAsync(requestUrl, requestJsonDataBloha).Result)
                {
                    // Проверка, что операция патч не привела к рекурсии и корректно отработала.
                    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                }
            });
        }
    }
}
