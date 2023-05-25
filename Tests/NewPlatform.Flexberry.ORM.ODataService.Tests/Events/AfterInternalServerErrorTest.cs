namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Events
{
    using System;
    using System.Net;
    using System.Net.Http;
    using ICSSoft.STORMNET;
#if NETCOREAPP
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Exceptions;
#endif
    using Xunit;

    /// <summary>
    /// Класс тестов для тестирования логики после возникновения исключения.
    /// </summary>
    public class AfterInternalServerErrorTest : BaseODataServiceIntegratedTest
    {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="fixtureData">Данные для теста.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public AfterInternalServerErrorTest(TestFixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper output)
            : base(fixtureData, output)
        {
        }
#endif

        private Exception Ex { get; set; }

        /// <summary>
        /// Метод вызывает после возникновения исключения.
        /// </summary>
        /// <param name="e">Исключение, которое возникло внутри ODataService.</param>
        /// <param name="code">Возвращаемый код HTTP. По-умолчанияю 500.</param>
        /// <returns>Исключение, которое будет отправлено клиенту.</returns>
        public Exception AfterInternalServerError(Exception e, ref HttpStatusCode code)
        {
            Ex = e;
            code = HttpStatusCode.InternalServerError;
            return e;
        }

        /// <summary>
        /// Тест для проверки корректности обработки события после возникновения исключения.
        /// </summary>
        [Fact]
        public void TestAfterInternalServerError()
        {
            ActODataService(args =>
            {
#if NETFRAMEWORK
                args.Token.Events.CallbackAfterInternalServerError = AfterInternalServerError;
#elif NETCOREAPP
                CustomExceptionFilter.CallbackAfterInternalServerError = AfterInternalServerError;
#endif

                Медведь медв = new Медведь { Вес = 48, Пол = tПол.Мужской };
                Медведь медв2 = new Медведь { Вес = 148, Пол = tПол.Мужской };
                Лес лес = new Лес { Название = "Бор" };
                Лес лес2 = new Лес { Название = "Березовая роща" };
                медв.ЛесОбитания = лес;
                медв2.ЛесОбитания = лес2;
                var берлога = new Берлога { Наименование = "Для хорошего настроения", ЛесРасположения = лес };
                var берлога2 = new Берлога { Наименование = "Для плохого настроения", ЛесРасположения = лес2 };
                медв.Берлога.Add(берлога);
                медв2.Берлога.Add(берлога2);
                var objs = new DataObject[] { медв, медв2, берлога2, берлога, лес, лес2 };
                args.DataService.UpdateObjects(ref objs);
                Ex = null;
                string requestUrl = string.Format("http://localhost/odata/{0}?$filter=", args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name);

                // Обращаемся к OData-сервису и обрабатываем ответ.
                using (HttpResponseMessage response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
                    Assert.NotNull(Ex);
                }
            });
        }
    }
}
