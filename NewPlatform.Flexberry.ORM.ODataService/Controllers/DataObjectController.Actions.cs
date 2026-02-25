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
            // The OperationImportSegment type represents the Microsoft OData v5.7.0 UnboundActionPathSegment here.
            OperationImportSegment segment = ODataPath.Segments[ODataPath.Segments.Count - 1] as OperationImportSegment;

            // The OperationImportSegment.Identifier property represents the Microsoft OData v5.7.0 UnboundActionPathSegment.ActionName property here.
            if (segment == null || !_functions.IsRegistered(segment.Identifier))
            {
                const string msg = "Action not found";
#if NETFRAMEWORK
                return SetResult(msg);
#elif NETSTANDARD
                return Ok(msg);
#endif
            }

            Action action = _functions.GetFunction(segment.Identifier) as Action;
            if (action == null)
            {
                const string msg = "Action not found";
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
            var result = action.Handler(queryParameters, parameters);
            if (action.ReturnType == typeof(void))
            {
                return Ok();
            }

            if (result == null)
            {
                const string msg = "Result is null.";
#if NETFRAMEWORK
                return SetResult(msg);
#elif NETSTANDARD
                return Ok(msg);
#endif
            }

            if (result is DataObject dataObject)
            {
                // Обрабатываем параметр __autoExpand для автоматического разворачивания загруженных мастеров
                string odataQuery = ProcessAutoExpand(result.GetType(), parameters, dataObject);
                DynamicView dynamicView = null;

                if (!string.IsNullOrEmpty(odataQuery))
                {
                    // Сохраняем предыдущее состояние для отката
                    var previousQueryOptions = QueryOptions;
#if NETFRAMEWORK
                    var previousSelectExpandClause = Request.ODataProperties().SelectExpandClause;
#elif NETSTANDARD
                    var previousSelectExpandClause = HttpContext.ODataFeature().SelectExpandClause;
#endif

                    try
                    {
                        QueryOptions = CreateQueryOptionsFromExpand(result.GetType(), odataQuery);

                        // Устанавливаем SelectExpandClause в запросе для использования сериализатором
                        if (QueryOptions.SelectExpand != null && QueryOptions.SelectExpand.SelectExpandClause != null)
                        {
#if NETFRAMEWORK
                            Request.ODataProperties().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
#elif NETSTANDARD
                            HttpContext.ODataFeature().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
#endif
                        }

                        // Создаем динамическое представление для корректной обработки expand
                        type = result.GetType();
                        CreateDynamicView();
                        dynamicView = _dynamicView;
                    }
                    catch (Exception ex)
                    {
                        // Откатываем предыдущее состояние для предотвращения частичного применения
                        QueryOptions = previousQueryOptions;
#if NETFRAMEWORK
                        Request.ODataProperties().SelectExpandClause = previousSelectExpandClause;
#elif NETSTANDARD
                        HttpContext.ODataFeature().SelectExpandClause = previousSelectExpandClause;
#endif
                        _dynamicView = null;

                        // Логируем ошибку но продолжаем с QueryOptions по умолчанию
                        LogService.LogError($"Failed to apply OData query parameter '{odataQuery}': {ex.Message}", ex);
                    }
                }

                var entityType = _model.GetEdmEntityType(result.GetType());
                var edmObj = GetEdmObject(entityType, result, 1, null, dynamicView);
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
                    // Для коллекций используем первый объект для определения загруженных свойств для auto-expand
                    DataObject firstObject = null;
                    if (result is IEnumerable enumerable)
                    {
                        firstObject = enumerable.Cast<DataObject>().FirstOrDefault();
                    }

                    // Обрабатываем параметр __autoExpand для автоматического разворачивания загруженных мастеров
                    string odataQuery = ProcessAutoExpand(type, parameters, firstObject);
                    DynamicView dynamicView = null;

                    if (!string.IsNullOrEmpty(odataQuery))
                    {
                        // Сохраняем предыдущее состояние для отката
                        var previousQueryOptions = QueryOptions;
#if NETFRAMEWORK
                        var previousSelectExpandClause = Request.ODataProperties().SelectExpandClause;
#elif NETSTANDARD
                        var previousSelectExpandClause = HttpContext.ODataFeature().SelectExpandClause;
#endif

                        try
                        {
                            QueryOptions = CreateQueryOptionsFromExpand(type, odataQuery);

                            // Устанавливаем SelectExpandClause в запросе для использования сериализатором
                            if (QueryOptions.SelectExpand != null && QueryOptions.SelectExpand.SelectExpandClause != null)
                            {
#if NETFRAMEWORK
                                Request.ODataProperties().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
#elif NETSTANDARD
                                HttpContext.ODataFeature().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
#endif
                            }

                            // Создаем динамическое представление для корректной обработки expand
                            this.type = type;
                            CreateDynamicView();
                            dynamicView = _dynamicView;
                        }
                        catch (Exception ex)
                        {
                            // Откатываем предыдущее состояние для предотвращения частичного применения
                            QueryOptions = previousQueryOptions;
#if NETFRAMEWORK
                            Request.ODataProperties().SelectExpandClause = previousSelectExpandClause;
#elif NETSTANDARD
                            HttpContext.ODataFeature().SelectExpandClause = previousSelectExpandClause;
#endif
                            _dynamicView = null;

                            // Логируем ошибку но продолжаем с QueryOptions по умолчанию
                            LogService.LogError($"Failed to apply OData query parameter '{odataQuery}' for collection: {ex.Message}", ex);
                        }
                    }

                    var coll = GetEdmCollection((IEnumerable)result, type, 1, null, dynamicView);
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
        /// Обрабатывает параметр __autoExpand для автоматического разворачивания загруженных свойств-мастеров.
        /// </summary>
        /// <param name="objectType">Тип возвращаемого DataObject.</param>
        /// <param name="parameters">Параметры action.</param>
        /// <param name="dataObject">Экземпляр DataObject (для случая с одним объектом).</param>
        /// <returns>Строка OData $expand для использования, или null если auto-expand не запрошен.</returns>
        private string ProcessAutoExpand(Type objectType, ODataActionParameters parameters, DataObject dataObject = null)
        {
#if NETSTANDARD
            string autoExpand = Request.Query["__autoExpand"].ToString();
#elif NETFRAMEWORK
            string autoExpand = Request.RequestUri.ParseQueryString()["__autoExpand"];
#endif
            if (string.IsNullOrEmpty(autoExpand) && parameters != null && parameters.ContainsKey("__autoExpand"))
            {
                autoExpand = parameters["__autoExpand"]?.ToString();
            }

            if (!string.IsNullOrEmpty(autoExpand) && autoExpand.ToLowerInvariant() == "true" && dataObject != null)
            {
#if NETSTANDARD
                string autoExpandQuery = AutoExpander.BuildExpandQuery(dataObject, (type, prop) => _model?.GetEdmTypePropertyName(type, prop));
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
        private string BuildExpandFromLoadedProperties(DataObject dataObject)
        {
            if (dataObject == null)
                return string.Empty;

            string[] loadedProperties = dataObject.GetLoadedProperties();
            if (loadedProperties == null || loadedProperties.Length == 0)
                return string.Empty;

            var expandProperties = new List<string>();
            Type objectType = dataObject.GetType();

            foreach (string propName in loadedProperties)
            {
                try
                {
                    Type propType = Information.GetPropertyType(objectType, propName);
                    if (propType != null && propType.IsSubclassOf(typeof(DataObject)) && !propType.IsSubclassOf(typeof(DetailArray)))
                    {
                        string edmName = _model.GetEdmTypePropertyName(objectType, propName);
                        if (!string.IsNullOrEmpty(edmName))
                        {
                            expandProperties.Add(edmName);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }

            if (expandProperties.Count == 0)
                return string.Empty;

            return "$expand=" + string.Join(",", expandProperties);
        }
#endif
    }
}
