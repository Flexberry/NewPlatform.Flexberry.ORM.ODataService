namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Update
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.KeyGen;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
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

        /// <summary>
        /// Тест проверяет, что метод <see cref="ProperUpdateOfObject"/> корректно обрабатывает мастеров:
        /// - переносит недостающие свойства из свежезагруженного объекта в основной объект;
        /// - не перезаписывает уже загруженные свойства;
        /// - предотвращает бесконечную рекурсию при циклических ссылках (самоссылка мастера).
        /// 
        /// В тесте используются два кэша:
        /// 1. <param name="cache"> — основной кэш, имитирующий существующие объекты, уже загруженные в систему.</param>
        ///    - <param name ="StartCaching(false)"> означает, что кэш не будет создавать объекты автоматически;</param>
        ///    - в него вручную добавляем «старые» объекты master и bloha.
        /// 2. <param name ="localCache"> — локальный кэш, имитирующий свежезагруженные объекты из базы.</param>
        ///    - в него мы не добавляем объекты вручную, потому что цель теста — проверить метод ProperUpdateOfObject, который переносит данные из локального кэша в основной.
        /// 
        /// Объекты masterLoaded и blohaLoaded представляют свежие данные из базы (копии объектов с теми же ключами).
        /// Метод должен обновить только недостающие свойства, не затирая уже существующие значения.
        /// </summary>
        [Fact]
        public void ProperUpdateObjects_Should_NoSkipMasters() 
        { 
            Медведь master = new Медведь { __PrimaryKey = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), ПорядковыйНомер = 1 };
            master.Мама = master;

            // Деталь и её копия (fresh load из базы).
            Блоха bloha = new Блоха { __PrimaryKey = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), МедведьОбитания = master };

            DataObjectCache cache = new DataObjectCache();
            cache.StartCaching(false);
            cache.AddDataObject(master);
            cache.AddDataObject(bloha);

            DataObjectCache localCache = new DataObjectCache();
            localCache.StartCaching(false);

            Медведь masterLoaded = new Медведь { __PrimaryKey = master.__PrimaryKey, ПорядковыйНомер = 2, Вес = 120 };

            masterLoaded.Мама = masterLoaded;

            Блоха blohaLoaded = new Блоха { Кличка = "БлохаАпдейт", __PrimaryKey = bloha.__PrimaryKey, МедведьОбитания = masterLoaded,    };
            HashSet<TypeKeyTuple> processed = new HashSet<TypeKeyTuple>();

            //Act
            var method = typeof(NewPlatform.Flexberry.ORM.ODataService.Extensions.DataServiceExtensions)
                .GetMethod("ProperUpdateOfObject", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            method.Invoke(null, new object[] { bloha, blohaLoaded, cache, localCache, processed });
            var bearFromCache = cache.GetLivingDataObject(typeof(Медведь), master.__PrimaryKey);
            var blohaFromCache = cache.GetLivingDataObject(typeof(Блоха), bloha.__PrimaryKey);

            //Assert
            Assert.NotNull(bearFromCache);
            Assert.NotNull(blohaFromCache);
            Assert.Equal(1, ((Медведь)bearFromCache).ПорядковыйНомер);
            Assert.Equal(120, ((Медведь)bearFromCache).Вес);
            Assert.Equal(master, ((Блоха)blohaFromCache).МедведьОбитания);
            Assert.Equal("БлохаАпдейт", ((Блоха)blohaFromCache).Кличка);

        }
    }
}
