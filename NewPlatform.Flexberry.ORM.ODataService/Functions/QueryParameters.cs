﻿namespace NewPlatform.Flexberry.ORM.ODataService.Functions
{
    using System;

    using ICSSoft.STORMNET.Business;
    using Microsoft.OData;

    using NewPlatform.Flexberry.ORM.ODataService.Controllers;
    using NewPlatform.Flexberry.ORM.ODataService.Model;

    using Newtonsoft.Json;

#if NETFRAMEWORK
    using System.Net.Http;
#elif NETSTANDARD
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Interfaces;
    using Microsoft.AspNetCore.Http;
#endif

    /// <summary>
    /// Класс для хранения параметров запроса OData.
    /// </summary>
    public class QueryParameters
    {
        // The original Microsoft OData v7.1.0 private constant.
        private const string RequestContainerKey = "Microsoft.AspNet.OData.RequestContainer";

        /// <summary>
        /// Запрос.
        /// </summary>
#if NETFRAMEWORK
        public HttpRequestMessage Request { get; set; }
#elif NETSTANDARD
        public HttpRequest Request { get; set; }
#endif

        /// <summary>
        /// Тело запроса.
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// Параметр запроса $top.
        /// </summary>
        public int? Top { get; set; }

        /// <summary>
        /// Параметр запроса $skip.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Хранит количество обработанных сущностей в пользовательской функции. Используется при формировании результата, если в запросе был параметр $count=true.
        /// </summary>
        public int? Count { get; set; }

        private DataObjectController _controller;

        /// <summary>
        /// Осуществляет получение типа объекта данных, соответствующего заданному имени набора сущностей в EDM-модели.
        /// </summary>
        /// <param name="edmEntitySetName">Имя набора сущностей в EDM-модели, для которого требуется получить представление по умолчанию.</param>
        /// <returns>Типа объекта данных, соответствующий заданному имени набора сущностей в EDM-модели.</returns>
        public Type GetDataObjectType(string edmEntitySetName)
        {
            DataObjectEdmModel model = (DataObjectEdmModel)_controller.QueryOptions.Context.Model;
            return model.GetDataObjectType(edmEntitySetName);
        }

        /// <summary>
        /// Создаёт lcs по заданному типу и запросу OData.
        /// </summary>
        /// <param name="type">Тип DataObject.</param>
        /// <param name="odataQuery">Запрос OData.</param>
        /// <returns>Возвращает lcs.</returns>
        public LoadingCustomizationStruct CreateLcs(Type type, string odataQuery = null)
        {
            var request = _controller.Request;
            if (odataQuery != null)
            {
#if NETFRAMEWORK
                var r = new HttpRequestMessage(HttpMethod.Get, odataQuery);

                object value;
                if (_controller.Request.Properties.TryGetValue(RequestContainerKey, out value))
                {
                    r.Properties.Add(RequestContainerKey, value);
                }

                request = r;
#elif NETSTANDARD
                // Parse and make escaped query part of 'odataQuery' value.
                string queryPart = new Uri(odataQuery).Query;

                // Create mock request in order to get ODataQueryOptions instance corresponding to query part of 'odataQuery' value if latter exists.
                if (!string.IsNullOrWhiteSpace(queryPart))
                {
                    var odataFeature = new ODataFeature();
                    odataFeature.RequestContainer = request.HttpContext.Features.Get<IODataFeature>().RequestContainer;

                    var httpContext = new DefaultHttpContext();
                    httpContext.RequestServices = request.HttpContext.RequestServices;
                    httpContext.Features.Set<IODataFeature>(odataFeature);

                    request = httpContext.Request;
                    request.QueryString = new QueryString(queryPart);
                }
#endif
            }

            _controller.QueryOptions = _controller.CreateODataQueryOptions(type, request);
            _controller.type = type;
            return _controller.CreateLcs();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="controller">Контроллер DataObjectController.</param>
        internal QueryParameters(DataObjectController controller)
        {
            _controller = controller;
            if (controller.QueryOptions == null)
            {
                return;
            }

            try
            {
                if (controller.QueryOptions.Skip != null)
                {
                    Skip = controller.QueryOptions.Skip.Value;
                }

                if (controller.QueryOptions.Top != null)
                {
                    Top = controller.QueryOptions.Top.Value;
                }
            }
            catch (Exception ex)
            {
                throw new ODataException($"Failed to initialize {nameof(QueryParameters)}: {JsonConvert.SerializeObject(controller.QueryOptions.RawValues)}", ex);
            }
        }
    }
}
