namespace NewPlatform.Flexberry.ORM.ODataService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using AdvLimit.ExternalLangDef;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;

    internal class DynamicView
    {
        /// <summary>
        /// Свойство в динамическом представлении.
        /// </summary>
        public class ViewProperty
        {
            /// <summary>
            /// Тип свойства.
            /// </summary>
            public Type type;

            /// <summary>
            /// Полное имя свойства.
            /// </summary>
            public string path;

            /// <summary>
            /// Если в полном имени свойства есть детейл, то тогда указывается его индекс, кроме 0, т.е. самой первой части.
            /// </summary>
            public int detailOfMasterIndex = -1;
        }

        /// <summary>
        /// Представление, в которое преобразуется динамическое представление.
        /// </summary>
        public View View => view;

        private List<ViewProperty> viewProperties;
        private View view;

        /// <summary>
        /// Создаётся новое представление, в которое добавляются свойства используемые в LINQ-выражении.
        /// </summary>
        /// <param name="expr">LINQ-выражение</param>
        /// <param name="dataObjectType">Тип</param>
        /// <param name="view">Начальное представление</param>
        /// <param name="dataService">Сервис данных</param>
        /// <param name="resolvingViews">Представления для разрешения сложных выражений, например, детейлов мастера.</param>
        /// <returns>Представление</returns>
        public static View GetViewWithPropertiesUsedInExpression(Expression expr, Type dataObjectType, View view, IDataService dataService, out IEnumerable<View> resolvingViews)
        {
            resolvingViews = null;

            List<View> agregatorsViews = null;
            IEnumerable<Expression> agregatorsExpressions = GetCastCallInExpression(expr);

            foreach (MethodCallExpression callExpression in agregatorsExpressions)
            {
                MemberExpression expression = callExpression.Arguments[0] as MemberExpression;

                // Если выражение содержит упоминание любого детейла, к которому применяется any, то надо вычислить представление агрегатора для метода LinqToLcs.GetLcs(...).
                View agregatorView = new View();
                agregatorView.DefineClassType = expression.Expression.Type;
                agregatorView.Name = "DynamicFromODataServiceForAgregator";

                View detailView = new View();
                detailView.DefineClassType = expression.Member.ReflectedType;
                detailView.Name = "DynamicFormOdataServiceForDetail";

                // TODO: Добавить свойства в представление - выбрать все свойства из лямбды.

                LambdaExpression lambdaExpression = callExpression.Arguments[1] as LambdaExpression;

                string propName = null;
                detailView.AddProperty(propName);

                string detailname = expression.Member.Name;
                agregatorView.AddDetailInView(detailname, detailView, true);

                if (agregatorsViews == null)
                {
                    agregatorsViews = new List<View>();
                }

                agregatorsViews.Add(agregatorView);
            }

            LoadingCustomizationStruct lcs;
            if (agregatorsViews == null)
            {
                lcs = LinqToLcs.GetLcs(expr, dataObjectType);
            }
            else
            {
                lcs = LinqToLcs.GetLcs(expr, view, agregatorsViews);
                resolvingViews = agregatorsViews;
            }

            if (lcs.ColumnsSort != null)
            {
                foreach (var sortDef in lcs.ColumnsSort)
                {
                    view.AddProperty(sortDef.Name, sortDef.Name, false, string.Empty);
                }
            }

            if (lcs.LimitFunction == null)
                return view;
            return ViewPropertyAppender.GetViewWithPropertiesUsedInFunction(view, lcs.LimitFunction, dataService);
        }

        /// <summary>
        /// Получить указание на агрегаторов в выражении.
        /// </summary>
        /// <param name="expression">Выражение, по которому пробегаемся.</param>
        /// <returns>Массив выражений, указывающих на агрегаторов.</returns>
        private static IEnumerable<Expression> GetCastCallInExpression(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (expression.NodeType == ExpressionType.Quote)
            {
                UnaryExpression expr = expression as UnaryExpression;
                return GetCastCallInExpression(expr.Operand);
            }

            if (expression.NodeType == ExpressionType.Lambda)
            {
                LambdaExpression expr = expression as LambdaExpression;
                return GetCastCallInExpression(expr.Body);
            }

            if (expression.NodeType == ExpressionType.Call)
            {
                MethodCallExpression expr = expression as MethodCallExpression;

                if (expr.Method.Name == "Cast")
                {
                    return new List<Expression>() { expr /*.Arguments[0] */ };
                }

                if (expr.Arguments.Count == 2 && expr.Arguments[0] is MethodCallExpression && (expr.Arguments[0] as MethodCallExpression).Method.Name == "Cast")
                {
                    return new List<Expression>() { expr /*.Arguments[0] */ };
                }
                else
                {
                    List<Expression> retList = null;
                    foreach (Expression argumentExpression in expr.Arguments)
                    {
                        IEnumerable<Expression> argumentExpressionList = GetCastCallInExpression(argumentExpression);
                        if (argumentExpressionList != null)
                        {
                            if (retList != null)
                            {
                                retList.AddRange(argumentExpressionList);
                            }
                            else
                            {
                                retList = argumentExpressionList as List<Expression>;
                            }
                        }
                    }

                    return retList;
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращает список свойств в типе.
        /// </summary>
        /// <param name="dataObjectType">Тип</param>
        /// <returns>Список свойств типа.</returns>
        public static List<string> GetProperties(Type dataObjectType)
        {
            var keyPropertyName = Information.ExtractPropertyName<DataObject>(n => n.__PrimaryKey);
            var excludedPropertiesNames = typeof(DataObject).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                .Where(n => n.Name != keyPropertyName)
                .Select(n => n.Name)
                .ToArray();

            Type viewsAttribute = dataObjectType.GetNestedType("Views");
            View defaultView = null;
            if (viewsAttribute != null)
            {
                PropertyInfo[] viewProperties = dataObjectType.GetNestedType("Views").GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
                if (viewProperties.Length > 0)
                    defaultView = (View)(viewProperties.FirstOrDefault(x => x.Name.EndsWith("E")) ?? viewProperties.First()).GetValue(null);
            }

            List<PropertyInfo> dataObjectProperties = new List<PropertyInfo>(dataObjectType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
            .Where(x => !excludedPropertiesNames.Contains(x.Name)
            && (x.CustomAttributes.Where(a => a.AttributeType == typeof(NotStoredAttribute)).Count() == 0 ||
            (defaultView != null && defaultView.Properties.Where(dvprop => dvprop.Name == x.Name).Count() > 0))));

            List<string> properties = new List<string>(dataObjectProperties.Select(x => x.Name).ToArray());

            if (dataObjectType.BaseType != typeof(DataObject) && dataObjectType.BaseType != typeof(object))
            {
                var parentProps = GetProperties(dataObjectType.BaseType);
                properties.AddRange(parentProps);
            }

            return properties;
        }

        /// <summary>
        /// Создаёт динамическое представление.
        /// </summary>
        /// <param name="dataObjectType">Тип</param>
        /// <param name="properties">Список полных имён свойств.</param>
        /// <param name="cache">Словарь с созданными динамическими представлениями, заполняется внутри Create.</param>
        /// <returns>Динамическое представление.</returns>
        public static DynamicView Create(Type dataObjectType, List<string> properties, Dictionary<string, DynamicView> cache = null)
        {
            if (properties == null)
                properties = GetProperties(dataObjectType);
            properties.Sort();
            var prevProp = string.Empty;
            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i] == prevProp)
                {
                    properties.RemoveAt(i);
                    i--;
                    continue;
                }

                prevProp = properties[i];
            }

            var key = CreateKey(dataObjectType, properties);
            if (cache != null && cache.ContainsKey(key))
                return cache[key];

            var view = new DynamicView(dataObjectType, properties, cache);
            if (cache != null)
                cache.Add(key, view);
            return view;
        }

        /// <summary>
        /// Проверяет принадлежность свойства данному View, а также входящим в него DetailInView.
        /// </summary>
        /// <param name="view">Представление.</param>
        /// <param name="fullPathProperty">Полный путь свойства.</param>
        /// <returns>Возвращает true, если представление содержит данное свойство.</returns>
        public static bool ContainsPoperty(View view, string fullPathProperty)
        {
            if (string.IsNullOrWhiteSpace(fullPathProperty))
                return false;
            var path = fullPathProperty.Split(new[] { '.' });
            if (path.Length == 1)
            {
                return view.CheckPropname(fullPathProperty, true);
            }

            if (view.Masters.FirstOrDefault(p => p.MasterName == path[0]).MasterName == path[0])
            {
                var master = path[0];
                for (int i = 1; i < path.Length; i++)
                {
                    master += $".{path[i]}";
                    if (Information.GetPropertyType(view.DefineClassType, master).IsSubclassOf(typeof(DetailArray)))
                        return false;
                }

                return view.CheckPropname(fullPathProperty);
            }

            if (view.Details.FirstOrDefault(p => p.Name == path[0]).Name == path[0])
            {
                return ContainsPoperty(view.GetDetail(path[0]).View, fullPathProperty.Substring(path[0].Length + 1));
            }

            return false;
        }

        private static string CreateKey(Type dataObjectType, List<string> properties)
        {
            return $"{dataObjectType.FullName}({string.Join(",", properties)})";
        }

        private DynamicView(Type dataObjectType, List<string> properties, Dictionary<string, DynamicView> cache)
        {
            CreateKey(dataObjectType, properties);
            ViewProperty viewProp;
            view = new View() { DefineClassType = dataObjectType };
            viewProperties = new List<ViewProperty>();
            string[] path;

            for (int p = 0; p < properties.Count; p++)
            {
                var prop = properties[p];
                viewProp = new ViewProperty() { path = prop };
                viewProperties.Add(viewProp);
                path = prop.Split(new[] { '.' });
                Type[] propTypes = new Type[path.Length];
                string testedProp = null;

                for (int i = 0; i < path.Length; i++)
                {
                    var pathSegment = path[i];
                    if (testedProp == null)
                    {
                        testedProp = pathSegment;
                    }
                    else
                    {
                        testedProp += $".{pathSegment}";
                    }

                    propTypes[i] = Information.GetPropertyType(dataObjectType, testedProp);
                    if (propTypes[i].IsSubclassOf(typeof(DetailArray)))
                    {
                        if (i != 0 && viewProp.detailOfMasterIndex == -1)
                        {
                            viewProp.detailOfMasterIndex = i;
                        }
                    }
                }

                var propType = propTypes[propTypes.Length - 1];
                viewProp.type = propType;
                if (propTypes[0].IsSubclassOf(typeof(DetailArray)))
                {
                    var detailSegment = path[0];
                    var propList = new List<string>();
                    if (path.Length > 1)
                        propList.Add(prop.Substring(detailSegment.Length + 1));

                    int pp = p + 1;
                    for (; pp < properties.Count; pp++)
                    {
                        if (properties[pp].IndexOf($"{detailSegment}.") != 0)
                            break;
                        propList.Add(properties[pp].Substring(detailSegment.Length + 1));
                    }

                    p = pp - 1;
                    var itemType = propTypes[0].GetProperty("Item", new[] { typeof(int) }).PropertyType;
                    propList.AddRange(GetProperties(itemType));
                    view.AddDetailInView(detailSegment, Create(itemType, propList, cache).View, true, "", true, "", null);
                }
                else
                {
                    if (viewProp.detailOfMasterIndex == -1)
                    {
                        if (propType.IsSubclassOf(typeof(DataObject)))
                        {
                            view.AddMasterInView(prop);
                            view.AddProperty(prop, prop, true, string.Empty);
                            view.AddProperty($"{prop}.{nameof(DataObject.__PrimaryKey)}");
                        }
                        else
                        {
                            view.AddProperty(prop, prop, true, string.Empty);
                        }
                    }
                }
            }
        }
    }
}
