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
    using Microsoft.AspNet.OData.Query;
    using Microsoft.OData.UriParser;
    using NewPlatform.Flexberry.ORM.ODataService.Functions;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Routing;

#if NETSTANDARD
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common;
#endif

    using Action = NewPlatform.Flexberry.ORM.ODataService.Functions.Action;

#if NETFRAMEWORK
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.OData.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Handlers;
#endif
#if NETSTANDARD
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.OData;
    using Microsoft.AspNet.OData.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Middleware;
#endif

    /// <summary>
    /// OData controller class.
    /// Part with OData Service functions.
    /// </summary>
    public partial class DataObjectController
    {
        private const string autoExpandParamName = "__autoExpand";

#if NETFRAMEWORK
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
        public IHttpActionResult PostODataActionsExecute(ODataActionParameters parameters)
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
                    return ResponseMessage(ex.Response);
                }
                else
                {
                    return ResponseMessage(InternalServerErrorMessage(ex));
                }
            }
            catch (TargetInvocationException ex)
            {
                if (HasOdataError(ex.InnerException))
                {
                    return ResponseMessage(((HttpResponseException)ex.InnerException).Response);
                }
                else
                {
                    return ResponseMessage(InternalServerErrorMessage(ex));
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(InternalServerErrorMessage(ex));
            }
        }
#elif NETSTANDARD
        /// <summary>
        /// Выполняет action.
        /// Имя "PostODataActionsExecute" устанавливается в <see cref="DataObjectRoutingConvention.SelectActionImpl"/>.
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
#endif

#if NETFRAMEWORK
        private IHttpActionResult ExecuteAction(ODataActionParameters parameters)
#elif NETSTANDARD
        private IActionResult ExecuteAction(ODataActionParameters parameters)
#endif
        {
            // OperationImportSegment представляет UnboundActionPathSegment в OData v5.7.0
            OperationImportSegment segment = ODataPath.Segments[ODataPath.Segments.Count - 1] as OperationImportSegment;

            if (segment == null || !_functions.IsRegistered(segment.Identifier))
            {
                string msg = "Action not found";
#if NETFRAMEWORK
                return SetResult(msg);
#elif NETSTANDARD
                return Ok(msg);
#endif
            }

            Action action = _functions.GetFunction(segment.Identifier) as Action;
            if (action == null)
            {
                string msg = "Action not found";
#if NETFRAMEWORK
                return SetResult(msg);
#elif NETSTANDARD
                return Ok(msg);
#endif
            }

            QueryParameters queryParameters = new QueryParameters(this);
            queryParameters.Count = null;
            queryParameters.Request = Request;
#if NETFRAMEWORK
            queryParameters.RequestBody = (string)Request.Properties[PostPatchHandler.RequestContent];
#elif NETSTANDARD
            queryParameters.RequestBody = (string)Request.HttpContext.Items[RequestHeadersHookMiddleware.PropertyKeyRequestContent];
#endif
            object result = action.Handler(queryParameters, parameters);
            if (action.ReturnType == typeof(void))
            {
                return Ok();
            }

            if (result == null)
            {
                string msg = "Result is null.";
#if NETFRAMEWORK
                return SetResult(msg);
#elif NETSTANDARD
                return Ok(msg);
#endif
            }

            if (result is DataObject dataObject)
            {
                DynamicView dynamicView = ApplyAutoExpand(result.GetType(), parameters, dataObject);
                Microsoft.OData.Edm.IEdmEntityType entityType = _model.GetEdmEntityType(result.GetType());
                object edmObj = GetEdmObject(entityType, result, 1, null, dynamicView);
#if NETFRAMEWORK
                return SetResult(edmObj);
#elif NETSTANDARD
                return Ok(edmObj);
#endif
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
#if NETFRAMEWORK
                    return SetResult(coll);
#elif NETSTANDARD
                    return Ok(coll);
#endif
                }
            }

#if NETFRAMEWORK
            return SetResultPrimitive(result.GetType(), result);
#elif NETSTANDARD
            return Ok(result);
#endif
        }

        /// <summary>
        /// Получает текущий SelectExpandClause из запроса.
        /// </summary>
        private SelectExpandClause GetSelectExpandClause()
        {
#if NETFRAMEWORK
            return Request?.ODataProperties()?.SelectExpandClause;
#elif NETSTANDARD
            return HttpContext?.ODataFeature()?.SelectExpandClause;
#endif
        }

        /// <summary>
        /// Устанавливает SelectExpandClause для запроса.
        /// </summary>
        private void SetSelectExpandClause(SelectExpandClause clause)
        {
#if NETFRAMEWORK
            Request?.ODataProperties()?.SelectExpandClause = clause;
#elif NETSTANDARD
            HttpContext?.ODataFeature()?.SelectExpandClause = clause;
#endif
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
#if NETSTANDARD
            string autoExpand = Request.Query[autoExpandParamName].ToString();
#elif NETFRAMEWORK
            string autoExpand = Request.RequestUri.ParseQueryString()[autoExpandParamName];
#endif
    if (string.IsNullOrEmpty(autoExpand) && parameters != null && parameters.ContainsKey(autoExpandParamName))
    {
        autoExpand = parameters[autoExpandParamName]?.ToString();
    }

    if (!string.IsNullOrEmpty(autoExpand) && autoExpand.ToUpperInvariant() == "TRUE" && dataObject != null)
    {
#if NETSTANDARD
                string autoExpandQuery = ExpandQueryGenerator.GetQueryByLoadedProps(dataObject, (Type type, string prop) => _model?.GetEdmTypePropertyName(type, prop));
#elif NETFRAMEWORK
                string autoExpandQuery = BuildExpandFromLoadedProperties(dataObject);
#endif
        if (!string.IsNullOrEmpty(autoExpandQuery))
        {
            LogService.LogDebug($"Auto-expanding masters for {objectType.Name}: {autoExpandQuery}");
            return autoExpandQuery;
        }
    }

    return null;
}

#if NETFRAMEWORK
        /// <summary>
        /// Строит OData $expand запрос из загруженных свойств-мастеров.
        /// Поддерживает вложенные мастера (master inside a master).
        /// </summary>
        /// <remarks>TODO: поправить дублирующуюся логику с классом ExpandQueryGenerator.</remarks>
        private string BuildExpandFromLoadedProperties(DataObject dataObject)
        {
            if (dataObject == null)
                return string.Empty;

            var rootNode = new ExpandNode();
            BuildExpandTreeForObject(dataObject, rootNode);

            string expandQuery = BuildExpandQueryFromNode(rootNode);
            return !string.IsNullOrEmpty(expandQuery) ? "$expand=" + expandQuery : string.Empty;
        }

        /// <summary>
        /// Рекурсивно строит дерево expand для объекта и его загруженных мастеров.
        /// </summary>
        private void BuildExpandTreeForObject(DataObject dataObject, ExpandNode parentNode)
        {
            if (dataObject == null)
                return;

            string[] loadedProps = dataObject.GetLoadedProperties();
            if (loadedProps == null || loadedProps.Length == 0)
                return;

            Type objectType = dataObject.GetType();

            foreach (string propName in loadedProps)
            {
                try
                {
                    // Проверяем, является ли свойство мастером
                    Type propType = Information.GetPropertyType(objectType, propName);
                    if (propType == null ||
                        !propType.IsSubclassOf(typeof(DataObject)) ||
                        propType.IsSubclassOf(typeof(DetailArray)))
                        continue;

                    // Получаем EDM имя
                    string edmName = _model.GetEdmTypePropertyName(objectType, propName);
                    if (string.IsNullOrEmpty(edmName))
                        continue;

                    // Ищем или создаем узел
                    var node = parentNode.Children.FirstOrDefault(n => n.EdmName == edmName);
                    if (node == null)
                    {
                        node = new ExpandNode { EdmName = edmName, PropertyType = propType };
                        parentNode.Children.Add(node);
                    }

                    // Рекурсивно обрабатываем вложенный мастер
                    object propValue = Information.GetPropValueByName(dataObject, propName);
                    if (propValue is DataObject nestedMaster)
                    {
                        BuildExpandTreeForObject(nestedMaster, node);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Строит строку $expand из дерева узлов.
        /// </summary>
        private string BuildExpandQueryFromNode(ExpandNode node)
        {
            if (node.Children.Count == 0)
                return string.Empty;

            var parts = new List<string>();
            foreach (var child in node.Children)
            {
                string nestedExpand = BuildExpandQueryFromNode(child);
                if (!string.IsNullOrEmpty(nestedExpand))
                    parts.Add($"{child.EdmName}($expand={nestedExpand})");
                else
                    parts.Add(child.EdmName);
            }

            return string.Join(",", parts);
        }

        /// <summary>
        /// Узел дерева для построения $expand запроса.
        /// </summary>
        private class ExpandNode
        {
            public string EdmName { get; set; }
            public Type PropertyType { get; set; }
            public List<ExpandNode> Children { get; } = new List<ExpandNode>();
        }
#endif
    }
}
