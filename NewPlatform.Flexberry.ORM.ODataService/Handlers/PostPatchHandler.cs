#if NETFRAMEWORK
namespace NewPlatform.Flexberry.ORM.ODataService.Handlers
{
    using Microsoft.AspNet.OData.Batch;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Определяет класс обработчика http-запроса (http request handler), который в случае, если данный запрос является
    /// POST или PATCH сохраняет тело запроса в свойствах данного запроса. В дальнейшем тело запроса будет использовано в методе
    /// DataObjectController.ReplaceOdataBindNull().
    /// </summary>
    public class PostPatchHandler : DelegatingHandler
    {
        /// <summary>
        /// Строковая константа, которая используется для доступа свойствам запроса.
        /// </summary>
        public const string RequestContent = "PostPatchHandler_RequestContent";

        /// <summary>
        /// Ключ к объекту в свойствах запроса, указывающий на то, что это часть batch-запроса.
        /// </summary>
        public const string PropertyKeyBatchRequest = "MS_BatchRequest";

        /// <summary>
        /// Ключ к объекту в свойствах запроса, указывающий на Id контента batch-запроса.
        /// </summary>
        public const string PropertyKeyContentId = "ContentId";

        /// <summary>
        /// Строковая константа, которая используется для доступа к оригинальному заголовку запроса Accept.
        /// </summary>
        public const string AcceptApplicationMsExcel = "PostPatchHandler_AcceptApplicationMsExcel";

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            foreach (var val in request.Headers.Accept)
            {
                if (val.MediaType == "application/ms-excel")
                {
                    request.Properties.Add(AcceptApplicationMsExcel, true);
                    break;
                }
            }

            request.Headers.Accept.Clear();
            request.Headers.Remove("X-Requested-With");

            string key = RequestContent;
            if (request.Properties.ContainsKey(PropertyKeyBatchRequest) && (bool)request.Properties[PropertyKeyBatchRequest] == true)
            {
                key = RequestContent + $"_{PropertyKeyContentId}_{request.Properties[PropertyKeyContentId]}";
            }

            if (request.Method.Method == "POST" || request.Method.Method == "PATCH")
            {
                request.Properties.Add(key, request.Content.ReadAsStringAsync().Result);
            }

            /*
            /// Исправление для Mono, взято из https://github.com/OData/odata.net/issues/165
            */
            if (!request.Headers.Contains("Accept-Charset"))
                request.Headers.Add("Accept-Charset", new[] { "utf-8" });

            HttpResponseMessage responseMessage = await base.SendAsync(request, cancellationToken);
            await RewriteResponse(responseMessage);
            return responseMessage;

        }

        /// <summary>
        /// Удаление некорректных символов в именах типов метаданных.
        /// Данные символы добавляются специально, чтобы MS не бросало исключение на пустой Namespace у типов с PublishName.
        /// </summary>
        /// <param name="response">Текущее ответное сообщение, в котором могут быть заменены символы.</param>
        /// <returns>Задача на обработку.</returns>
        private static async Task RewriteResponse(HttpResponseMessage response)
        {
            /* Same code for NETSTANDARD is placed on NewPlatform.Flexberry.ORM.ODataService.Extensions.ODataApplicationBuilderExtensions.*/
            string contentType = response?.Content?.Headers?.ContentType?.MediaType;
            if (!string.IsNullOrEmpty(contentType) &&
            (contentType.Contains("application/json")
            || contentType.Contains("application/xml")
            || contentType.Contains("multipart/mixed")))
            {
                HttpContent content = response.Content;
                Stream contentStream = await content.ReadAsStreamAsync();
                using StreamReader sr = new StreamReader(contentStream);
                string responseStr = sr.ReadToEnd();

                responseStr = responseStr
                   .Replace("(____.", "(")
                   .Replace("\"____.", "\"")
                   .Replace("____.", ".")
                   .Replace(" Namespace=\"____\"", " Namespace=\"\"");

                using (MemoryStream newStream = new MemoryStream())
                {
                    using StreamWriter sw = new StreamWriter(newStream);
                    sw.Write(responseStr);
                    sw.Flush();
                    newStream.Seek(0, SeekOrigin.Begin);

                    contentStream.Position = 0;
                    contentStream.SetLength(0);

                    await newStream.CopyToAsync(contentStream);
                }
            }
        }
    }
}
#endif
