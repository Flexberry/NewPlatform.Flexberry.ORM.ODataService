﻿namespace NewPlatform.Flexberry.ORM.ODataService.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reflection;
    using ICSSoft.STORMNET;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNet.OData.Routing;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.OData;
    using Microsoft.OData.UriParser;
    using NewPlatform.Flexberry.ORM.ODataService.Functions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.WebUtilities;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Middleware;

    using ODataPath = Microsoft.AspNet.OData.Routing.ODataPath;

    /// <summary>
    /// The <see cref="DataObject"/> OData controller class.
    /// The ODataService functions part.
    /// </summary>
    public partial class DataObjectController
    {
        /// <summary>
        /// The container with OData Service functions.
        /// </summary>
        private IFunctionContainer Functions => ManagementToken?.Functions;

        /// <summary>
        /// Выполняет пользовательскую функцию.
        /// Имя "GetODataFunctionsExecute" устанавливается в <see cref="Routing.Conventions.DataObjectRoutingConvention.SelectActionImpl"/>.
        /// </summary>
        /// <returns>
        /// Результат выполнения пользовательской функции, преобразованный к типам сущностей EDM-модели или к примитивным типам.
        /// После преобразования создаётся результат HTTP для ответа.
        /// </returns>
        [CustomEnableQuery]
        public IActionResult GetODataFunctionsExecute()
        {
            try
            {
                try
                {
                    QueryOptions = CreateODataQueryOptions(typeof(DataObject));
                    return ExecuteUserFunction(new QueryParameters(this));
                }
                catch (ODataException oDataException)   
                {
                    return BadRequest(new ODataError() { ErrorCode = StatusCodes.Status400BadRequest.ToString(), Message = oDataException.Message });
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException is ODataException oDataException)
                    {
                        return BadRequest(new ODataError() { ErrorCode = StatusCodes.Status400BadRequest.ToString(), Message = oDataException.Message });
                    }

                    throw;
                }
            }
            catch (Exception ex)
            {
                throw CustomException(ex);
            }
        }

        /// <summary>
        /// Выполняет пользовательскую функцию.
        /// </summary>
        /// <param name="queryParameters">Параметры запроса.</param>
        /// <returns>Результат выполнения пользовательской функции, преобразованный к типам сущностей EDM-модели или к примитивным типам.</returns>
        internal IActionResult ExecuteUserFunction(QueryParameters queryParameters)
        {
            queryParameters.Count = null;
            queryParameters.Request = Request;
            ODataPath odataPath = Request.HttpContext.ODataFeature().Path;

            // The OperationImportSegment type represents the Microsoft OData v5.7.0 UnboundFunctionPathSegment type here.
            OperationImportSegment segment = odataPath.Segments[odataPath.Segments.Count - 1] as OperationImportSegment;

            // The OperationImportSegment.Identifier property represents the Microsoft OData v5.7.0 UnboundFunctionPathSegment.FunctionName property here.
            if (segment == null || !Functions.IsRegistered(segment.Identifier))
                return Ok("Function not found");

            Function function = Functions.GetFunction(segment.Identifier);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (string parameterName in function.ParametersTypes.Keys)
            {
                try
                {
                    var parameterValue = segment.GetParameterValue(parameterName);
                    if (parameterValue is ODataEnumValue enumParameterValue)
                    {
                        parameterValue = Enum.Parse(function.ParametersTypes[parameterName], enumParameterValue.Value);
                    }

                    parameters.Add(parameterName, parameterValue);
                }
                catch (Exception ex)
                {
                    throw new ODataException($"Failed to convert parameter: {parameterName}", ex);
                }
            }

            var result = function.Handler(queryParameters, parameters);
            if (result == null)
            {
                return Ok("Result is null.");
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
                    var queryOpt = CreateODataQueryOptions(type);

                    QueryOptions = queryOpt;
                    if (QueryOptions.SelectExpand != null && QueryOptions.SelectExpand.SelectExpandClause != null)
                    {
                        Request.HttpContext.ODataFeature().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
                    }

                    this.type = type;
                    CreateDynamicView();
                    IncludeCount = false;
                    if (queryOpt.Count != null && queryOpt.Count.Value)
                    {
                        IncludeCount = true;
                        if (queryParameters.Count != null)
                        {
                            Count = (int)queryParameters.Count;
                        }
                        else
                        {
                            Count = GetObjectsCount(type, queryOpt);
                        }
                    }

                    NameValueCollection queryParams = QueryHelpers.QueryToNameValueCollection(Request.Query);

                    if ((EdmModel.ExportService != null || EdmModel.ODataExportService != null)
                        && (Request.HttpContext.Items.ContainsKey(RequestHeadersHookMiddleware.AcceptApplicationMsExcel) || Convert.ToBoolean(queryParams["exportExcel"])))
                    {
                            _objs = (result as IEnumerable).Cast<DataObject>().ToArray();
                            return CreateExcel(queryParams);
                    }

                    var coll = GetEdmCollection((IEnumerable)result, type, 1, null, _dynamicView);
                    return Ok(coll);
                }

                return Ok(result);
            }

            if (result is DataObject)
            {
                QueryOptions = CreateODataQueryOptions(result.GetType());
                if (QueryOptions.SelectExpand != null && QueryOptions.SelectExpand.SelectExpandClause != null)
                {
                    Request.HttpContext.ODataFeature().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
                }

                this.type = result.GetType();
                CreateDynamicView();
                var entityType = EdmModel.GetEdmEntityType(this.type);
                return Ok(GetEdmObject(entityType, result, 1, null, _dynamicView));
            }

            return Ok(result);
        }
    }
}
