namespace NewPlatform.Flexberry.ORM.ODataService.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Reflection;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using ICSSoft.STORMNET.KeyGen;
    using ICSSoft.STORMNET.Security;
    using ICSSoft.STORMNET.UserDataTypes;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Adapters;
    using Microsoft.AspNet.OData.Common;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNet.OData.Interfaces;
    using Microsoft.AspNet.OData.Query;
    using Microsoft.AspNet.OData.Routing;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;
    using NewPlatform.Flexberry.ORM.ODataService.Batch;
    using NewPlatform.Flexberry.ORM.ODataService.Expressions;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Files;
    using NewPlatform.Flexberry.ORM.ODataService.Formatter.Serialization;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.Offline;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Middleware;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.WebUtilities;

    using HandleNullPropagationOptionHelper = Expressions.HandleNullPropagationOptionHelper;
    using ODataPath = Microsoft.AspNet.OData.Routing.ODataPath;
    using OrderByQueryOption = Expressions.OrderByQueryOption;
    using SRResources = Expressions.SRResources;

    /// <summary>
    /// The <see cref="DataObject"/> OData controller class.
    /// </summary>
    public partial class DataObjectController : ODataController
    {
        private static readonly IWebApiAssembliesResolver _defaultAssembliesResolver = new WebApiAssembliesResolver();

        /// <summary>
        /// The data object file properties accessor.
        /// </summary>
        private readonly IDataObjectFileAccessor _dataObjectFileAccessor;

        /// <summary>
        /// The data service for all manipulations with data.
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        /// The data object cache for sync loading.
        /// </summary>
        private DataObjectCache _dataObjectCache;

        private DynamicView _dynamicView;

        private List<string> _filterDetailProperties;

        private LoadingCustomizationStruct _lcs;

        private List<Type> _lcsLoadingTypes = new List<Type>();

        private ManagementToken _managementToken;

        private DataObject[] _objs;

        private Dictionary<SelectItem, ExpandedNavigationSelectItem> _parentExpandedNavigationSelectItem = new Dictionary<SelectItem, ExpandedNavigationSelectItem>();

        private Dictionary<SelectItem, string> _properties = new Dictionary<SelectItem, string>();

        /// <summary>
        /// Gets a <see cref="ICSSoft.STORMNET.DataObjectCache" /> instance from a http context if such instance exists,
        /// otherwise creates a new <see cref="ICSSoft.STORMNET.DataObjectCache"/> instance.
        /// </summary>
        /// <remarks>
        /// Tries to extract object from the request shared data for batch requests
        /// before creating a new <see cref="DataObjectCache"/> instance.
        /// </remarks>
        private DataObjectCache DataObjectCache
        {
            get
            {
                if (_dataObjectCache == null && HttpContext != null)
                {
                    if (IsBatchChangeSetRequest)
                    {
                        _dataObjectCache = (DataObjectCache)HttpContext.Items[DataObjectODataBatchHandler.DataObjectCachePropertyKey];
                    }

                    if (_dataObjectCache == null)
                    {
                        _dataObjectCache = new DataObjectCache();
                        _dataObjectCache.StartCaching(false);
                    }
                }

                return _dataObjectCache;    
            }
        }

        /// <summary>
        /// The current EDM model.
        /// </summary>
        private DataObjectEdmModel EdmModel => ManagementToken?.Model;

        private bool IsBatchChangeSetRequest => HttpContext?.ODataBatchFeature()?.ChangeSetId != null;

        private ManagementToken ManagementToken
        {
            get
            {
                if (_managementToken == null)
                {
                    _managementToken = RouteData == null ? null : RouteData.Routers.OfType<ODataRoute>().Single().GetManagementToken();
                }

                return _managementToken;
            }
        }

        /// <summary>
        /// ���������� ��������� � ����������, ������� ����� ������� � ����������.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// �������� ��� ��� � ���������� ���������� ���������.
        /// </summary>
        public bool IncludeCount { get; set; }

        /// <summary>
        /// ������������ � ������� ���������. ����������� � ������ Init().
        /// </summary>
        public ODataQueryOptions QueryOptions { get; set; }

        /// <summary>
        /// ��� DataObject, ������� ������������� �������� � ������ �� �������. ����������� � ������ Init().
        /// </summary>
        public Type type { get; set; }

        /// <summary>
        /// The current offline manager.
        /// </summary>
        internal BaseOfflineManager OfflineManager { get; set; }

        /// <summary>
        /// ����������� ��-���������.
        /// </summary>
        /// <param name="dataObjectFileAccessor">The data object file properties accessor.</param>
        /// <param name="dataService">The data service for all manipulations with data.</param>
        /// <param name="offlineManager">The offline manager.</param>
        public DataObjectController(IDataObjectFileAccessor dataObjectFileAccessor, IDataService dataService = null, BaseOfflineManager offlineManager = null)
            : base()
        {
            _dataObjectFileAccessor = dataObjectFileAccessor;

            _dataService = UnityFactoryHelper.ResolveRequiredIfNull(dataService);
            OfflineManager = UnityFactoryHelper.ResolveIfNull(offlineManager) ?? new DummyOfflineManager();
        }

        /// <summary>
        /// ������� ��������� �������� ������ ��������.
        /// </summary>
        /// <returns>A <see cref="LoadingCustomizationStruct"/> instance.</returns>
        public LoadingCustomizationStruct CreateLcs()
        {
            Expression expr = GetExpression(type, QueryOptions);
            if (_filterDetailProperties != null && _filterDetailProperties.Count > 0)
            {
                CreateDynamicView();
                _filterDetailProperties = null;
            }

            View view = EdmModel.GetDataObjectDefaultView(type);
            if (_dynamicView != null)
                view = _dynamicView.View;
            IEnumerable<View> resolvingViews;
            view = DynamicView.GetViewWithPropertiesUsedInExpression(expr, type, view, _dataService, out resolvingViews);
            if (_lcsLoadingTypes.Count == 0)
                _lcsLoadingTypes = EdmModel.GetDerivedTypes(type).ToList();

            for (int i = 0; i < _lcsLoadingTypes.Count; i++)
            {
                if (!Information.IsStoredType(_lcsLoadingTypes[i]))
                {
                    _lcsLoadingTypes.RemoveAt(i);
                    i--;
                }
            }

            LoadingCustomizationStruct lcs = new LoadingCustomizationStruct(null);
            if (expr != null)
            {
                lcs = LinqToLcs.GetLcs(expr, view, resolvingViews);
            }

            lcs.View = view;
            lcs.LoadingTypes = new Type[_lcsLoadingTypes.Count];

            for (int i = 0; i < _lcsLoadingTypes.Count; i++)
            {
                lcs.LoadingTypes[i] = _lcsLoadingTypes[i];
            }

            return lcs;
        }

        /// <summary>
        /// ������ ��������� ������� OData.
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <returns>��������� ������� OData.</returns>
        public ODataQueryOptions CreateODataQueryOptions(Type type)
        {
            return CreateODataQueryOptions(type, Request);
        }

        /// <summary>
        /// ������ ��������� ������� OData.
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <param name="request">������ OData.</param>
        /// <returns>��������� ������� OData.</returns>
        public ODataQueryOptions CreateODataQueryOptions(Type type, HttpRequest request)
        {
            return new ODataQueryOptions(CreateODataQueryContext(type), request);
        }

        /// <summary>
        /// ������������ ������� GET, ������� ������������� ������� �������� �������� �� ������ ���������.
        /// ���� ����� ��������� ����������� ��������, �������� ��������� �������.
        /// </summary>
        /// <returns>����������� �������� �� ������ ���������.</returns>
        [CustomEnableQuery]
        public IActionResult Get()
        {
            try
            {
                Init();
                return ExecuteExpression();
            }
            catch (Exception ex)
            {
                throw CustomException(ex);
            }
        }

        /// <summary>
        /// ������������ ������� GET, ������� ������������� ������� �������� �������� �� ������ ���������.
        /// ���� ����� ��������� ����������� ��������, �������� ��������� �������. ��� "GetCollection" ��������������� � DataObjectRoutingConvention.SelectAction.
        /// </summary>
        /// <returns>����������� �������� �� ������ ���������.</returns>
        [CustomEnableQuery]
        public OkObjectResult GetCollection()
        {
            try
            {
                return Ok(EvaluateOdataPath());
            }
            catch (Exception ex)
            {
                throw CustomException(ex);
            }
        }

        /// <summary>
        /// ������������ ������� GET, ������� ������������� ������� �������� ��������� ��������. ��� "GetEntity" ��������������� � DataObjectRoutingConvention.SelectAction.
        /// </summary>
        /// <returns>��������</returns>
        [CustomEnableQuery]
        public OkObjectResult GetEntity()
        {
            try
            {
                return Ok(EvaluateOdataPath());
            }
            catch (Exception ex)
            {
                throw CustomException(ex);
            }
        }

        /// <summary>
        /// ������������ ������� GET, ������� ������������� ������� �������� ��������� �������� � ������� ����� �� ������ ���������.
        /// </summary>
        /// <returns>��������</returns>
        [CustomEnableQuery]
        public OkObjectResult GetGuid()
        {
            try
            {
                ODataPath odataPath = HttpContext.ODataFeature().Path;
                var keySegment = odataPath.Segments[1] as KeySegment;
                Guid key = new Guid(keySegment.Keys.First().Value.ToString());

                Init();
                var obj = LoadObject(type, key);

                return Ok(GetEdmObject(EdmModel.GetEdmEntityType(type), obj, 1, null, _dynamicView));
            }
            catch (Exception ex)
            {
                throw CustomException(ex);
            }
        }


        /// <summary>
        /// ������������ ������� GET, ������� ������������� ������� �������� ��������� �������� � ������� ����� �� ������ ���������.
        /// </summary>
        /// <returns>��������</returns>
        [CustomEnableQuery]
        public OkObjectResult GetString()
        {
            try
            {
                ODataPath odataPath = HttpContext.ODataFeature().Path;
                var keySegment = odataPath.Segments[1] as KeySegment;
                string key = keySegment.Keys.First().Value.ToString().Trim().Replace("'", string.Empty);

                Init();
                var obj = LoadObject(type, key);

                return Ok(GetEdmObject(EdmModel.GetEdmEntityType(type), obj, 1, null, _dynamicView));
            }
            catch (Exception ex)
            {
                throw CustomException(ex);
            }
        }

        /// <summary>
        /// ����������� ������ DataObject � ��������.
        /// </summary>
        /// <param name="entityType">��� ��������.</param>
        /// <param name="obj">������ DataObject.</param>
        /// <param name="level">������� ��������� ��� ������������� �������. ���� ������ ������ ���� ����� 1, � ���� ��� ����������� �����, �� ����� ���� ����� 0.</param>
        /// <param name="expandedNavigationSelectItem">������������� ��������.</param>
        /// <returns>��������.</returns>
        public EdmEntityObject GetEdmObject(IEdmEntityType entityType, object obj, int level, ExpandedNavigationSelectItem expandedNavigationSelectItem)
        {
            return GetEdmObject(entityType, obj, level, expandedNavigationSelectItem, null);
        }

        /// <summary>
        /// ���������� ���������� �������� ��� linq-��������� ���������������� ���������� ������� OData (������ ��� $filter).
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <param name="queryOptions">��������� �������.</param>
        /// <returns>���������� ��������.</returns>
        public int GetObjectsCount(Type type, ODataQueryOptions queryOptions)
        {
            var expr = GetExpressionFilterOnly(type, queryOptions);
            View view = EdmModel.GetDataObjectDefaultView(type);
            var lcs = LinqToLcs.GetLcs(expr, view);
            lcs.View = view;
            lcs.LoadingTypes = new[] { type };
            lcs.ReturnType = LcsReturnType.Objects;

            return _dataService.GetObjectsCount(lcs);
        }

        /// <summary>
        /// ������������ ��� ���������������� ������� OData
        /// </summary>
        /// <returns>�������� ��������� ������ ��� �������� � ������ ����� ����������.</returns>
        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "MERGE", "DELETE")]
        public IActionResult HandleUnmappedRequest()
        {
            return null;
        }

        /// <summary>
        /// ���������� linq-��������� ��������������� ���������� ������� OData.
        /// </summary>
        /// <param name="queryOpt">��������� �������.</param>
        /// <typeparam name="TElement">��������.</typeparam>
        /// <returns>Linq-���������.</returns>
        public Expression ToExpression<TElement>(ODataQueryOptions queryOpt)
        {
            if (queryOpt == null)
                return null;
            IQueryable<TElement> queryable = Enumerable.Empty<TElement>().AsQueryable();

            // if (queryOpt.Filter != null) queryable = (IQueryable<TElement>)queryOpt.Filter.ApplyTo(queryable, new ODataQuerySettings());
            if (queryOpt.Filter != null)
                queryable = (IQueryable<TElement>)FilterApplyTo(queryOpt.Filter, queryable);
            if (queryOpt.OrderBy != null)
            {
                // queryable = queryOpt.OrderBy.ApplyTo(queryable, new ODataQuerySettings());
                queryable = new OrderByQueryOption(queryOpt.OrderBy, type).ApplyTo(queryable, new ODataQuerySettings());
            }

            if (queryOpt.Skip != null)
                queryable = queryOpt.Skip.ApplyTo(queryable, new ODataQuerySettings());
            if (queryOpt.Top != null)
                queryable = queryOpt.Top.ApplyTo(queryable, new ODataQuerySettings());
            return queryable.Expression;
        }

        /// <summary>
        /// ���������� linq-��������� ��������������� ���������� ������� OData (������ ��� $filter).
        /// </summary>
        /// <param name="queryOpt">��������� �������.</param>
        /// <typeparam name="TElement">��������.</typeparam>
        /// <returns>Linq-���������.</returns>
        public Expression ToExpressionFilterOnly<TElement>(ODataQueryOptions queryOpt)
        {
            if (queryOpt == null)
                return null;
            IQueryable<TElement> queryable = Enumerable.Empty<TElement>().AsQueryable();

            // if (queryOpt.Filter != null) queryable = (IQueryable<TElement>)queryOpt.Filter.ApplyTo(queryable, new ODataQuerySettings());
            if (queryOpt.Filter != null)
                queryable = (IQueryable<TElement>)FilterApplyTo(queryOpt.Filter, queryable);
            return queryable.Expression;
        }

        /// <summary>
        /// Exports data as a file with .xlsx content.
        /// </summary>
        /// <param name="queryParams">The request query values.</param>
        /// <returns>A file with .xlsx content.</returns>
        internal IActionResult CreateExcel(NameValueCollection queryParams)
        {
            View view = _dynamicView.View;
            if (_lcs != null)
            {
                view = _lcs.View;
            }

            view.Name = "View";
            ExportParams par = new ExportParams
            {
                PropertiesOrder = new List<string>(),
                View = view,
                DataObjectTypes = null,
                LimitFunction = null
            };

            var colsOrder = queryParams.Get("colsOrder").Split(',').ToList();
            par.PropertiesOrder = new List<string>();
            par.HeaderCaptions = new List<IHeaderCaption>();

            foreach (string column in colsOrder)
            {
                var columnInfo = column.Split(new char[] { '/' }, 2);
                var decodeColumnInfo0 = WebUtility.UrlDecode(columnInfo[0]);
                var decodeColumnInfo1 = WebUtility.UrlDecode(columnInfo[1]);

                par.PropertiesOrder.Add(decodeColumnInfo0);
                par.HeaderCaptions.Add(new HeaderCaption { PropertyName = decodeColumnInfo0, Caption = decodeColumnInfo1 });
            }

            for (int i = 0; i < view.Details.Length; i++)
            {
                DetailInView detail = view.Details[i];
                detail.View.Name = string.IsNullOrEmpty(detail.Name) ? $"ViewDetail{i}" : detail.Name;
                var column = par.HeaderCaptions.FirstOrDefault(col => col.PropertyName == detail.Name);
                if (column != null)
                {
                    column.MasterName = string.IsNullOrEmpty(view.Name) ? "View" : view.Name;
                    column.DetailName = detail.View.Name;
                }

                var properties = new List<PropertyInView>();
                for (int j = 0; j < detail.View.Properties.Length; j++)
                {
                    PropertyInView propDetail = detail.View.Properties[j];
                    if (_properties.Keys.FirstOrDefault(p => !(p is PathSelectItem) && _properties[p] == $"{detail.Name}.{propDetail.Name}") != null)
                        continue;
                    if (_properties.Keys.FirstOrDefault(p => p is PathSelectItem && _properties[p] == $"{detail.Name}.{propDetail.Name}") != null)
                        properties.Add(propDetail);
                }

                detail.View.Properties = properties.ToArray();
            }

            par.DetailsInSeparateColumns = Convert.ToBoolean(queryParams.Get("detSeparateCols"));
            par.DetailsInSeparateRows = Convert.ToBoolean(queryParams.Get("detSeparateRows"));

            using (MemoryStream stream = EdmModel.ODataExportService != null
                ? EdmModel.ODataExportService.CreateExportStream(_dataService, par, _objs, queryParams)
                : EdmModel.ExportService.CreateExportStream(_dataService, par, _objs))
            {
                return File(stream.ToArray(), "application/ms-excel", "list.xlsx");
            }
        }

        /// <summary>
        /// ����������� ��������� �������� DataObject � ����� ���������.
        /// </summary>
        /// <param name="objs">��������� �������� DataObject.</param>
        /// <param name="type">��� ������� DataObject.</param>
        /// <param name="level">������� ��������� ��� ������������� �������. ���� ������ ������ ���� ����� 1, � ���� ��� ����������� �����, �� ����� ���� ����� 0.</param>
        /// <param name="expandedNavigationSelectItem">������������� ��������.</param>
        /// <param name="dynamicView">������������ �������������.</param>
        /// <returns>����� ���������.</returns>
        internal EdmEntityObjectCollection GetEdmCollection(IEnumerable objs, Type type, int level, ExpandedNavigationSelectItem expandedNavigationSelectItem, DynamicView dynamicView = null)
        {
            if (level == 0)
                return null;
            var entityType = EdmModel.GetEdmEntityType(type);
            List<IEdmEntityObject> edmObjList = new List<IEdmEntityObject>();

            foreach (var obj in objs)
            {
                var realType = EdmModel.GetEdmEntityType(obj.GetType());
                var edmObj = GetEdmObject(realType, obj, level, expandedNavigationSelectItem, dynamicView);
                if (edmObj != null)
                    edmObjList.Add(edmObj);
            }

            if (IncludeCount && expandedNavigationSelectItem == null)
            {
                Request.HttpContext.Items.Add(CustomODataFeedSerializer.Count, Count);
            }

            IEdmCollectionTypeReference entityCollectionType = new EdmCollectionTypeReference(new EdmCollectionType(new EdmEntityTypeReference(entityType, false)));
            EdmEntityObjectCollection collection = new EdmEntityObjectCollection(entityCollectionType, edmObjList);
            return collection;
        }

        /// <summary>
        /// ����������� ������ DataObject � ��������.
        /// </summary>
        /// <param name="entityType">��� ��������.</param>
        /// <param name="obj">������ DataObject.</param>
        /// <param name="level">������� ��������� ��� ������������� �������. ���� ������ ������ ���� ����� 1.</param>
        /// <param name="expandedNavigationSelectItem">������������� ��������.</param>
        /// <param name="dynamicView">������������ �������������.</param>
        /// <returns>��������.</returns>
        internal EdmEntityObject GetEdmObject(IEdmEntityType entityType, object obj, int level, ExpandedNavigationSelectItem expandedNavigationSelectItem, DynamicView dynamicView)
        {
            if (level == 0 || obj == null || (obj is DataObject dataObject && dataObject.__PrimaryKey == null))
                return null;
            EdmEntityObject entity = new EdmEntityObject(entityType);

            var expandedProperties = new Dictionary<string, ExpandedNavigationSelectItem>();
            var selectedProperties = new Dictionary<string, SelectItem>();
            IEnumerable<SelectItem> selectedItems = null;
            if (expandedNavigationSelectItem == null)
            {
                if (QueryOptions?.SelectExpand != null)
                    selectedItems = QueryOptions.SelectExpand.SelectExpandClause.SelectedItems;
            }
            else
            {
                selectedItems = expandedNavigationSelectItem.SelectAndExpand.SelectedItems;
            }

            if (selectedItems != null)
            {
                foreach (var item in selectedItems)
                {
                    var expandedItem = CastExpandedNavigationSelectItem(item);
                    if (expandedItem == null)
                    {
                        if (item is PathSelectItem pathSelectItem && pathSelectItem.SelectedPath.FirstSegment is PropertySegment propertySegment)
                        {
                            string key = propertySegment.Property.Name;
                            if (!selectedProperties.ContainsKey(key))
                            {
                                selectedProperties.Add(key, pathSelectItem);
                            }
                        }
                    }
                    else
                    {
                        expandedProperties.Add(((NavigationPropertySegment)expandedItem.PathToNavigationProperty.FirstSegment).NavigationProperty.Name, expandedItem);
                    }
                }
            }

            foreach (var prop in entityType.Properties())
            {
                string dataObjectPropName;
                try
                {
                    dataObjectPropName = EdmModel.GetDataObjectProperty(entityType.FullTypeName(), prop.Name).Name;
                }
                catch (KeyNotFoundException)
                {
                    // Check if prop value is the link from master to pseudodetail (pseudoproperty).
                    if (HasPseudoproperty(entityType, prop.Name))
                    {
                        continue;
                    }

                    throw;
                }

                Type objectType = obj.GetType();
                PropertyInfo propertyInfo = objectType.GetProperty(dataObjectPropName);
                if (prop is EdmNavigationProperty navProp)
                {
                    if (expandedProperties.ContainsKey(navProp.Name))
                    {
                        var expandedItem = expandedProperties[navProp.Name];
                        string propPath = _properties.ContainsKey(expandedItem) ? _properties[expandedItem] : null;
                        EdmMultiplicity targetMultiplicity = navProp.TargetMultiplicity();
                        if (targetMultiplicity == EdmMultiplicity.One || targetMultiplicity == EdmMultiplicity.ZeroOrOne)
                        {
                            DataObject master = propertyInfo.GetValue(obj, null) as DataObject;
                            EdmEntityObject edmObj = null;
                            if (dynamicView == null)
                            {
                                View view;
                                if (master == null)
                                {
                                    view = EdmModel.GetDataObjectDefaultView(objectType);
                                    obj = LoadObject(view, (DataObject)obj);
                                }

                                master = propertyInfo.GetValue(obj, null) as DataObject;
                                if (master != null)
                                {
                                    view = EdmModel.GetDataObjectDefaultView(master.GetType());
                                    if (view != null)
                                    {
                                        master = LoadObject(view, master);
                                        edmObj = GetEdmObject(EdmModel.GetEdmEntityType(master.GetType()), master, level, expandedItem);
                                    }
                                }
                            }
                            else
                            {
                                if (master != null)
                                {
                                    if (!DynamicView.ContainsPoperty(dynamicView.View, propPath))
                                    {
                                        _dataService.LoadObject(dynamicView.View, master, false, true, DataObjectCache);
                                    }

                                    edmObj = GetEdmObject(EdmModel.GetEdmEntityType(master.GetType()), master, level, expandedItem, dynamicView);
                                }
                            }

                            entity.TrySetPropertyValue(navProp.Name, edmObj);
                        }

                        if (targetMultiplicity == EdmMultiplicity.Many)
                        {
                            View view = EdmModel.GetDataObjectDefaultView(objectType);
                            if (dynamicView == null || !DynamicView.ContainsPoperty(dynamicView.View, propPath))
                            {
                                obj = LoadObject(view, (DataObject)obj);
                            }

                            var detail = (DetailArray)propertyInfo.GetValue(obj, null);
                            IEnumerable<DataObject> objs = detail.GetAllObjects();
                            if (expandedItem.SkipOption != null)
                                objs = objs.Skip((int)expandedItem.SkipOption);
                            if (expandedItem.TopOption != null)
                                objs = objs.Take((int)expandedItem.TopOption);
                            var coll = GetEdmCollection(objs, detail.ItemType, 1, expandedItem, dynamicView);
                            if (coll != null && coll.Count > 0)
                            {
                                entity.TrySetPropertyValue(navProp.Name, coll);
                            }
                        }
                    }
                }
                else
                {
                    if (prop.Name == EdmModel.KeyPropertyName)
                    {
                        object key = propertyInfo.GetValue(obj, null);
                        if (key is KeyGuid keyGuid)
                        {
                            entity.TrySetPropertyValue(prop.Name, keyGuid.Guid);
                        }
                        else
                        {
                            entity.TrySetPropertyValue(prop.Name, key);
                            //entity.TrySetPropertyValue(prop.Name, new Guid((string)key));
                        }

                        //KeyGuid keyGuid = (KeyGuid)obj.GetType().GetProperty(prop.Name).GetValue(obj, null);
                        //entity.TrySetPropertyValue(prop.Name, keyGuid.Guid);
                    }

                    // ������������ ��������, ���� $select ����, ������� � $select ��� ������ �������� ����������� (��������, Enum).
                    else if (!selectedProperties.Any()
                             || selectedProperties.ContainsKey(prop.Name)
                             || !prop.Type.IsNullable)
                    {
                        object value;
                        if (propertyInfo == null)
                        {
                            propertyInfo = objectType.GetProperty(dataObjectPropName, BindingFlags.Public | BindingFlags.FlattenHierarchy);
                            if (propertyInfo == null)
                                continue;
                            value = propertyInfo.GetValue(null);
                        }
                        else
                        {
                            try
                            {
                                value = propertyInfo.GetValue(obj, null);
                            }
                            catch (System.Exception)
                            {
                                continue;
                            }
                        }

                        Type propType = propertyInfo.PropertyType;
                        if (propType == typeof(DataObject))
                            continue;

                        // ���� ��� �������� ��������� � ������ �� ������������������ ����������� �������� �������,
                        // ������ �������� ��������, � ��� ����� ���������� ������ �������.
                        if (_dataObjectFileAccessor.HasDataObjectFileProvider(propType))
                        {
                            // ��������� �������� ������� �������� ������.
                            // ODataService ����� ���������� ������ � ���������������� ����������� ��������� ��������.
                            if (!selectedProperties.Any() || (selectedProperties.Any() && selectedProperties.ContainsKey(dataObjectPropName)))
                            {
                                value = _dataObjectFileAccessor.GetDataObjectFileProvider(propType)
                                    .GetFileDescription(_dataService, (DataObject)obj, dataObjectPropName)
                                    ?.ToJson();
                            }
                        }
                        else
                        {
                            // �������������� ����� ��� ����������� �������.
                            if (value is KeyGuid)
                                value = ((KeyGuid)value).Guid;
                            if (value is NullableDateTime)
                                value = new DateTime(((NullableDateTime)value).Value.Ticks, DateTimeKind.Utc);
                            if (value is NullableInt)
                                value = ((NullableInt)value).Value;
                            if (value is NullableDecimal)
                                value = ((NullableDecimal)value).Value;
                            if (value is Contact)
                                value = (string)((Contact)value);
                            if (value is Event)
                                value = (string)((Event)value);
                            if (value is GeoData)
                                value = (string)((GeoData)value);
                            if (value is Image)
                                value = (string)((Image)value);
                            if (value is DateTime)
                                value = new DateTime(((DateTime)value).Ticks, DateTimeKind.Utc);
                        }

                        entity.TrySetPropertyValue(prop.Name, value);
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// ��������� linq-��������� ��������������� ���������� ������� OData � ������� �������� DataObject.
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <param name="queryOptions">��������� �������.</param>
        /// <param name="objs">������ �������� DataObject.</param>
        /// <returns>����������� � �������� OData ������� DataObject.</returns>
        private IQueryable ApplyExpression(Type type, ODataQueryOptions queryOptions, DataObject[] objs)
        {
            MethodInfo methodToExpression = GetType().GetMethod("ApplyTo").MakeGenericMethod(type);
            return (IQueryable)methodToExpression.Invoke(this, new object[] { queryOptions, objs });
        }

        /// <summary>
        /// ��������� linq-��������� ��������������� ���������� ������� OData � ������� �������� DataObject.
        /// </summary>
        /// <param name="queryOpt">��������� �������.</param>
        /// <param name="objs">������ �������� DataObject.</param>
        /// <typeparam name="TElement">��������.</typeparam>
        /// <returns>����������� � �������� OData ������� DataObject.</returns>
        public IQueryable ApplyTo<TElement>(ODataQueryOptions queryOpt, DataObject[] objs)
        {
            if (queryOpt == null)
                return null;

            IQueryable<TElement> queryable = objs.AsQueryable().Cast<TElement>();

            if (queryOpt.Filter != null)
                queryable = (IQueryable<TElement>)FilterApplyTo(queryOpt.Filter, queryable);

            if (queryOpt.Skip != null)
                queryable = queryOpt.Skip.ApplyTo(queryable, new ODataQuerySettings());

            if (queryOpt.Top != null)
                queryable = queryOpt.Top.ApplyTo(queryable, new ODataQuerySettings());

            if (queryOpt.OrderBy != null)
                queryable = queryOpt.OrderBy.ApplyTo(queryable, new ODataQuerySettings());

            return queryable;
        }

        private ExpandedNavigationSelectItem CastExpandedNavigationSelectItem(SelectItem item)
        {
            if (!(item is ExpandedNavigationSelectItem))
                return null;
            var expandedItem = (ExpandedNavigationSelectItem)item;
            if (!(expandedItem.PathToNavigationProperty.FirstSegment is NavigationPropertySegment))
                return null;
            return expandedItem;
        }

        private void CreateDynamicView()
        {
            if (QueryOptions.SelectExpand == null || QueryOptions.SelectExpand.SelectExpandClause == null)
            {
                var properties = DynamicView.GetProperties(type);
                if (_filterDetailProperties != null && _filterDetailProperties.Count > 0)
                {
                    properties.AddRange(_filterDetailProperties);
                }

                _dynamicView = DynamicView.Create(type, properties /*, EdmModel.DynamicViewCache */); // TODO: !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return;
            }

            List<string> props = new List<string>();
            if (QueryOptions.SelectExpand.SelectExpandClause.AllSelected)
            {
                var props2 = DynamicView.GetProperties(type);
                props.AddRange(props2);
            }

            GetPropertiesForDynamicView(null, QueryOptions.SelectExpand.SelectExpandClause.SelectedItems);
            foreach (SelectItem item in _properties.Keys)
            {
                var name = _properties[item];
                ExpandedNavigationSelectItem expandedItem = item as ExpandedNavigationSelectItem;
                if (expandedItem != null && expandedItem.SelectAndExpand.AllSelected)
                {
                    var edmType = ((NavigationPropertySegment)expandedItem.PathToNavigationProperty.FirstSegment).EdmType;
                    string typeName;
                    if (edmType is EdmCollectionType)
                    {
                        typeName = (edmType as EdmCollectionType).ElementType.FullName();
                    }
                    else
                    {
                        typeName = (edmType as EdmEntityType).FullName();
                    }

                    var types = EdmModel.GetTypes(new List<string>() { typeName });
                    var props2 = DynamicView.GetProperties(types[0]);
                    for (int i = 0; i < props2.Count; i++)
                    {
                        props2[i] = $"{name}.{props2[i]}";
                    }

                    props.AddRange(props2);
                }
                else
                {
                    props.Add(name);
                }
            }

            if (_filterDetailProperties != null && _filterDetailProperties.Count > 0)
            {
                props.AddRange(_filterDetailProperties);
            }

            _dynamicView = DynamicView.Create(type, props /*, EdmModel.DynamicViewCache */); // TODO: !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        /// <summary>
        /// ������ �������� ������� OData.
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <returns>�������� ������� OData.</returns>
        private ODataQueryContext CreateODataQueryContext(Type type)
        {
            // The EntitySetSegment type represents the Microsoft OData v5.7.0 EntitySetPathSegment type here.
            ODataPath path = new ODataPath(new EntitySetSegment(EdmModel.GetEdmEntitySet(EdmModel.GetEdmEntityType(type))));
            return new ODataQueryContext(EdmModel, type, path);
        }

        /// <summary>
        /// ������ ��������� <see cref="ODataServiceCore.Common.Exceptions.CustomException"/> � ����� 500 ��-���������, ���������� ��������� � ������� ������.
        /// ��� ��������� ������������� ���� ���������� ����������� ���������� CallbackAfterInternalServerError.
        /// </summary>
        /// <param name="exception">������ �������.</param>
        /// <returns>����� ��������� <see cref="ODataServiceCore.Common.Exceptions.CustomException"/>.</returns>
        private ODataServiceCore.Common.Exceptions.CustomException CustomException(Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            Exception originalException = exception;
            exception = ExecuteCallbackAfterInternalServerError(exception, ref code);

            if (exception == null)
            {
                exception = new Exception("Exception is null.");
            }

            LogService.LogError(originalException.Message, originalException);

            return new ODataServiceCore.Common.Exceptions.CustomException(exception, (int)code);
        }

        /// <summary>
        /// ���������� ����� ���������, ��������������� ���������� ������� OData.
        /// </summary>
        /// <returns>����� ���������.</returns>
        private IActionResult ExecuteExpression()
        {
            _objs = new DataObject[0];
            _lcs = CreateLcs();
            int count = -1;
            bool callExecuteCallbackBeforeGet = true;
            IncludeCount = false;
            if (QueryOptions.Count != null && QueryOptions.Count.Value)
            {
                IncludeCount = true;
                int returnTop = _lcs.ReturnTop;
                var rowNumber = _lcs.RowNumber;
                _lcs.RowNumber = null;
                _lcs.ReturnTop = 0;
                _objs = LoadObjects(_lcs, out count, callExecuteCallbackBeforeGet, true);
                _lcs.RowNumber = rowNumber;
                _lcs.ReturnTop = returnTop;
                callExecuteCallbackBeforeGet = false;
                Count = count;
            }

            if (!IncludeCount || count != 0)
                _objs = LoadObjects(_lcs, out count, callExecuteCallbackBeforeGet, false);

            NameValueCollection queryParams = QueryHelpers.QueryToNameValueCollection(Request.Query);

            if ((EdmModel.ExportService != null || EdmModel.ODataExportService != null) && (HttpContext.Items.ContainsKey(RequestHeadersHookMiddleware.AcceptApplicationMsExcel) || Convert.ToBoolean(queryParams.Get("exportExcel"))))
            {
                return CreateExcel(queryParams);
            }

            EdmEntityObjectCollection edmCol = GetEdmCollection(_objs, type, 1, null, _dynamicView);
            return Ok(edmCol);
        }

        /// <summary>
        /// ���������� �������� ��� ��������� ��������� �� ���� OData � �������.
        /// </summary>
        /// <returns>�������� ��� ��������� ���������.</returns>
        private IEdmObject EvaluateOdataPath()
        {
            // The EntitySetSegment type represents the Microsoft OData v5.7.0 EntitySetPathSegment type here.
            type = EdmModel.GetDataObjectType(HttpContext.ODataFeature().Path.Segments.OfType<EntitySetSegment>().First().Identifier);
            DetailArray detail = null;
            ODataPath odataPath = HttpContext.ODataFeature().Path;
            var keySegment = odataPath.Segments[1] as KeySegment;
            Guid key = new Guid(keySegment.Keys.First().Value.ToString());
            IEdmEntityType entityType = null;
            var obj = LoadObject(type, key);
            if (obj == null)
            {
                throw new InvalidOperationException("Not Found OData Path Segment " + 1);
            }

            bool returnCollection = false;
            for (int i = 2; i < odataPath.Segments.Count; i++)
            {
                type = obj.GetType();
                entityType = EdmModel.GetEdmEntityType(type);
                string propName = odataPath.Segments[i].ToString();
                EdmNavigationProperty navProp = (EdmNavigationProperty)entityType.FindProperty(propName);

                if (navProp.TargetMultiplicity() == EdmMultiplicity.One || navProp.TargetMultiplicity() == EdmMultiplicity.ZeroOrOne)
                {
                    DataObject master = (DataObject)obj.GetType().GetProperty(propName).GetValue(obj, null);
                    if (master == null)
                    {
                        View view = EdmModel.GetDataObjectDefaultView(obj.GetType());
                        obj = LoadObject(view, obj);
                    }

                    if (master == null)
                    {
                        throw new InvalidOperationException("Not Found OData Path Segment " + i);
                    }

                    if (master != null && EdmModel.GetDataObjectDefaultView(master.GetType()) != null)
                    {
                        master = LoadObject(EdmModel.GetDataObjectDefaultView(master.GetType()), master);
                    }

                    obj = master;
                }

                if (navProp.TargetMultiplicity() == EdmMultiplicity.Many)
                {
                    View view = EdmModel.GetDataObjectDefaultView(obj.GetType());
                    obj = LoadObject(view, obj);
                    detail = (DetailArray)obj.GetType().GetProperty(propName).GetValue(obj, null);
                    i++;
                    if (i == odataPath.Segments.Count)
                    {
                        returnCollection = true;
                        break;
                    }

                    keySegment = odataPath.Segments[i] as KeySegment;
                    key = new Guid(keySegment.Keys.First().Value.ToString());
                    obj = detail.GetAllObjects().FirstOrDefault(o => ((KeyGuid)o.__PrimaryKey).Guid == key);
                    if (obj == null)
                    {
                        throw new InvalidOperationException("Not Found OData Path Segment " + i);
                    }
                }
            }

            entityType = EdmModel.GetEdmEntityType(obj.GetType());
            if (returnCollection)
            {
                type = detail.ItemType;
            }
            else
            {
                type = obj.GetType();
            }

            QueryOptions = new ODataQueryOptions(new ODataQueryContext(EdmModel, type, HttpContext.ODataFeature().Path), Request);
            if (QueryOptions.SelectExpand != null && QueryOptions.SelectExpand.SelectExpandClause != null)
            {
                HttpContext.ODataFeature().SelectExpandClause = QueryOptions.SelectExpand.SelectExpandClause;
            }

            if (returnCollection)
            {
                IQueryable queryable = ApplyExpression(type, QueryOptions, detail.GetAllObjects());
                return GetEdmCollection(queryable, type, 1, null);
            }

            return GetEdmObject(entityType, obj, 1, null);
        }

        /// <summary>
        /// ���������� linq-��������� ��������������� ���������� ������� OData.
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <param name="queryOptions">��������� �������.</param>
        /// <returns>Linq-���������.</returns>
        private Expression GetExpression(Type type, ODataQueryOptions queryOptions)
        {
            MethodInfo methodToExpression = GetType().GetMethod("ToExpression").MakeGenericMethod(type);
            return (Expression)methodToExpression.Invoke(this, new object[] { queryOptions });
        }

        /// <summary>
        /// ���������� linq-��������� ��������������� ���������� ������� OData (������ ��� $filter).
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <param name="queryOptions">��������� �������.</param>
        /// <returns>Linq-���������.</returns>
        private Expression GetExpressionFilterOnly(Type type, ODataQueryOptions queryOptions)
        {
            MethodInfo methodToExpression = GetType().GetMethod("ToExpressionFilterOnly").MakeGenericMethod(type);
            return (Expression)methodToExpression.Invoke(this, new object[] { queryOptions });
        }

        private void GetPropertiesForDynamicView(ExpandedNavigationSelectItem parent, IEnumerable<SelectItem> selectedItems)
        {
            foreach (var item in selectedItems)
            {
                _parentExpandedNavigationSelectItem[item] = parent;
                _properties[item] = GetPropertyName(item);
                var expandedItem = CastExpandedNavigationSelectItem(item);
                if (expandedItem == null)
                {
                    continue;
                }

                GetPropertiesForDynamicView(expandedItem, expandedItem.SelectAndExpand.SelectedItems);
            }
        }

        private string GetPropertyName(SelectItem item)
        {
            if (item == null)
                return null;
            IEdmProperty itemProperty;
            PathSelectItem pathSelectItem = item as PathSelectItem;
            ExpandedNavigationSelectItem expandedItem = item as ExpandedNavigationSelectItem;
            if (pathSelectItem != null && pathSelectItem.SelectedPath.FirstSegment is NavigationPropertySegment)
            {
                itemProperty = (pathSelectItem.SelectedPath.FirstSegment as NavigationPropertySegment).NavigationProperty;
            }
            else
            {
                if (pathSelectItem != null)
                {
                    var firstSegment = pathSelectItem.SelectedPath.FirstSegment as PropertySegment;
                    if (firstSegment != null)
                    {
                        itemProperty = firstSegment.Property;
                    }
                    else
                    {
                        if (pathSelectItem.SelectedPath.FirstSegment is DynamicPathSegment dynamicPathSegment)
                        {
                            throw new Exception($"Property name does not exist: {dynamicPathSegment.Identifier}");
                        }

                        throw new Exception($"Invalid segment: {pathSelectItem.SelectedPath.FirstSegment.ToString()}");
                    }
                }
                else
                {
                    itemProperty = (expandedItem.PathToNavigationProperty.FirstSegment as NavigationPropertySegment).NavigationProperty;
                }
            }

            string itemName = EdmModel.GetDataObjectProperty(itemProperty.DeclaringType.FullTypeName(), itemProperty.Name).Name;
            string parentName = null;
            var parentExpandedItem = _parentExpandedNavigationSelectItem[item];
            while (parentExpandedItem != null)
            {
                IEdmProperty property = (parentExpandedItem.PathToNavigationProperty.FirstSegment as NavigationPropertySegment).NavigationProperty;
                string name = EdmModel.GetDataObjectProperty(property.DeclaringType.FullTypeName(), property.Name).Name;
                if (parentName == null)
                {
                    parentName = name;
                }
                else
                {
                    parentName = $"{name}.{parentName}";
                }

                parentExpandedItem = _parentExpandedNavigationSelectItem[parentExpandedItem];
            }

            if (parentName == null)
                return itemName;
            return $"{parentName}.{itemName}";
        }

        private IQueryable FilterApplyTo(FilterQueryOption filter, IQueryable query)
        {
            ODataQuerySettings querySettings = new ODataQuerySettings();
            IWebApiAssembliesResolver assembliesResolver = _defaultAssembliesResolver;
            if (query == null)
            {
                throw Error.ArgumentNull("query");
            }

            if (querySettings == null)
            {
                throw Error.ArgumentNull("querySettings");
            }

            if (assembliesResolver == null)
            {
                throw Error.ArgumentNull("assembliesResolver");
            }

            if (type == null)
            {
                throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, "ApplyTo");
            }

            FilterClause filterClause = filter.FilterClause;
            if (filterClause == null)
            {
                throw Error.ArgumentNull("filterClause");
            }

            // Ensure we have decided how to handle null propagation
            ODataQuerySettings updatedSettings = querySettings;
            if (querySettings.HandleNullPropagation == HandleNullPropagationOption.Default)
            {
                updatedSettings.HandleNullPropagation = HandleNullPropagationOptionHelper.GetDefaultHandleNullPropagationOption(query);
            }

            FilterBinder binder;
            try
            {
                binder = FilterBinder.Transform(filterClause, type, filter.Context.Model, assembliesResolver, updatedSettings);
            }
            catch (Exception ex)
            {
                LogService.LogDebug($"Failed to transform: {QueryOptions.Context.Path}", ex);
                throw;
            }

            _filterDetailProperties = binder.FilterDetailProperties;
            if (binder.IsOfTypesList.Count > 0)
            {
                _lcsLoadingTypes = EdmModel.GetTypes(binder.IsOfTypesList);
            }
            else
            {
                _lcsLoadingTypes.Clear();
            }
            query = ExpressionHelpers.Where(query, binder.LinqExpression, type);
            return query;
        }

        private bool HasPseudoproperty(IEdmEntityType entityType, string propertyName)
        {
            Type masterType = EdmLibHelpers.GetClrType(entityType.ToEdmTypeReference(true), EdmModel);
            IDataObjectEdmModelBuilder builder = (EdmModel as DataObjectEdmModel).EdmModelBuilder;

            return builder.GetPseudoDetail(masterType, propertyName) != null;
        }

        /// <summary>
        /// �������������� ���������� ������ ���������� �� ������� OData.
        /// </summary>
        private void Init()
        {
            // The EntitySetSegment type represents the Microsoft OData v5.7.0 EntitySetPathSegment type here.
            type = EdmModel.GetDataObjectType(Request.HttpContext.ODataFeature().Path.Segments.OfType<EntitySetSegment>().First().Identifier);
            QueryOptions = new ODataQueryOptions(new ODataQueryContext(EdmModel, type, Request.HttpContext.ODataFeature().Path), Request);
            try
            {
                var selectExpandClause = QueryOptions.SelectExpand?.SelectExpandClause;
                if (selectExpandClause != null)
                {
                    Request.HttpContext.ODataFeature().SelectExpandClause = selectExpandClause;
                }
            }
            catch (Exception e)
            {
                LogService.LogDebug($"Failed to get {nameof(SelectExpandQueryOption.SelectExpandClause)}: {QueryOptions.Context.Path}", e);
                throw;
            }

            CreateDynamicView();
        }

        /// <summary>
        /// ���������� ������ DataObject ��� ������� �����.
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <param name="key">���� ������� DataObject.</param>
        /// <returns>������ DataObject ��� ������� �����.</returns>
        private DataObject LoadObject(Type type, string key)
        {
            View view = EdmModel.GetDataObjectDefaultView(type);
            return LoadObject(type, view, key);
        }

        /// <summary>
        /// ���������� ������ DataObject ��� ������� �����.
        /// </summary>
        /// <param name="type">��� DataObject.</param>
        /// <param name="key">���� ������� DataObject.</param>
        /// <returns>������ DataObject ��� ������� �����.</returns>
        private DataObject LoadObject(Type type, Guid key)
        {
            View view = EdmModel.GetDataObjectDefaultView(type);
            return LoadObject(type, view, key);
        }

        private DataObject LoadObject(View view, DataObject obj)
        {
            return LoadObject(obj.GetType(), view, obj.__PrimaryKey);
        }

        /// <summary>
        /// �������� ������ ������ �� �����.
        /// </summary>
        /// <param name="objType"> ��� �������.</param>
        /// <param name="view"> �������������.</param>
        /// <param name="keyValue"> �������� �����.</param>
        /// <returns> ������ ������.</returns>
        private DataObject LoadObject(Type objType, View view, object keyValue)
        {
            LoadingCustomizationStruct lcs = LoadingCustomizationStruct.GetSimpleStruct(objType, _dynamicView.View);
            lcs.LimitFunction = FunctionBuilder.BuildEquals(keyValue);
            int count = -1;
            DataObject[] dobjs = LoadObjects(lcs, out count);
            if (dobjs.Length > 0)
                return dobjs[0];
            return null;
        }

        /// <summary>
        /// �������� ������� ��� ���������� �������� ��� �������� lcs.
        /// </summary>
        /// <param name="lcs">LoadingCustomizationStruct.</param>
        /// <param name="count">� ���� ��������� �������� ���������� ��������, ���� �������� callGetObjectsCount ���������� � true, ����� -1.</param>
        /// <param name="callExecuteCallbackBeforeGet">����� ����� �� ��������� ����� ExecuteCallbackBeforeGet.</param>
        /// <param name="callGetObjectsCount">����� ����� �� ��������� ����� GetObjectsCount ������ LoadObjects � ������� ������.</param>
        /// <param name="callExecuteCallbackAfterGet">����� ����� �� ��������� ����� ExecuteCallbackAfterGet.</param>
        /// <returns>���� �������� callGetObjectsCount ���������� � false, �� ������������ �������, ����� ������ ������ ��������.</returns>
        private DataObject[] LoadObjects(LoadingCustomizationStruct lcs, out int count, bool callExecuteCallbackBeforeGet = true, bool callGetObjectsCount = false, bool callExecuteCallbackAfterGet = true)
        {
            foreach (var propType in Information.GetAllTypesFromView(lcs.View))
            {
                if (!_dataService.SecurityManager.AccessObjectCheck(propType, tTypeAccess.Full, false))
                {
                    _dataService.SecurityManager.AccessObjectCheck(propType, tTypeAccess.Read, true);
                }
            }

            DataObject[] dobjs = new DataObject[0];
            bool doLoad = true;
            count = -1;
            if (callExecuteCallbackBeforeGet)
                doLoad = ExecuteCallbackBeforeGet(ref lcs);
            if (doLoad)
            {
                if (!callGetObjectsCount)
                {
                    dobjs = _dataService.LoadObjects(lcs, DataObjectCache);
                }
                else
                {
                    count = _dataService.GetObjectsCount(lcs);
                }
            }

            if (!OfflineManager.LockObjects(QueryOptions, dobjs))
                throw new OperationCanceledException(); // TODO

            if (callExecuteCallbackAfterGet)
                ExecuteCallbackAfterGet(ref dobjs);

            return dobjs;
        }
    }
}
