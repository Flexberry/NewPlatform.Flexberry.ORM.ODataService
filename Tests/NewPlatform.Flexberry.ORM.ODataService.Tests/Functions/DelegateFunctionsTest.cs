namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using NewPlatform.Flexberry.ORM.ODataService.Functions;
    using NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions;
    using Newtonsoft.Json;

    using Xunit;

    /// <summary>
    /// Unit test class for OData Service user-defined functions
    /// </summary>
    public class DelegateFunctionsTest : BaseODataServiceIntegratedTest
    {
        /// <summary>
        /// Unit test for <see cref="IFunctionContainer.Register(Delegate)"/>.
        /// Tests the function call without query parameters.
        /// </summary>
        [Fact]
        public void TestFunctionCallWithoutQueryParameters()
        {
            ActODataService(args =>
            {
                args.Token.Functions.Register(new Func<int, int, int>(FunctionAddWithoutQueryParameters));

                string url = "http://localhost/odata/FunctionAddWithoutQueryParameters(a=2,b=2)";
                using (HttpResponseMessage response = args.HttpClient.GetAsyncEx(url).Result)
                {
                    var resultText = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultText);

                    Assert.Equal(4, (int)(long)result["value"]);
                }
            });
        }

        /// <summary>
        /// Unit test for <see cref="IFunctionContainer.Register(Delegate)"/>.
        /// Tests the function call with query parameters.
        /// </summary>
        [Fact]
        public void TestFunctionCallWithQueryParameters()
        {
            ActODataService(args =>
            {
                args.Token.Functions.Register(new Func<QueryParameters, int, int, int>(FunctionAddWithQueryParameters));

                string url = "http://localhost/odata/FunctionAddWithQueryParameters(a=2,b=2)";
                using (HttpResponseMessage response = args.HttpClient.GetAsyncEx(url).Result)
                {
                    var resultText = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultText);

                    Assert.Equal(4, (int)(long)result["value"]);
                }
            });
        }

        private static int FunctionAddWithoutQueryParameters(int a, int b)
        {
            return a + b;
        }

        private static int FunctionAddWithQueryParameters(QueryParameters queryParameters, int a, int b)
        {
            Assert.NotNull(queryParameters);

            return a + b;
        }
    }
}
