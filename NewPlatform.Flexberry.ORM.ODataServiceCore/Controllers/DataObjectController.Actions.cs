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

        private IActionResult ExecuteAction(ODataActionParameters parameters)
        {
            ODataPath odataPath = Request.HttpContext.ODataFeature().Path;

            // The OperationImportSegment type represents the Microsoft OData v5.7.0 UnboundActionPathSegment here.
            OperationImportSegment segment = odataPath.Segments[odataPath.Segments.Count - 1] as OperationImportSegment;

            // The OperationImportSegment.Identifier property represents the Microsoft OData v5.7.0 UnboundActionPathSegment.ActionName property here.
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
            var result = action.Handler(queryParameters, parameters);
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
                // Обрабатываем параметр __autoExpand для автоматического разворачивания загруженных мастеров
                string odataQuery = ProcessAutoExpand(result.GetType(), parameters, dataObject);
                DynamicView dynamicView = null;

                if (!string.IsNullOrEmpty(odataQuery))
                {
                    // Сохраняем предыдущее состояние для отката
                    var previousQueryOptions = QueryOptions;
                    var previousSelectExpandClause = HttpContext.ODataFeature().SelectExpandClause;

                    try
                    {
                        QueryOptions = CreateQueryOptionsFromExpand(result.GetType(), odataQuery);

                        // Устанавливаем SelectExpandClause в запросе для использования сериализатором
                        if (QueryOptions.SelectExpand != null && QueryOptions.SelectExpand.SelectExpandClause != null)
                        {
                            HttpContext.ODataFeature().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
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
                        HttpContext.ODataFeature().SelectExpandClause = previousSelectExpandClause;
                        _dynamicView = null;

                        // Логируем ошибку но продолжаем с QueryOptions по умолчанию
                        LogService.LogError($"Failed to apply OData query parameter '{odataQuery}': {ex.Message}", ex);
                    }
                }

                var entityType = EdmModel.GetEdmEntityType(result.GetType());
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
                        var previousSelectExpandClause = HttpContext.ODataFeature().SelectExpandClause;

                        try
                        {
                            QueryOptions = CreateQueryOptionsFromExpand(type, odataQuery);

                            // Устанавливаем SelectExpandClause в запросе для использования сериализатором
                            if (QueryOptions.SelectExpand != null && QueryOptions.SelectExpand.SelectExpandClause != null)
                            {
                                HttpContext.ODataFeature().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
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
                            HttpContext.ODataFeature().SelectExpandClause = previousSelectExpandClause;
                            _dynamicView = null;

                            // Логируем ошибку но продолжаем с QueryOptions по умолчанию
                            LogService.LogError($"Failed to apply OData query parameter '{odataQuery}' for collection: {ex.Message}", ex);
                        }
                    }

                    var coll = GetEdmCollection((IEnumerable)result, type, 1, null, dynamicView);
                    return Ok(coll);
                }
            }

            return Ok(result);
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
            string autoExpand = Request.Query["__autoExpand"].ToString();
            if (string.IsNullOrEmpty(autoExpand) && parameters != null && parameters.ContainsKey("__autoExpand"))
            {
                autoExpand = parameters["__autoExpand"]?.ToString();
            }

            if (!string.IsNullOrEmpty(autoExpand) && autoExpand.ToLowerInvariant() == "true" && dataObject != null)
            {
                string autoExpandQuery = AutoExpander.BuildExpandQuery(dataObject, (type, prop) => EdmModel?.GetEdmTypePropertyName(type, prop));
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
