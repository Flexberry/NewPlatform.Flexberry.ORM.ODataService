namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    using NewPlatform.Flexberry.ORM.ODataService.Tests;
    using NewPlatform.Flexberry.ORM.ODataService.Model;

    public class DataObjectEdmModelTest
    {
        [Fact]
        public void TestGetDerivedTypes()
        {
            var model = new DataObjectEdmModel(new DataObjectEdmMetadata());

            IList<Type> derivedTypes = model.GetDerivedTypes(typeof(Лес)).ToList();

            Assert.NotNull(derivedTypes);
            Assert.Equal(1, derivedTypes.Count);
            Assert.Equal(typeof(Лес), derivedTypes.First());
        }
    }
}
