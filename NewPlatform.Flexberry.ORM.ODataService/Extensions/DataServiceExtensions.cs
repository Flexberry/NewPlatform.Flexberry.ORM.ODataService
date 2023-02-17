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
                lcs.LoadingTypes = TypeUsageProvider.TypeUsage.GetUsageTypes(view.DefineClassType, detailInView.Name);
                Type agregatorKeyType = agregators[0].__PrimaryKey.GetType();

                // Производим обработку только тех агрегаторов, для которых ранее не был загружен детейл.
                object[] keys = agregators.Where(a => !a.CheckLoadedProperty(detailInView.Name)).Select(d => d.__PrimaryKey).ToArray();
                lcs.LimitFunction = FunctionBuilder.BuildIn(agregatorPropertyName, SQLWhereLanguageDef.LanguageDef.GetObjectTypeForNetType(agregatorKeyType), keys);

                // Нужно соблюсти единственность инстанций агрегаторов при вычитке, поэтому реализуем отдельный кеш. Смешивать с кешем dataObjectCache нельзя, поскольку в предстоящей выборке будут те же самые детейлы (значения в кеше затрутся).
                DataObjectCache agregatorCache = new DataObjectCache();
                agregatorCache.StartCaching(false);
                foreach (DataObject agregator in agregators)
                {
                    agregatorCache.AddDataObject(agregator);
                }

                // Вычитываются детейлы одного типа, но для нескольких инстанций агрегаторов (оптимизируем количество SQL-запросов).
                DataObject[] loadedDetails = dataService.LoadObjects(lcs, agregatorCache);
                agregatorCache.StopCaching();

                foreach (DataObject agregator in agregators)
                {
                    agregator.AddLoadedProperties(detailInView.Name);
                }

                // Ввиду того, что агрегаторы нам пришли готовые с пустыми коллекциями детейлов, заполняем детейлы по агрегаторам значениями из кеша или из базы.
                // На загрузку детейлов второго уровня передаем только детейлы отсутствующие в кэше.
                List<DataObject> toLoadSecondDetails = new List<DataObject>();
                foreach (DataObject loadedDetail in loadedDetails)
                {
                    DataObject agregator = (DataObject)Information.GetPropValueByName(loadedDetail, agregatorPropertyName);
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
    }
}
