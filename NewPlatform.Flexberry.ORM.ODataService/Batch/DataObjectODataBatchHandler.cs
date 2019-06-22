namespace NewPlatform.Flexberry.ORM.ODataService.Batch
{
    using Microsoft.OData.Core;
    using NewPlatform.Flexberry.ORM.ODataService.Expressions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Batch;
    using System.Web.OData.Batch;

    internal class DataObjectODataBatchHandler : DefaultODataBatchHandler
    {
        public DataObjectODataBatchHandler(HttpServer httpServer) : base(httpServer)
        {
        }

        public override async Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw Error.ArgumentNull("request");
            }

            ValidateRequest(request);

            IList<ODataBatchRequestItem> subRequests = await ParseBatchRequestsAsync(request, cancellationToken);

            //string preferHeader = RequestPreferenceHelpers.GetRequestPreferHeader(request);
            //if ((preferHeader != null && preferHeader.Contains(PreferenceContinueOnError)) || (!request.GetConfiguration().HasEnabledContinueOnErrorHeader()))
            //{
            //    ContinueOnError = true;
            //}
            //else
            //{
            //    ContinueOnError = false;
            //}

            try
            {
                IList<ODataBatchResponseItem> responses = await ExecuteRequestMessagesAsync(subRequests, cancellationToken);
                return await CreateResponseMessageAsync(responses, request, cancellationToken);
            }
            finally
            {
                foreach (ODataBatchRequestItem subRequest in subRequests)
                {
                    request.RegisterForDispose(subRequest.GetResourcesForDisposal());
                    request.RegisterForDispose(subRequest);
                }
            }
        }

        public override async Task<IList<ODataBatchResponseItem>> ExecuteRequestMessagesAsync(IEnumerable<ODataBatchRequestItem> requests, CancellationToken cancellationToken)
        {
            if (requests == null)
            {
                throw Error.ArgumentNull("requests");
            }

            IList<ODataBatchResponseItem> responses = new List<ODataBatchResponseItem>();

            try
            {
                foreach (ODataBatchRequestItem request in requests)
                {
                    ODataBatchResponseItem responseItem = await request.SendRequestAsync(Invoker, cancellationToken);
                    responses.Add(responseItem);
                    
                    //if (responseItem != null && responseItem.IsResponseSuccessful() == false && ContinueOnError == false)
                    //{
                    //    break;
                    //}
                }
            }
            catch
            {
                foreach (ODataBatchResponseItem response in responses)
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
                throw;
            }

            return responses;
        }

        public override async Task<IList<ODataBatchRequestItem>> ParseBatchRequestsAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw Error.ArgumentNull("request");
            }

            ODataMessageReaderSettings oDataReaderSettings = new ODataMessageReaderSettings
            {
                DisableMessageStreamDisposal = true,
                MessageQuotas = MessageQuotas,
                BaseUri = GetBaseUri(request)
            };

            ODataMessageReader reader = await request.Content.GetODataMessageReaderAsync(oDataReaderSettings, cancellationToken);
            request.RegisterForDispose(reader);

            List<ODataBatchRequestItem> requests = new List<ODataBatchRequestItem>();
            ODataBatchReader batchReader = reader.CreateODataBatchReader();
            Guid batchId = Guid.NewGuid();
            while (batchReader.Read())
            {
                if (batchReader.State == ODataBatchReaderState.ChangesetStart)
                {
                    IList<HttpRequestMessage> changeSetRequests = await batchReader.ReadChangeSetRequestAsync(batchId, cancellationToken);
                    foreach (HttpRequestMessage changeSetRequest in changeSetRequests)
                    {
                        changeSetRequest.CopyBatchRequestProperties(request);
                    }
                    requests.Add(new ChangeSetRequestItem(changeSetRequests));
                }
                else if (batchReader.State == ODataBatchReaderState.Operation)
                {
                    HttpRequestMessage operationRequest = await batchReader.ReadOperationRequestAsync(batchId, bufferContentStream: true, cancellationToken: cancellationToken);
                    operationRequest.CopyBatchRequestProperties(request);
                    requests.Add(new OperationRequestItem(operationRequest));
                }
            }

            return requests;
        }
    }
}
