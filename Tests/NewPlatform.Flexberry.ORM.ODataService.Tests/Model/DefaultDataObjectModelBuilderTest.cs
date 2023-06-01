namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Model
{
    using ICSSoft.STORMNET;

    using Xunit;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using Unity;
    using System;
    using ICSSoft.Services;

    public class DefaultDataObjectModelBuilderTest
    {
        private class H1 : DataObject
        {
        }
        private class H2 : H1
        {
        }

        [Fact]
        public void TestDataObjectIsNotRegisteredInEmptyModel()
        {
            var model = new DataObjectEdmModel(new DataObjectEdmMetadata());

            Assert.False(model.IsDataObjectRegistered(typeof(DataObject)));
        }

        [Fact]
        public void TestRegisteringHierarchy()
        {
            IUnityContainer unityContainer = new UnityContainer();
            IServiceProvider serviceProvider = new UnityServiceProvider(unityContainer);

            var builder = new DefaultDataObjectEdmModelBuilder(new[] { GetType().Assembly }, serviceProvider);

            DataObjectEdmModel model = builder.Build();

            Assert.True(model.IsDataObjectRegistered(typeof(DataObject)));
            Assert.True(model.IsDataObjectRegistered(typeof(H1)));
            Assert.True(model.IsDataObjectRegistered(typeof(H2)));
        }
    }
}
