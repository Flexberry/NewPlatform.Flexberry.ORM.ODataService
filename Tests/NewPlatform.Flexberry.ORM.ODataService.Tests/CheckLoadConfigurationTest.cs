namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;

    using Microsoft.Practices.Unity.Configuration;
    using Unity;
    using Unity.Injection;
    using Xunit;

    /// <summary>
    /// Class for checking that a dependency registered in the code can be overriden from the config.
    /// </summary>
    public class CheckLoadConfigurationTest
    {
        [Fact]
        public void CheckIfLoadConfigurationWorks()
        {
            using var container = new UnityContainer();

            // Default culture
            container.RegisterType<IFormatProvider, CultureInfo>(
                new InjectionConstructor("en-US"));

            // This type is registered in code only. Just to prove its registration survives.
            container.RegisterType<IList, ArrayList>();

            Assert.Equal("$1,234.56", GetFormattedSum(container.Resolve<IFormatProvider>(), 1234.56M));
            string beforeLoad = GetCodeRegistration(container);

            // Override with the config file
            if (ConfigurationManager.GetSection("unity") != null)
            {
                container.LoadConfiguration();
                Assert.Equal("1.234,56 kr.", GetFormattedSum(container.Resolve<IFormatProvider>(), 1234.56M));

                string afterLoad = GetCodeRegistration(container);

                Assert.Equal(beforeLoad, afterLoad);
            }
        }

        private static string GetFormattedSum(IFormatProvider fp, decimal money)
        {
            var fi = fp.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
            return string.Format(fi, "{0:C}", money);
        }

        private static string GetCodeRegistration(IUnityContainer container)
        {
            var reg = container.Registrations.Single(r => r.RegisteredType == typeof(IList));
            return reg.MappedToType.Name;
        }
    }
}
