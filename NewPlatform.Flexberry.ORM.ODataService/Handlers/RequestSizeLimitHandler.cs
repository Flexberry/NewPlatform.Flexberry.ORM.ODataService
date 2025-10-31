namespace NewPlatform.Flexberry.ORM.ODataService.Handlers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNet.OData.Batch;

#if NETFRAMEWORK
    /// <summary>
    /// Определяет класс обработчика http-запроса (http request handler), который в случае, если данный запрос превышает
    /// максимально допустимую длину, то выбрасывается исключение:
    /// HttpStatusCode.RequestEntityTooLarge. Работает для .NET Framework.
    /// </summary>
    public class RequestSizeLimitHandler : DelegatingHandler
    {
        public const long MaxRequestSize = 20 * 1024 * 1024; // TODO сделать считывание из конфига

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           var checkResult = CheckRequestSize(request);

           if (checkResult != null)
                return checkResult;

           return await base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Проверяет Content-Length запроса и возвращает HttpResponseMessage,
        /// если размер превышает лимит.
        /// </summary>
        protected virtual HttpResponseMessage CheckRequestSize(HttpRequestMessage request)
        {
            if (request?.Content?.Headers?.ContentLength == null)
                return null;

            long length = request.Content.Headers.ContentLength.Value;

            if (length > MaxRequestSize)
            {
                return new HttpResponseMessage(HttpStatusCode.RequestEntityTooLarge)
                {
                    ReasonPhrase = $"Request length {length} exceeds maximum allowed request length {MaxRequestSize} bytes.",
                };
            }

            return null;
        }
    }

#else //NETCOREAPP
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware для проверки размера тела запроса в .NET Core.
    /// </summary>
    public class RequestSizeLimitMiddleware
    {
        private readonly RequestDelegate _next;
        public const long MaxRequestSize = 20 * 1024 * 1024; // 20 МБ

        public RequestSizeLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            // Проверяются только запросы с телом.
            if (request.ContentLength.HasValue &&
                (request.Method == HttpMethods.Post ||
                 request.Method == HttpMethods.Put ||
                 request.Method == HttpMethods.Patch))
            {
                if (request.ContentLength.Value > MaxRequestSize)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.RequestEntityTooLarge;
                    await context.Response.WriteAsync(
                        $"Request length {request.ContentLength.Value} exceeds maximum allowed length {MaxRequestSize} bytes.");
                    return;
                }
            }

            await _next(context);
        }
    }
#endif
}