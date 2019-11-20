﻿namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Events
{
    using System.Net;
    using System.Net.Http;

    using ICSSoft.STORMNET;

    using NewPlatform.Flexberry.ORM.ODataService.Controllers;
    using NewPlatform.Flexberry.ORM.ODataService.Events;

    using Unity;

    using Xunit;

    /// <summary>
    /// Класс тестов для тестирования логики после операции считывания данных OData-сервисом.
    /// </summary>

    public class AfterGetTest : BaseODataServiceIntegratedTest
    {
        /// <summary>
        /// Содержит массив DataObject, который является параметром в методе AfterGet.
        /// </summary>
        private DataObject[] Objs { get; set; }

        /// <summary>
        /// Метод вызываемый после вычитывания объектов.
        /// </summary>
        /// <param name="objs">Считанные объекты.</param>
        public void AfterGet(DataObjectController controller, ref DataObject[] objs)
        {
            Objs = objs;
        }

        /// <summary>
        /// Тест для проверки корректности обработки события после вычитывания объектов.
        /// </summary>
        [Fact]
        public void TestAfterGet()
        {
            ActODataService(args =>
            {
                var eventsContainer = new FakeEventHandlerContainer { CallbackAfterGet = AfterGet };
                args.UnityContainer.RegisterInstance<IEventHandlerContainer>(eventsContainer);

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
                Objs = null;
                string requestUrl = string.Format("http://localhost/odata/{0}", args.Token.Model.GetEdmEntitySet(typeof(Медведь)).Name);

                // Обращаемся к OData-сервису и обрабатываем ответ.
                using (HttpResponseMessage response = args.HttpClient.GetAsync(requestUrl).Result)
                {
                    // Убедимся, что запрос завершился успешно.
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    Assert.NotNull(Objs);
                }
            });
        }
    }
}
