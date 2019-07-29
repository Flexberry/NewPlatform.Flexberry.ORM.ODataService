namespace NewPlatform.Flexberry.ORM.ODataService.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.OData.Routing.Conventions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;

    /// <summary>
    /// Класс, осуществляющий выбор контроллера и действий для OData-запросов.
    /// </summary>
    public class DataObjectRoutingConvention : EntityRoutingConvention
    {
        /// <summary>
        /// Осуществляет выбор действия, которое будет выполняться при запросе.
        /// </summary>
        /// <param name="odataPath">Путь запроса.</param>
        /// <param name="controllerContext">Сведения об HTTP-запросе в контексте контроллера.</param>
        /// <param name="actionMap">Соответствие имен действий с описанием их методов.</param>
        /// <returns>Имя действия, которое будет выполнятся при запросе или <c>null</c>, если данная конвенция не может подобрать нужное действие.</returns>
        public override string SelectAction(RouteContext routeContext, SelectControllerResult controllerResult, IEnumerable<ControllerActionDescriptor> actionDescriptors)
        {
            ODataPath odataPath = null;
            if (odataPath.Count > 0 && odataPath.Last() is OperationImportSegment)
            {
                return "GetODataFunctionsExecute";
            }

            if (odataPath.Count > 0 && odataPath.Last() is OperationSegment)
            {
                return "PostODataActionsExecute";
            }

            if ((odataPath.Count > 1 && odataPath.Last() is NavigationPropertyLinkSegment) ||
                (odataPath.Count > 2 && odataPath.Skip(odataPath.Count - 2).First() is NavigationPropertySegment))
            {
                if (odataPath.Last().EdmType is EdmCollectionType)
                    return "GetCollection";

                if (odataPath.Last().EdmType is EdmEntityType)
                    return "GetEntity";
            }

            RouteData rd = routeContext.RouteData;
            if (rd.Values.Count > 0 && rd.Values.Last().Value is OperationImportSegment)
            {
                return "GetODataFunctionsExecute";
            }

            if (rd.Values.Count > 0 && rd.Values.Last().Value is OperationSegment)
            {
                return "PostODataActionsExecute";
            }

            if ((rd.Values.Count > 1 && rd.Values.Last().Value is NavigationPropertySegment) || 
                (rd.Values.Count > 2 && rd.Values.Skip(rd.Values.Count - 2).Last().Value is NavigationPropertySegment))
            {
                if (odataPath.Last().EdmType is EdmCollectionType)
                    return "GetCollection";

                if (odataPath.Last().EdmType is EdmEntityType)
                    return "GetEntity";
            }


            if (routeContext.HttpContext.Request.Method == "GET")
            {
                Guid guid;
                if (Guid.TryParse(odataPath.Take(2).Last().ToString(), out guid))
                {
                    return "GetGuid";
                }
                else
                {
                    return "GetString";
                }
            }

            if (routeContext.HttpContext.Request.Method == "DELETE")
            {
                Guid guid;
                if (Guid.TryParse(odataPath.Take(2).Last().ToString(), out guid))
                {
                    return "DeleteGuid";
                }
                else
                {
                    return "DeleteString";
                }
            }


            string ret = base.SelectAction(routeContext, controllerResult, actionDescriptors);
            return ret;
        }
    }
}