namespace NewPlatform.Flexberry.ORM.ODataService.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    //using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Extensions;
    //using System.Web.OData.Extensions;
    //using System.Web.OData.Query;
    //using System.Web.OData.Routing;
    using Expressions;
    using ICSSoft.STORMNET;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Routing;
    using Microsoft.AspNetCore.Mvc;
    //using Microsoft.OData.Core;
    //using Microsoft.OData.Edm.Library;
    //using Microsoft.OData.Edm.Values;
    using NewPlatform.Flexberry.ORM.ODataService.Formatter;
    using NewPlatform.Flexberry.ORM.ODataService.Functions;
    using NewPlatform.Flexberry.ORM.ODataService.Handlers;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Routing;
    using Action = NewPlatform.Flexberry.ORM.ODataService.Functions.Action;
    using System.Text;
    using Microsoft.OData.UriParser;

    /// <summary>
    /// OData controller class.
    /// Part with OData Service functions.
    /// </summary>
    public partial class DataObjectController
    {
        /// <summary>
        /// Выполняет action.
        /// Имя "PostODataActionsExecute" устанавливается в <see cref="DataObjectRoutingConvention.SelectAction"/>.
        /// </summary>
        /// <param name="parameters">Параметры action.</param>
        /// <returns>
        /// Результат выполнения action, преобразованный к типам сущностей EDM-модели или к примитивным типам.
        /// В случае, если зарегистрированый action не возвращает результат, будет возвращён только код 200 OK.
        /// После преобразования создаётся результат HTTP для ответа.
        /// </returns>
        public IActionResult PostODataActionsExecute(ODataActionParameters parameters)
        {
            try
            {
                QueryOptions = CreateODataQueryOptions(typeof(DataObject));
                return ExecuteAction(parameters);
            }
            catch (HttpResponseException ex)
            {
                if (HasOdataError(ex))
                {
                    return new ResponseMessageResult(ex.Response);
                }
                else
                {
                    return new ResponseMessageResult(InternalServerErrorMessage(ex));
                }
            }
            catch (TargetInvocationException ex)
            {
                if (HasOdataError(ex.InnerException))
                {
                    return new ResponseMessageResult(((HttpResponseException)ex.InnerException).Response);
                }
                else
                {
                    return new ResponseMessageResult(InternalServerErrorMessage(ex));
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(InternalServerErrorMessage(ex));
            }
        }

        private IActionResult ExecuteAction(ODataActionParameters parameters)
        {
            Microsoft.AspNet.OData.Routing.ODataPath odataPath = Request.ODataFeature().Path;
            OperationSegment segment = odataPath.Segments.Last() as OperationSegment;//UnboundActionPathSegment;
            if (segment == null || !_functions.IsRegistered(segment.Identifier)) //ActionName))
            {
                return SetResult("Action not found");
            }

            Action action = _functions.GetFunction(segment.Identifier) as Action;
            if (action == null)
            {
                return SetResult("Action not found");
            }

            QueryParameters queryParameters = new QueryParameters(this);
            queryParameters.Count = null;
            queryParameters.Request = new HttpRequestMessage((HttpMethod)Enum.Parse(typeof(HttpMethod), Request.Method, true), Request.QueryString.ToString());
            //Request;
            byte[] body = new byte[(long)Request.ContentLength];
            Request.Body.Read(body, 0,(int)Request.ContentLength);
            queryParameters.RequestBody = Encoding.ASCII.GetString(body);

                //(string)Request.Properties[PostPatchHandler.RequestContent];
            var result = action.Handler(queryParameters, parameters);
            if (action.ReturnType == typeof(void))
            {
                return Ok();
            }

            if (result == null)
            {
                return SetResult("Result is null.");
            }

            if (result is DataObject)
            {
                var entityType = _model.GetEdmEntityType(result.GetType());
                return SetResult(GetEdmObject(entityType, result, 1, null));
            }

            if (!(result is string) && result is IEnumerable)
            {
                Type type = null;
                if (result.GetType().IsGenericType)
                {
                    Type[] args = result.GetType().GetGenericArguments();
                    if (args.Length == 1)
                        type = args[0];
                }

                if (result.GetType().IsArray)
                {
                    type = result.GetType().GetElementType();
                }

                if (type != null && (type.IsSubclassOf(typeof(DataObject)) || type == typeof(DataObject)))
                {
                    var coll = GetEdmCollection((IEnumerable)result, type, 1, null);
                    return SetResult(coll);
                }
            }

            return SetResultPrimitive(result.GetType(), result);
        }
    }
}