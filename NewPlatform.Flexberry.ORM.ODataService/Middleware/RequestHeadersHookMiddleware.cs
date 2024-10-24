﻿#if NETSTANDARD
namespace NewPlatform.Flexberry.ORM.ODataService.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Определяет компонент middleware, встраиваемый в конвейер обработки http-запросов для модификации заголовков и разделяемых данных запроса.
    /// Если запрос является POST или PATCH, в разделяемых данных такого запроса также сохраняется тело запроса, в дальнейшем используемое
    /// в методе DataObjectController.ReplaceOdataBindNull().
    /// </summary>
    public class RequestHeadersHookMiddleware
    {
        /// <summary>
        /// Строковая константа, которая используется для доступа к телу запроса в разделяемых данных запроса.
        /// </summary>
        public const string PropertyKeyRequestContent = "PostPatchHandler_RequestContent";

        /// <summary>
        /// Строковая константа, которая используется для доступа к оригинальному заголовку запроса Accept в разделяемых данных запроса.
        /// </summary>
        public const string AcceptApplicationMsExcel = "PostPatchHandler_AcceptApplicationMsExcel";

        private readonly RequestDelegate _next;

        /// <summary>
        /// Instantiates a new instance of <see cref="RequestHeadersHookMiddleware"/>.
        /// </summary>
        public RequestHeadersHookMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke the middleware.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Skip if request has been aborted.
            if (context.RequestAborted.IsCancellationRequested)
            {
                await _next.Invoke(context);
            }

            var request = context.Request;

            IEnumerable<string> mimeTypeDirectives = AcceptHeaderParser.MimeTypeDirectivesFromAcceptHeader(request.Headers[HeaderNames.Accept]);
            if (mimeTypeDirectives.Any(x => x.Equals("application/ms-excel", StringComparison.OrdinalIgnoreCase)))
            {
                context.Items.Add(AcceptApplicationMsExcel, true);
            }

            request.Headers[HeaderNames.Accept] = string.Empty; // Clear Accept header.
            request.Headers.Remove("X-Requested-With");

            if (request.Method == "POST" || request.Method == "PATCH")
            {
                request.EnableBuffering();

                string bodyString = await GetBodyString(request);

                context.Items.Add(PropertyKeyRequestContent, bodyString);
            }

            /*
            /// Исправление для Mono, взято из https://github.com/OData/odata.net/issues/165 .
            */
            if (!request.Headers.ContainsKey(HeaderNames.AcceptCharset))
                request.Headers.Add(HeaderNames.AcceptCharset, new[] { "utf-8" });

            await _next.Invoke(context);
        }

        private static async Task<string> GetBodyString(HttpRequest request)
        {
            // Leave the body open so the next middleware can read it.
            using var reader = new System.IO.StreamReader(
                request.Body,
                Encoding.UTF8,
                false,
                1024,
                true);
            string bodyString = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            return bodyString;
        }
    }
}
#endif
