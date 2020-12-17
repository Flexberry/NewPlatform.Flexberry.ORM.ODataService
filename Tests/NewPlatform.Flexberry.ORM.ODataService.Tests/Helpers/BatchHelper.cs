namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Helpers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.KeyGen;

#if NETFRAMEWORK
    using Microsoft.AspNet.OData.Batch;
#endif

    using Xunit;

    /// <summary>
    /// Помощник при работе с Batch-запросами.
    /// </summary>
    public static class BatchHelper
    {
        /// <summary>
        /// Создать тело подзапроса.
        /// </summary>
        /// <param name="url">Адрес подзапроса.</param>
        /// <param name="body">Основное тело подзапроса.</param>
        /// <param name="dataObject">Объект данных.</param>
        /// <returns>Тело подзапроса.</returns>
        public static string CreateChangeset(string url, string body, DataObject dataObject)
        {
            var changeset = new StringBuilder();

            changeset.AppendLine($"{GetMethodAndUrl(dataObject, url)} HTTP/1.1");
            changeset.AppendLine($"Content-Type: application/json;type=entry");
            changeset.AppendLine($"Prefer: return=representation");
            changeset.AppendLine();

            changeset.AppendLine(body);

            return changeset.ToString();
        }

        /// <summary>
        /// Создать batch-запрос.
        /// </summary>
        /// <param name="url">Базовый адрес odata-сервиса.</param>
        /// <param name="changesets">Набор подзапросов.</param>
        /// <returns>Сконфигурированный <see cref="HttpRequestMessage" /> содержащий batch-запрос.</returns>
        public static HttpRequestMessage CreateBatchRequest(string url, string[] changesets)
        {
            string boundary = $"batch_{Guid.NewGuid()}";
            string body = CreateBatchBody(boundary, changesets);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{url}/$batch"),
                Method = new HttpMethod("POST"),
                Content = new StringContent(body),
            };

            request.Content.Headers.ContentType.MediaType = "multipart/mixed";
            request.Content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("boundary", boundary));

            return request;
        }

        public static void CheckODataBatchResponseStatusCode(HttpResponseMessage response, HttpStatusCode[] statusCodes)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
#if NETFRAMEWORK
            int i = 0;
            ODataBatchContent content = response.Content as ODataBatchContent;
            foreach (ChangeSetResponseItem changeSetResponseItem in content.Responses)
            {
                foreach (HttpResponseMessage httpResponseMessage in changeSetResponseItem.Responses)
                {
                    Assert.Equal(statusCodes[i++], httpResponseMessage.StatusCode);
                }
            }
#endif
        }

        private static string CreateBatchBody(string boundary, string[] changesets)
        {
            var body = new StringBuilder($"--{boundary}");
            body.AppendLine();

            string changesetBoundary = $"changeset_{Guid.NewGuid()}";

            body.AppendLine($"Content-Type: multipart/mixed;boundary={changesetBoundary}");
            body.AppendLine();

            for (var i = 0; i < changesets.Length; i++)
            {
                body.AppendLine($"--{changesetBoundary}");
                body.AppendLine($"Content-Type: application/http");
                body.AppendLine($"Content-Transfer-Encoding: binary");
                body.AppendLine($"Content-ID: {i + 1}");
                body.AppendLine();

                body.AppendLine(changesets[i]);
            }

            body.AppendLine($"--{changesetBoundary}--");
            body.AppendLine($"--{boundary}--");
            body.AppendLine();

            return body.ToString();
        }

        private static string GetMethodAndUrl(DataObject dataObject, string url)
        {
            switch (dataObject.GetStatus())
            {
                case ObjectStatus.Created:
                    return $"POST {url}";

                case ObjectStatus.Altered:
                case ObjectStatus.UnAltered:
                    return $"PATCH {url}({((KeyGuid)dataObject.__PrimaryKey).Guid})";

                case ObjectStatus.Deleted:
                    return $"DELETE {url}({((KeyGuid)dataObject.__PrimaryKey).Guid})";

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
