namespace NewPlatform.Flexberry.ORM.ODataService.Batch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using Microsoft.AspNet.OData.Batch;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Batch handler for DataService.
    /// </summary>
    internal class DataObjectODataBatchHandler : DefaultODataBatchHandler
    {
        /// <summary>
        /// DataService instance for execute queries.
        /// </summary>
        private IDataService dataService;

        /// <summary>
        /// Initializes a new instance of the NewPlatform.Flexberry.ORM.ODataService.Batch.DataObjectODataBatchHandler class.
        /// </summary>
        /// <param name="dataService">DataService instance for execute queries.</param>
        public DataObjectODataBatchHandler(IDataService dataService)
            : base()
        {
            this.dataService = dataService;
        }

        /// <summary>
        /// Request Properties collection key for DataObjectsToUpdate list.
        /// </summary>
        public const string DataObjectsToUpdatePropertyKey = "DataObjectsToUpdate";

        /// <inheritdoc />
        public async override Task<IList<ODataBatchResponseItem>> ExecuteRequestMessagesAsync(IEnumerable<ODataBatchRequestItem> requests, RequestDelegate handler)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }

            IList<ODataBatchResponseItem> responses = new List<ODataBatchResponseItem>();
            try
            {
                foreach (ODataBatchRequestItem request in requests)
                {
                    var operation = request as OperationRequestItem;
                    if (operation != null)
                    {
                        responses.Add(await request.SendRequestAsync(Invoker, cancellation));
                    }
                    else
                    {
                        await ExecuteChangeSet((ChangeSetRequestItem)request, responses, cancellation);
                    }
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

        /// <summary>
        /// Execute changeset processing.
        /// </summary>
        /// <param name="changeSet">Changeset for processing.</param>
        /// <param name="responses">Responses for each request.</param>
        /// <param name="cancellation">Cancelation token.</param>
        /// <returns>Task for changeset processing.</returns>
        private async Task ExecuteChangeSet(ChangeSetRequestItem changeSet, IList<ODataBatchResponseItem> responses, CancellationToken cancellation)
        {
            List<DataObject> dataObjectsToUpdate = new List<DataObject>();

            foreach (var context in changeSet.Contexts)
            {
                // TODO: теперь вместо HttpRequestMessage приходит HttpContext. Надо разобраться как правильно его обработать.
                HttpRequestMessage request;
                if (!request.Properties.ContainsKey(DataObjectsToUpdatePropertyKey))
                {
                    request.Properties.Add(DataObjectsToUpdatePropertyKey, dataObjectsToUpdate);
                }
            }

            ChangeSetResponseItem changeSetResponse = (ChangeSetResponseItem)await changeSet.SendRequestAsync(Invoker, cancellation);
            responses.Add(changeSetResponse);

            if (changeSetResponse.Responses.All(r => r.IsSuccessStatusCode))
            {
                DataObject[] dataObjects = dataObjectsToUpdate.ToArray();
                dataService.UpdateObjects(ref dataObjects);
            }
        }
    }
}
