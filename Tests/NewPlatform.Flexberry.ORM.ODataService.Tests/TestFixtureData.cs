#if NETCOREAPP
using ICSSoft.Services;
using ODataServiceSample.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    /// <summary>
    /// Data class for all tests.
    /// </summary>
    public class TestFixtureData : IDisposable
    {
        public CustomWebApplicationFactory<Startup> factory;
        public IUnityContainer unityContainer;

        public TestFixtureData()
        {
            factory = new CustomWebApplicationFactory<Startup>();
            unityContainer = UnityFactory.GetContainer();
        }

        public void Dispose()
        {
            factory.Dispose();
        }
    }
}
#endif