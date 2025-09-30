namespace NewPlatform.Flexberry.ORM.ODataService.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using ICSSoft.STORMNET.FunctionalLanguage.SQLWhere;

    /// <summary>
    /// Класс, содержащий расширения для сервиса данных.
    /// </summary>
    public static class DataServiceExtensions
    {
        /// <summary>
        /// Осуществляет вычитку объектов данных заданного типа по заданному представлению и LINQ-выражению.
        /// </summary>
        /// <param name="dataService">Экземпляр сервиса данных.</param>
        /// <param name="dataObjectType">Тип вычитываемых объектов данных, наследуемый от <see cref="DataObject"/>.</param>
        /// <param name="dataObjectView">Представления для вычитки объектов данных.</param>
        /// <param name="expression">LINQ-выражение, ограничивающее набор вычитываемых объектов.</param>
        /// <returns>Коллекция вычитанных объектов.</returns>
        public static object Execute(this SQLDataService dataService, Type dataObjectType, View dataObjectView, Expression expression)
        {
            IQueryProvider queryProvider = typeof(LcsQueryProvider<,>)
                .MakeGenericType(dataObjectType, typeof(LcsGeneratorQueryModelVisitor))
                .GetConstructor(new Type[3] { typeof(SQLDataService), typeof(View), typeof(IEnumerable<View>) })
                .Invoke(new object[3] { dataService, dataObjectView, null }) as IQueryProvider;

            return queryProvider.Execute(expression);
        }

        /// <summary>
        /// Загрузка объекта с его мастерами (объект должен быть не изменнённый и не до конца загруженный).
        /// С мастерами необходимо обращаться аккуратно: если в кэше уже есть мастер, то нужно эту ситуацию разрешить,
        /// поскольку иначе стандартная загрузка перетрёт данные мастера в кэше (и если он там изменён, то все изменения будут потеряны).
        /// </summary>
        /// <param name="dataService">Экземпляр сервиса данных.</param>
        /// <param name="view">Представление объекта с мастерами.</param>
        /// <param name="dobjectFromCache">Объект данных, в который будет производиться загрузка.</param>
        /// <param name="dataObjectCache">Текущий кэш объектов данных (в данном кэше ранее существующие там объекты не должны быть перетёрты).</param>
        public static void SafeLoadWithMasters(
            this IDataService dataService, View view, ICSSoft.STORMNET.DataObject dobjectFromCache, DataObjectCache dataObjectCache)
        {
            if (dataService == null)
            {
                throw new ArgumentNullException(nameof(dataService));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (dobjectFromCache == null)
            {
                throw new ArgumentNullException(nameof(dobjectFromCache));
            }

            if (dataObjectCache == null)
            {
                throw new ArgumentNullException(nameof(dataObjectCache));
            }

            // Прогружается пустой объект, чтобы избежать риска перетирания основного.
            DataObject createdObject = (DataObject)Activator.CreateInstance(dobjectFromCache.GetType());
            createdObject.SetExistObjectPrimaryKey(dobjectFromCache.__PrimaryKey);

            // Используется отдельный кэш, чтобы не перетереть данные в основном кэше.
            DataObjectCache specialCache = new DataObjectCache();
            specialCache.StartCaching(false);
            specialCache.AddDataObject(createdObject);
            dataService.LoadObject(view, createdObject, false, true, specialCache);
            specialCache.StopCaching();

            // Перенос данных из одного объекта в другой.
            ProperUpdateOfObject(dobjectFromCache, createdObject, dataObjectCache, specialCache);
        }

        /// <summary>
        /// Загрузка детейлов с сохранением состояния изменения.
        /// </summary>
        /// <param name="dataService">Экземпляр сервиса данных.</param>
        /// <param name="view">Представление агрегатора с детейлами. Чтение будет идти по массиву Details представлений детейлов, включая детейлы второго и последующих уровней.</param>
        /// <param name="agregators">Агрегаторы, для которых дочитываются детейлы.</param>
        /// <param name="dataObjectCache">Кеш объектов данных, в нём хранятся состояния детейлов, которые не надо затирать. В него же будут добавлены новые зачитанные детейлы.</param>
        public static void SafeLoadDetails(this IDataService dataService, View view, IList<DataObject> agregators, DataObjectCache dataObjectCache)
        {
            if (dataService == null)
            {
                throw new ArgumentNullException(nameof(dataService));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (agregators == null)
            {
                throw new ArgumentNullException(nameof(agregators));
            }

            if (dataObjectCache == null)
            {
                throw new ArgumentNullException(nameof(dataObjectCache));
            }

            if (agregators.Count == 0)
            {
                return;
            }

            // Обрабатываем все детейлы, которые указаны в представлении.
            DetailInView[] detailsInView = view.Details;
            foreach (DetailInView detailInView in detailsInView)
            {
                // Оригинальное представление будет использоваться в рекурсии.
                View detailView = detailInView.View.Clone();
                DetailInView[] secondDetailsInView = detailView.Details;

                // Удалим из представления детейлы второго уровня, поскольку их обработка будет рекурсивной с аналогичной логикой.
                foreach (DetailInView secondDetailInView in secondDetailsInView)
                {
                    detailView.RemoveDetail(secondDetailInView.Name);
                }

                Type detailType = detailView.DefineClassType;
                string agregatorPropertyName = Information.GetAgregatePropertyName(detailType);

                // Нужно гарантировать, что у загруженных детейлов будет проставлена ссылка на агрегатора, поэтому добавим свойство в представление (если уже есть, то второй раз не добавится).
                detailView.AddProperty(agregatorPropertyName);
                LoadingCustomizationStruct lcs = LoadingCustomizationStruct.GetSimpleStruct(detailView.DefineClassType, detailView);
                Type[] usageTypes = TypeUsageProvider.TypeUsage.GetUsageTypes(view.DefineClassType, detailInView.Name);
                List<Type> storedTypes = new List<Type>();
                foreach (var usageType in usageTypes)
                {
                    if (Information.IsStoredType(usageType))
                    {
                        storedTypes.Add(usageType);
                    }
                }

                if (!storedTypes.Any())
                {
                    foreach (DataObject agregator in agregators)
                    {
                        Type agregatorType = agregator.GetType();
                        Type[] usageTypesFromAgregator = TypeUsageProvider.TypeUsage.GetUsageTypes(agregatorType, detailInView.Name);
                        foreach (Type usageTypeFromAgregator in usageTypesFromAgregator)
                        {
                            if (Information.IsStoredType(usageTypeFromAgregator) && !storedTypes.Contains(usageTypeFromAgregator))
                            {
                                storedTypes.Add(usageTypeFromAgregator);
                            }
                        }
                    }
                }

                lcs.LoadingTypes = storedTypes.ToArray();
                Type agregatorKeyType = agregators[0].__PrimaryKey.GetType();

                // Производим обработку только тех агрегаторов, для которых ранее не был загружен детейл.
                object[] keys = agregators.Where(a => !a.CheckLoadedProperty(detailInView.Name)).Select(d => d.__PrimaryKey).ToArray();
                lcs.LimitFunction = FunctionBuilder.BuildIn(agregatorPropertyName, SQLWhereLanguageDef.LanguageDef.GetObjectTypeForNetType(agregatorKeyType), keys);

                // Нужно соблюсти единственность инстанций агрегаторов при вычитке, поэтому реализуем отдельный кеш. Смешивать с кешем dataObjectCache нельзя, поскольку в предстоящей выборке будут те же самые детейлы (значения в кеше затрутся).
                // Агрегаторы в кэш не помещаем. От помещения агрегаторов в кэш возникают неконтролируемые сбои основного кэша.
                DataObjectCache agregatorCache = new DataObjectCache();
                agregatorCache.StartCaching(false);

                // Вычитываются детейлы одного типа, но для нескольких инстанций агрегаторов (оптимизируем количество SQL-запросов).
                DataObject[] loadedDetails = dataService.LoadObjects(lcs, agregatorCache);
                agregatorCache.StopCaching();

                Dictionary<object, DataObject> extraCacheForAgregators = new Dictionary<object, DataObject>();
                foreach (DataObject agregator in agregators)
                {
                    agregator.AddLoadedProperties(detailInView.Name);
                    extraCacheForAgregators.Add(agregator.__PrimaryKey, agregator);
                }

                // Ввиду того, что агрегаторы нам пришли готовые с пустыми коллекциями детейлов, заполняем детейлы по агрегаторам значениями из кеша или из базы.
                // На загрузку детейлов второго уровня передаем только детейлы отсутствующие в кэше.
                List<DataObject> toLoadSecondDetails = new List<DataObject>();
                foreach (DataObject loadedDetail in loadedDetails)
                {
                    DataObject agregatorTemp = (DataObject)Information.GetPropValueByName(loadedDetail, agregatorPropertyName);
                    DataObject agregator = extraCacheForAgregators[agregatorTemp.__PrimaryKey];
                    object detailPrimaryKey = loadedDetail.__PrimaryKey;

                    DataObject detailFromCache = dataObjectCache.GetLivingDataObject(loadedDetail.GetType(), detailPrimaryKey);

                    // Необходимо игнорировать объекты-пустышки, которые проинициализированы только первичным ключом.
                    bool detailFromCacheIsEmpty = detailFromCache == null || !detailFromCache.GetLoadedProperties().Any();
                    DataObject detailForAdd = detailFromCacheIsEmpty
                        ? loadedDetail
                        : detailFromCache;

                    agregator.AddDetail(detailForAdd);

                    DataObject agregatorDataCopy = agregator.GetDataCopy();
                    DataObject detailDataCopy = detailForAdd.GetDataCopy();

                    if (agregatorDataCopy != null && detailDataCopy != null)
                    {
                        agregatorDataCopy.AddDetail(detailDataCopy);
                    }

                    if (detailFromCacheIsEmpty)
                    {
                        dataObjectCache.AddDataObject(loadedDetail);
                        toLoadSecondDetails.Add(loadedDetail);
                    }
                }

                // Детейлы второго и последующих уровней нужно обработать аналогичным образом.
                if (toLoadSecondDetails.Any() && secondDetailsInView.Any())
                {
                    dataService.SafeLoadDetails(detailInView.View, toLoadSecondDetails, dataObjectCache);
                }
            }
        }

        /// <summary>
        /// Догрузка объекта по указанному представлению, с загрузкой детейлов с сохранением состояния изменения.
        /// </summary>
        /// <param name="dataService">Экземпляр сервиса данных.</param>
        /// <param name="dataObject">Объект данных, который нужно догрузить.</param>
        /// <param name="view">Представление, которое используется для догрузки.</param>
        /// <param name="dataObjectCache">Кеш объектов данных.</param>
        public static void SafeLoadObject(this IDataService dataService, DataObject dataObject, View view, DataObjectCache dataObjectCache)
        {
            if (dataService == null)
            {
                throw new ArgumentNullException(nameof(dataService));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // Вычитывать объект сразу с детейлами нельзя, поскольку в этой же транзакции могут уже оказаться отдельные операции с детейлами и перевычитка затрёт эти изменения.
            View miniView = view.Clone();
            DetailInView[] miniViewDetails = miniView.Details;
            miniView.Details = new DetailInView[0];
            dataService.LoadObject(miniView, dataObject, false, true, dataObjectCache);

            if (miniViewDetails.Length > 0)
            {
                dataService.SafeLoadDetails(view, new DataObject[] { dataObject }, dataObjectCache);
            }
        }

        /// <summary>
        /// Add detail object to agregator according detail type.
        /// </summary>
        /// <param name="agregator">Agregator object.</param>
        /// <param name="detail">Detail object.</param>
        public static void AddDetail(this DataObject agregator, DataObject detail)
        {
            if (agregator == null)
            {
                throw new ArgumentNullException(nameof(agregator));
            }

            if (detail == null)
            {
                throw new ArgumentNullException(nameof(detail));
            }

            Type agregatorType = agregator.GetType();
            Type detailType = detail.GetType();
            string detailPropertyName = Information.GetDetailArrayPropertyName(agregatorType, detailType);

            Type parentType = detailType.BaseType;
            while (detailPropertyName == null && parentType != typeof(DataObject) && parentType != typeof(object) && parentType != null)
            {
                detailPropertyName = Information.GetDetailArrayPropertyName(agregatorType, parentType);
                parentType = parentType.BaseType;
            }

            if (detailPropertyName != null)
            {
                DetailArray details = (DetailArray)Information.GetPropValueByName(agregator, detailPropertyName);

                if (details != null)
                {
                    DataObject existDetail = details.GetByKey(detail.__PrimaryKey);

                    if (existDetail == null)
                    {
                        details.AddObject(detail);
                    }
                }
            }
            else
            {
                LogService.LogWarn($"Detail type {detailType.AssemblyQualifiedName} not found in agregator of type {agregatorType.AssemblyQualifiedName}.");
            }
        }


        /// <summary>
        /// Перенос означенных свойств из свежезагруженного объекта в основной, расположенный в основном кэше.
        /// </summary>
        /// <param name="currentObject">Основной объект, куда необходимо копировать значения свойств.</param>
        /// <param name="loadedObjectLocal">Свежезагруженный объект.</param>
        /// <param name="dataObjectCache">Основной кэш.</param>
        /// <param name="dataObjectCacheLocal">Локальный кэш, куда была выполнена свежая прогрузка.</param>
        /// <param name="processedDataObjects">Информация об уже обработанных сущностях (для защиты от рекурсивной обработки).</param>
        private static void ProperUpdateOfObject(
            DataObject currentObject,
            DataObject loadedObjectLocal,
            DataObjectCache dataObjectCache,
            DataObjectCache dataObjectCacheLocal,
            HashSet<TypeKeyTuple> processedDataObjects = null)
        {
            if (currentObject == null)
            {
                throw new ArgumentNullException(nameof(currentObject));
            }

            if (loadedObjectLocal == null)
            {
                throw new ArgumentNullException(nameof(loadedObjectLocal));
            }

            if (dataObjectCache == null)
            {
                throw new ArgumentNullException(nameof(dataObjectCache));
            }

            if (dataObjectCacheLocal == null)
            {
                throw new ArgumentNullException(nameof(dataObjectCacheLocal));
            }

            // Перенос значений свойств объекта (в том числе могут быть мастера). Если мастера означены, то перенос свойств мастера производится далее.
            List<string> localObjectLoadedProps = loadedObjectLocal.GetLoadedPropertiesList();
            List<string> currentObjectLoadedProps = currentObject.GetLoadedPropertiesList();
            List<string> notLoadedForActual = localObjectLoadedProps.Except(currentObjectLoadedProps).ToList();
            DataObject currentDataCopy = currentObject.GetDataCopy();
            foreach (string notLoadedPropName in notLoadedForActual)
            {
                object propValue = Information.GetPropValueByName(loadedObjectLocal, notLoadedPropName);
                Information.SetPropValueByName(currentObject, notLoadedPropName, propValue);
                currentObject.AddLoadedProperties(notLoadedPropName);
                Information.SetPropValueByName(currentDataCopy, notLoadedPropName, propValue);
            }

            processedDataObjects ??= new HashSet<TypeKeyTuple>();
            TypeKeyTuple dataForHash = new TypeKeyTuple(currentObject.GetType(), currentObject.__PrimaryKey);
            if (!processedDataObjects.Add(dataForHash))
            {
                return; // Найдена ссылка в цепочке объектов на ранее отсмотренный. Чтобы предотвратить рекурсию, далее не нужно загружать.
            }

            // Ещё могут быть частично загруженные мастера.
            ProperCacheUpdateForOneObject(dataObjectCache, dataObjectCacheLocal, loadedObjectLocal, true, processedDataObjects);
        }

        /// <summary>
        /// Обновление кэша по свежезагруженному объекту.
        /// </summary>
        /// <param name="dataObjectCacheActual">Текущий основной кэш объектов.</param>
        /// <param name="dataObjectCacheWithMasters">Вспомогательный кэш, куда загружался объект.</param>
        /// <param name="loadedDataObject">Свежезагруженный объект, по которому обновляется основной кэш.</param>
        /// <param name="loadedObjectsAdded">Флаг, определяющий, что в кэш уже добавлен свежезагруженный объект.</param>
        /// <param name="processedDataObjects">Информация об уже обработанных сущностях (для защиты от рекурсивной обработки).</param>
        private static void ProperCacheUpdateForOneObject(
            DataObjectCache dataObjectCacheActual,
            DataObjectCache dataObjectCacheWithMasters,
            DataObject loadedDataObject,
            bool loadedObjectsAdded,
            HashSet<TypeKeyTuple> processedDataObjects)
        {
            if (dataObjectCacheActual == null)
            {
                throw new ArgumentNullException(nameof(dataObjectCacheActual));
            }

            if (dataObjectCacheWithMasters == null)
            {
                throw new ArgumentNullException(nameof(dataObjectCacheWithMasters));
            }

            if (processedDataObjects == null)
            {
                throw new ArgumentNullException(nameof(processedDataObjects));
            }

            if (loadedDataObject == null)
            {
                return;
            }

            TypeKeyTuple dataForHash = new TypeKeyTuple(loadedDataObject.GetType(), loadedDataObject.__PrimaryKey);
            if (!processedDataObjects.Add(dataForHash))
            {
                return; // Найдена ссылка в цепочке объектов на ранее отсмотренный. Чтобы предотвратить рекурсию, далее не нужно загружать.
            }

            if (!loadedObjectsAdded)
            {
                dataObjectCacheActual.AddDataObject(loadedDataObject);
            }

            Type dobjType = typeof(DataObject);
            Type currentType = loadedDataObject.GetType();
            List<string> loadedProperties = loadedDataObject.GetLoadedPropertiesList();
            foreach (string currentPropertyName in loadedProperties)
            {
                Type currentPropertyType = Information.GetPropertyType(currentType, currentPropertyName);
                if (currentPropertyType.IsSubclassOf(dobjType)) // Выбираем у текущего объекта ссылки на мастеров.
                {
                    DataObject currentMaster = (DataObject)Information.GetPropValueByName(loadedDataObject, currentPropertyName);
                    if (currentMaster != null)
                    {
                        // Типы currentPropertyType и currentMaster.GetType() могут различаться из-за наследования.
                        DataObject masterFromActualCache = dataObjectCacheActual.GetLivingDataObject(currentMaster.GetType(), currentMaster.__PrimaryKey);

                        if (masterFromActualCache == null)
                        {
                            // Если мастера ранее не было в кэше, то просто его туда переносим.
                            dataObjectCacheActual.AddDataObject(currentMaster);

                            // Но в добавленном мастере могут быть мастера 2 и далее уровней.
                            ProperCacheUpdateForOneObject(dataObjectCacheActual, dataObjectCacheWithMasters, currentMaster, true, processedDataObjects);
                        }
                        else
                        { // Если мастер был в кэше, то аккуратно нужно перенести только незагруженные ранее свойства.
                            if (masterFromActualCache.GetStatus(false) == ObjectStatus.UnAltered && masterFromActualCache.GetLoadingState() != LoadingState.Loaded)
                            {
                                ProperUpdateOfObject(masterFromActualCache, currentMaster, dataObjectCacheActual, dataObjectCacheWithMasters, processedDataObjects);
                            }
                        }
                    }
                }
            }
        }

    }
}
