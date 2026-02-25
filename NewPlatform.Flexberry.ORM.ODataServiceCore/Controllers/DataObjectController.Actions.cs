namespace NewPlatform.Flexberry.ORM.ODataService.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.OData;
    using Microsoft.OData.UriParser;
    using NewPlatform.Flexberry.ORM.ODataService.Functions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Middleware;

    using Action = Functions.Action;
    using ODataPath = Microsoft.AspNet.OData.Routing.ODataPath;

    /// <summary>
    /// The <see cref="DataObject"/> OData controller class.
    /// The ODataService actions part.
    /// </summary>
    public partial class DataObjectController
    {
        /// <summary>
        /// Выполняет action.
        /// Имя "PostODataActionsExecute" устанавливается в <see cref="Routing.Conventions.DataObjectRoutingConvention.SelectActionImpl"/>.
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
                try
                {
                    QueryOptions = CreateODataQueryOptions(typeof(DataObject));
                    return ExecuteAction(parameters);
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
        /// Выполняет зарегистрированный OData action и возвращает результат.
        /// </summary>
        private IActionResult ExecuteAction(ODataActionParameters parameters)
        {
            ODataPath odataPath = Request.HttpContext.ODataFeature().Path;

            // OperationImportSegment представляет UnboundActionPathSegment в OData v5.7.0
            OperationImportSegment segment = odataPath.Segments[odataPath.Segments.Count - 1] as OperationImportSegment;

            if (segment == null || !Functions.IsRegistered(segment.Identifier))
            {
                return Ok("Action not found");
            }

            Action action = Functions.GetFunction(segment.Identifier) as Action;
            if (action == null)
            {
                return Ok("Action not found");
            }

            QueryParameters queryParameters = new QueryParameters(this);
            queryParameters.Count = null;
            queryParameters.Request = Request;
            queryParameters.RequestBody = (string)Request.HttpContext.Items[RequestHeadersHookMiddleware.PropertyKeyRequestContent];
            object result = action.Handler(queryParameters, parameters);
            if (action.ReturnType == typeof(void))
            {
                return Ok();
            }

            if (result == null)
            {
                return Ok("Result is null.");
            }

            if (result is DataObject dataObject)
            {
                DynamicView dynamicView = ApplyAutoExpand(result.GetType(), parameters, dataObject);
                Microsoft.OData.Edm.IEdmEntityType entityType = EdmModel.GetEdmEntityType(result.GetType());
                return Ok(GetEdmObject(entityType, result, 1, null, dynamicView));
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
                    DataObject firstObject = null;
                    if (result is IEnumerable enumerable)
                    {
                        firstObject = enumerable.Cast<DataObject>().FirstOrDefault();
                    }

                    DynamicView dynamicView = ApplyAutoExpand(type, parameters, firstObject);
                    IEnumerable coll = GetEdmCollection((IEnumerable)result, type, 1, null, dynamicView);
                    return Ok(coll);
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Получает текущий SelectExpandClause из запроса.
        /// </summary>
        private SelectExpandClause GetSelectExpandClause()
        {
            return HttpContext.ODataFeature().SelectExpandClause;
        }

        /// <summary>
        /// Устанавливает SelectExpandClause для запроса.
        /// </summary>
        private void SetSelectExpandClause(SelectExpandClause clause)
        {
            HttpContext.ODataFeature().SelectExpandClause = clause;
        }

        /// <summary>
        /// Применяет auto-expand к результату action и возвращает DynamicView.
        /// Если auto-expand не запрошен - возвращает null.
        /// </summary>
        private DynamicView ApplyAutoExpand(Type objectType, ODataActionParameters parameters, DataObject dataObject)
        {
            string odataQuery = ProcessAutoExpand(objectType, parameters, dataObject);
            if (string.IsNullOrEmpty(odataQuery))
                return null;

            ODataQueryOptions previousQueryOptions = QueryOptions;
            SelectExpandClause previousSelectExpandClause = GetSelectExpandClause();

            try
            {
                QueryOptions = CreateQueryOptionsFromExpand(objectType, odataQuery);

                if (QueryOptions.SelectExpand?.SelectExpandClause != null)
                {
                    SetSelectExpandClause(QueryOptions.SelectExpand.SelectExpandClause);
                }

                type = objectType;
                CreateDynamicView();
                return _dynamicView;
            }
            catch (Exception ex)
            {
                QueryOptions = previousQueryOptions;
                SetSelectExpandClause(previousSelectExpandClause);
                _dynamicView = null;

                LogService.LogError($"Failed to apply auto-expand '{odataQuery}': {ex.Message}", ex);
                return null;
            }
        }

        /// <summary>
        /// Обрабатывает параметр __autoExpand для автоматического разворачивания загруженных мастеров.
        /// Возвращает строку $expand или null если auto-expand не запрошен.
        /// </summary>
        private string ProcessAutoExpand(Type objectType, ODataActionParameters parameters, DataObject dataObject = null)
        {
            string autoExpand = Request.Query["__autoExpand"].ToString();
            if (string.IsNullOrEmpty(autoExpand) && parameters != null && parameters.ContainsKey("__autoExpand"))
            {
                autoExpand = parameters["__autoExpand"]?.ToString();
            }

            if (!string.IsNullOrEmpty(autoExpand) && autoExpand.ToLowerInvariant() == "true" && dataObject != null)
            {
                string autoExpandQuery = AutoExpander.BuildExpandQuery(dataObject, (Type type, string prop) => EdmModel?.GetEdmTypePropertyName(type, prop));
                if (!string.IsNullOrEmpty(autoExpandQuery))
                {
                    LogService.LogDebug($"Auto-expanding masters for {objectType.Name}: {autoExpandQuery}");
                    return autoExpandQuery;
                }
            }

            return null;
        }
    }
}
