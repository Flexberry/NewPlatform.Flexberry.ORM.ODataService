namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Tests;

    public class DataObjectEdmModelTest : BaseODataServiceIntegratedTest
    {
#if NETCOREAPP
        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        /// <param name="factory">Фабрика для приложения.</param>
        /// <param name="output">Вывод отладочной информации.</param>
        public DataObjectEdmModelTest(CustomWebApplicationFactory<ODataServiceSample.AspNetCore.Startup> factory, Xunit.Abstractions.ITestOutputHelper output)
            : base(factory, output)
        {
        }
#endif

        [Fact]
        public void TestGetDerivedTypes()
        {
            var model = new DataObjectEdmModel(new DataObjectEdmMetadata());

            IList<Type> derivedTypes = model.GetDerivedTypes(typeof(Лес)).ToList();

            Assert.NotNull(derivedTypes);
            Assert.Equal(1, derivedTypes.Count);
            Assert.Equal(typeof(Лес), derivedTypes.First());
        }

        /// <summary>
        /// Checks for an exception that occurs when the PublishName are the same.
        /// </summary>
        [Fact(Skip = "Test will pass if two objects have the same PublishName.")]
        public void TestCheckIdenticalPublishName()
        {
            // Arrange
            var expectedException = typeof(Exception);

            // Act
            var exception = Assert.Throws<Exception>(() => ActODataService(args => { }));

            // Assert
            Assert.Equal(exception.GetType(), expectedException);
        }
    }
}
