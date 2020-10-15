namespace NewPlatform.Flexberry.ORM.ODataService.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using HttpClientExtensions = NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions.HttpClientExtensions;

    /// <summary>
    /// The type stub.
    /// </summary>
    internal class CustomHttpClient : HttpClient
    {
        public CustomHttpClient() : base()
        {
        }

        public CustomHttpClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
        {
        }

        //public new Task<HttpResponseMessage> GetAsync(string requestUri)
        //{
        //    requestUri = HttpClientExtensions.GetCustomUrl(this, requestUri);
        //    return base.GetAsync(requestUri);
        //}
    }
}
