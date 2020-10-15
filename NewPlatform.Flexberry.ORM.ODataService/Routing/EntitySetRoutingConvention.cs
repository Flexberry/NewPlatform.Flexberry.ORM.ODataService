// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

namespace NewPlatform.Flexberry.ORM.ODataService.Routing
{
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Microsoft.OData.Edm;
    //using System.Web.OData.Routing.Conventions;
    using System.Web.OData.Routing;
    using System;

    /// <summary>
    /// An implementation of <see cref="IODataRoutingConvention"/> that handles entity sets.
    /// </summary>
    public class EntitySetRoutingConvention : NavigationSourceRoutingConvention
    {
        /// <summary>
        /// Selects the action for OData requests.
        /// </summary>
        /// <param name="odataPath">The OData path.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionMap">The action map.</param>
        /// <returns>
        ///   <c>null</c> if the request isn't handled by this convention; otherwise, the name of the selected action
        /// </returns>
        public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (odataPath == null)
            {
                //throw Error.ArgumentNull("odataPath");
                throw new ArgumentNullException("odataPath");
            }

            if (controllerContext == null)
            {
                //throw Error.ArgumentNull("controllerContext");
                throw new ArgumentNullException("controllerContext");
            }

            if (actionMap == null)
            {
                //throw Error.ArgumentNull("actionMap");
                throw new ArgumentNullException("actionMap");
            }

            if (odataPath.PathTemplate == "~/entityset")
            {
                EntitySetPathSegment entitySetSegment = (EntitySetPathSegment)odataPath.Segments[0];
                IEdmEntitySetBase entitySet = entitySetSegment.EntitySetBase;
                HttpMethod httpMethod = controllerContext.Request.Method;

                if (httpMethod == HttpMethod.Get)
                {
                    // e.g. Try GetCustomers first, then fall back to Get action name
                    //return actionMap.FindMatchingAction(
                    //    "Get" + entitySet.Name,
                    //    "Get");
                    throw new NotImplementedException("-solo- conventions");
                }
                else if (httpMethod == HttpMethod.Post)
                {
                    // e.g. Try PostCustomer first, then fall back to Post action name
                    //return actionMap.FindMatchingAction(
                    //    "Post" + entitySet.EntityType().Name,
                    //    "Post");
                    throw new NotImplementedException("-solo- conventions");
                }
            }
            else if (odataPath.PathTemplate == "~/entityset/$count" &&
                controllerContext.Request.Method == HttpMethod.Get)
            {
                EntitySetPathSegment entitySetSegment = (EntitySetPathSegment)odataPath.Segments[0];
                IEdmEntitySetBase entitySet = entitySetSegment.EntitySetBase;

                // e.g. Try GetCustomers first, then fall back to Get action name
                //return actionMap.FindMatchingAction(
                //    "Get" + entitySet.Name,
                //    "Get");
                throw new NotImplementedException("-solo- conventions");
            }
            else if (odataPath.PathTemplate == "~/entityset/cast")
            {
                EntitySetPathSegment entitySetSegment = (EntitySetPathSegment)odataPath.Segments[0];
                IEdmEntitySetBase entitySet = entitySetSegment.EntitySetBase;
                IEdmCollectionType collectionType = (IEdmCollectionType)odataPath.EdmType;
                IEdmEntityType entityType = (IEdmEntityType)collectionType.ElementType.Definition;
                HttpMethod httpMethod = controllerContext.Request.Method;

                if (httpMethod == HttpMethod.Get)
                {
                    // e.g. Try GetCustomersFromSpecialCustomer first, then fall back to GetFromSpecialCustomer
                    //return actionMap.FindMatchingAction(
                    //    "Get" + entitySet.Name + "From" + entityType.Name,
                    //    "GetFrom" + entityType.Name);
                    throw new NotImplementedException("-solo- conventions");
                }
                else if (httpMethod == HttpMethod.Post)
                {
                    // e.g. Try PostCustomerFromSpecialCustomer first, then fall back to PostFromSpecialCustomer
                    //return actionMap.FindMatchingAction(
                    //    "Post" + entitySet.EntityType().Name + "From" + entityType.Name,
                    //    "PostFrom" + entityType.Name);
                    throw new NotImplementedException("-solo- conventions");
                }
            }
            else if (odataPath.PathTemplate == "~/entityset/cast/$count" &&
                controllerContext.Request.Method == HttpMethod.Get)
            {
                EntitySetPathSegment entitySetSegment = (EntitySetPathSegment)odataPath.Segments[0];
                IEdmEntitySetBase entitySet = entitySetSegment.EntitySetBase;
                IEdmCollectionType collectionType = (IEdmCollectionType)odataPath.Segments[1].GetEdmType(
                    entitySetSegment.GetEdmType(previousEdmType: null));
                IEdmEntityType entityType = (IEdmEntityType)collectionType.ElementType.Definition;

                // e.g. Try GetCustomersFromSpecialCustomer first, then fall back to GetFromSpecialCustomer
                //return actionMap.FindMatchingAction(
                //    "Get" + entitySet.Name + "From" + entityType.Name,
                //    "GetFrom" + entityType.Name);
                throw new NotImplementedException("-solo- conventions");
            }

            return null;
        }
    }
}
