namespace NewPlatform.Flexberry.ORM.ODataService.Tests.CRUD.Update
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using ICSSoft.STORMNET.KeyGen;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers;
    using Xunit;
    using View = ICSSoft.STORMNET.View;

    /// <summary>
    /// The class of tests for CRUD operations at Batch form.
    /// There are extra batch tests at <see cref="ModifyDataTest"/>.
    /// </summary>
    public class BatchTest : BaseODataServiceIntegratedTest
    {
#if NETCOREAPP
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="factory">Factory for application.</param>
        /// <param name="output">Output for debug information.</param>
        public BatchTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        /// <summary>
        /// Test batch update of master-class with class at the same time.
        /// It checks that dataobject cache is not crashed.
        /// </summary>
        [Fact]
        public void UpdateMasterAndClassTest()
        {
            /* Суть теста в том, что сначала в батч-запрос идёт мастер, а далее идёт сам объект со ссылкой на мастера.
             * Ссылка на мастера не меняется при этом.
             * Необходимо, чтобы при последовательной обработке батчей мастер не был перевычитан и его значение обновилось корректно.
             */
            ActODataService(args =>
            {
                // Arrange.
                string[] porodaPropertiesNames =
                {
                    Information.ExtractPropertyPath<Порода>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Порода>(x => x.Название),
                };
                string[] koshkaPropertiesNames =
                {
                    Information.ExtractPropertyPath<Кошка>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<Кошка>(x => x.Кличка),
                };
                View porodaDynamicView = new View(new ViewAttribute("porodaDynamicView", porodaPropertiesNames), typeof(Порода));
                View koshkaDynamicView = new View(new ViewAttribute("koshkaDynamicView", koshkaPropertiesNames), typeof(Кошка));

                const string InitialName = "Initial";
                const string OtherName = "Other";
                Порода poroda = new Порода() { Название = InitialName };
                Кошка koshka = new Кошка() { Кличка = InitialName, Порода = poroda};
                args.DataService.UpdateObject(koshka);

                Порода poroda1 = args.DataService.Query<Порода>(porodaDynamicView).FirstOrDefault(x => x.__PrimaryKey == poroda.__PrimaryKey);
                Кошка koshka1 = args.DataService.Query<Кошка>(koshkaDynamicView).FirstOrDefault(x => x.__PrimaryKey == koshka.__PrimaryKey);
                Assert.NotNull(poroda1);
                Assert.NotNull(koshka1);

                poroda.Название = OtherName;
                koshka.Кличка = OtherName;

                string requestJsonDatakoshka = koshka.ToJson(koshkaDynamicView, args.Token.Model);
                DataObjectDictionary objJsonKoshka = DataObjectDictionary.Parse(requestJsonDatakoshka, koshkaDynamicView, args.Token.Model);

                objJsonKoshka.Add(
                    $"{nameof(Кошка.Порода)}@odata.bind",
                    string.Format(
                        "{0}({1})",
                        args.Token.Model.GetEdmEntitySet(typeof(Порода)).Name,
                        ((KeyGuid)poroda.__PrimaryKey).Guid.ToString("D")));

                requestJsonDatakoshka = objJsonKoshka.Serialize();

                const string baseUrl = "http://localhost/odata";
                string[] changesets = new[] // Важно, чтобы сначала шёл мастер, потом объект, имеющий на него ссылку.
                {

                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Порода)).Name}",
                        poroda.ToJson(porodaDynamicView, args.Token.Model),
                        poroda),
                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(Кошка)).Name}",
                        requestJsonDatakoshka,
                        koshka),
                };

                // Act.
                HttpRequestMessage batchRequest = CreateBatchRequest(baseUrl, changesets);
                using (HttpResponseMessage response = args.HttpClient.SendAsync(batchRequest).Result)
                {
                    // Assert.
                    CheckODataBatchResponseStatusCode(response, new HttpStatusCode[] { HttpStatusCode.OK, HttpStatusCode.OK });
                    Порода poroda2 = args.DataService.Query<Порода>(porodaDynamicView).FirstOrDefault(x => x.__PrimaryKey == poroda.__PrimaryKey);
                    Кошка koshka2 = args.DataService.Query<Кошка>(koshkaDynamicView).FirstOrDefault(x => x.__PrimaryKey == koshka.__PrimaryKey);
                    Assert.NotNull(poroda2);
                    Assert.NotNull(koshka2);
                    Assert.Equal(OtherName, poroda2.Название);
                    Assert.Equal(OtherName, koshka2.Кличка);
                }
            });
        }

        /// <summary>
        /// Test batch update with inheritance.
        /// It checks that dataobject cache is not crashed.
        /// </summary>
        [Fact]
        public void UpdateWithInheritanceAndDetailsTest()
        {
            /* Суть теста в том, что есть класс А, у него детейл Б, у которого есть наследник В.
             * При загрузке объекта класса А подгрузятся его детейлы, однако они будут подгружены по представлению, которое соответствует классу Б, даже если детейлы класса В.
             * Таким образом, в кэше окажутся объекты класса В, которые загружены только по свойствам Б. Раз не все свойства подгружены, то состояние LightLoaded.
             * Догружать необходимо только те свойства, что ещё не загружались (потому что загруженные уже могут быть изменены).
             *
             * Необходимо, чтобы при последовательной обработке батча все значения обновились корректно.
             */
            ActODataService(args =>
            {
                // Arrange.
                const string InitialName = "Initial";
                const string OtherName = "Other";
                TestConfiguration testConfiguration = new TestConfiguration() { Name = InitialName };
                FirstLevel first = new FirstLevel() { Name = InitialName, TestConfiguration = testConfiguration };
                TestClass second1 = new TestClass { Name = InitialName, FirstLevel = first };
                TestAssociation second2 = new TestAssociation { Name = InitialName, FirstLevel = first, SecondLevel1 = second1 };
                ThirdLevel third = new ThirdLevel { Name = InitialName, TestClass = second1 };
                DataObject[] updateObjects = new DataObject[] { testConfiguration, first, second1, second2, third };
                args.DataService.UpdateObjects(ref updateObjects);

                second2.Name = OtherName; // Изменение значения детейла одного типа, который имеет мастеровую ссылку на детейл второго типа (второй тип имеет детейл собственный).
                ThirdLevel third2 = new ThirdLevel { Name = OtherName, TestClass = second1 }; // Добавление детейлов в детейл второго типа.

                string[] firstPropertiesNames =
                {
                    Information.ExtractPropertyPath<FirstLevel>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<FirstLevel>(x => x.Name),
                };
                View firstLevelDynamicView = new View(new ViewAttribute("firstLevelDynamicView", firstPropertiesNames), typeof(FirstLevel));

                string[] second1PropertiesNames =
                {
                    Information.ExtractPropertyPath<TestClass>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<TestClass>(x => x.Name),
                };
                View second1DynamicView = new View(new ViewAttribute("second1DynamicView", second1PropertiesNames), typeof(TestClass));

                string[] second2PropertiesNames =
                {
                    Information.ExtractPropertyPath<TestAssociation>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<TestAssociation>(x => x.Name),
                };
                View second2DynamicView = new View(new ViewAttribute("second2DynamicView", second2PropertiesNames), typeof(TestAssociation));

                string[] thirdPropertiesNames =
                {
                    Information.ExtractPropertyPath<ThirdLevel>(x => x.__PrimaryKey),
                    Information.ExtractPropertyPath<ThirdLevel>(x => x.Name),
                };
                View thirdLevelDynamicView = new View(new ViewAttribute("thirdDynamicView", thirdPropertiesNames), typeof(ThirdLevel));

                // Операция изменения детейла второго типа (он попадает в батч-запрос как агрегатор к добавляемому детейлу второго уровня).
                string requestJsonDataSecond1 = second1.ToJson(second1DynamicView, args.Token.Model);
                DataObjectDictionary objJsonSecond1 = DataObjectDictionary.Parse(requestJsonDataSecond1, second1DynamicView, args.Token.Model);
                objJsonSecond1.Add( // Добавляется ссылка на агрегатор.
                    $"{nameof(TestClass.FirstLevel)}@odata.bind",
                    string.Format("{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(FirstLevel)).Name, ((KeyGuid)first.__PrimaryKey).Guid.ToString("D")));
                requestJsonDataSecond1 = objJsonSecond1.Serialize();

                // Операция вставки детейла второго уровня.
                string requestJsonDataThird2 = third2.ToJson(thirdLevelDynamicView, args.Token.Model);
                DataObjectDictionary objJsonThird2 = DataObjectDictionary.Parse(requestJsonDataThird2, thirdLevelDynamicView, args.Token.Model);
                objJsonThird2.Add( // Добавляется ссылка на агрегатор.
                    $"{nameof(ThirdLevel.TestClass)}@odata.bind",
                    string.Format("{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(TestClass)).Name, ((KeyGuid)second1.__PrimaryKey).Guid.ToString("D")));
                requestJsonDataThird2 = objJsonThird2.Serialize();

                // Операция изменения детейла первого типа.
                string requestJsonDataSecond2 = second2.ToJson(second2DynamicView, args.Token.Model);
                DataObjectDictionary objJsonSecond2 = DataObjectDictionary.Parse(requestJsonDataSecond2, second2DynamicView, args.Token.Model);
                objJsonSecond2.Add( // Добавляется ссылка на агрегатор.
                    $"{nameof(TestAssociation.FirstLevel)}@odata.bind",
                    string.Format("{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(FirstLevel)).Name, ((KeyGuid)first.__PrimaryKey).Guid.ToString("D")));
                objJsonSecond2.Add( // Добавляется ссылка мастеровая на другой детейл.
                    $"{nameof(TestAssociation.SecondLevel1)}@odata.bind",
                    string.Format("{0}({1})", args.Token.Model.GetEdmEntitySet(typeof(TestClass)).Name, ((KeyGuid)second1.__PrimaryKey).Guid.ToString("D")));
                requestJsonDataSecond2 = objJsonSecond2.Serialize();

                const string baseUrl = "http://localhost/odata";
                string[] changesets = new[]
                {

                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(TestClass)).Name}",
                        requestJsonDataSecond1,
                        second1),
                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(ThirdLevel)).Name}",
                        requestJsonDataThird2,
                        third2),
                    CreateChangeset(
                        $"{baseUrl}/{args.Token.Model.GetEdmEntitySet(typeof(TestAssociation)).Name}",
                        requestJsonDataSecond2,
                        second2),
                };
                HttpRequestMessage batchRequest = CreateBatchRequest(baseUrl, changesets);

                // Код для удобства отлавливания исключений.
                args.Token.Events.CallbackAfterInternalServerError = (Exception exception, ref HttpStatusCode code) =>
                {
                    Exception currentException = exception;

                    while (currentException != null)
                    {
                        currentException = currentException.InnerException;
                    }

                    return exception;
                };

                // Act.
                using (HttpResponseMessage response = args.HttpClient.SendAsync(batchRequest).Result)
                {
                    // Assert.
                    CheckODataBatchResponseStatusCode(response, new HttpStatusCode[] { HttpStatusCode.OK, HttpStatusCode.Created, HttpStatusCode.OK });

                    string[] thirdPropertiesNames2 =
                    {
                        Information.ExtractPropertyPath<ThirdLevel>(x => x.__PrimaryKey),
                        Information.ExtractPropertyPath<ThirdLevel>(x => x.Name),
                        Information.ExtractPropertyPath<ThirdLevel>(x => x.TestClass),
                    };
                    View thirdLevelDynamicView2 = new View(new ViewAttribute("thirdDynamicView2", thirdPropertiesNames2), typeof(ThirdLevel));
                    List<ThirdLevel> thirdLevelList = args.DataService.Query<ThirdLevel>(thirdLevelDynamicView2).Where(x => x.TestClass.__PrimaryKey == second1.__PrimaryKey).ToList();
                    Assert.NotNull(thirdLevelList);
                    Assert.True(thirdLevelList.Any());
                    Assert.Equal(2, thirdLevelList.Count);

                    TestAssociation checkAssociation = args.DataService.Query<TestAssociation>(second2DynamicView).FirstOrDefault(x => x.__PrimaryKey == second2.__PrimaryKey);
                    Assert.NotNull(checkAssociation);
                    Assert.Equal(OtherName, checkAssociation.Name);
                }
            });
        }
    }
}

