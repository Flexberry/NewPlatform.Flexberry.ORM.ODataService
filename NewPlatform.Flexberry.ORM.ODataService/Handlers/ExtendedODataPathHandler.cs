namespace NewPlatform.Flexberry.ORM.ODataService.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    //using Microsoft.AspNet.OData.Routing;
    //using Microsoft.Data.Edm;
    //using Microsoft.Data.OData;
    //using Microsoft.Data.OData.Query;
    //using Microsoft.Data.OData.Query.SemanticAst;
    using Microsoft.OData.Edm;
    using Microsoft.OData;
    //using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;
    //using Microsoft.OData.Core;
    //using Microsoft.OData.Core.UriParser;
    //using Microsoft.OData.Core.UriParser.Metadata;
    //using Microsoft.OData.Core.UriParser.TreeNodeKinds;
    //using Microsoft.OData.Edm;
    //using Microsoft.OData.UriParser;
    using NewPlatform.Flexberry.ORM.ODataService.Core.Expressions;
    using NewPlatform.Flexberry.ORM.ODataService.Expressions;
    using Microsoft.AspNet.OData.Routing;

    //using Semantic = Microsoft.OData.Core.UriParser.Semantic;
    //using SingleValueNode = Microsoft.OData.Core.UriParser.Semantic.SingleValueNode;

    /// <inheritdoc cref="DefaultODataPathHandler"/>
    public class ExtendedODataPathHandler : DefaultODataPathHandler
    {
        private ODataUriResolverSetttings _resolverSettings = new ODataUriResolverSetttings();

        /// <inheritdoc cref="DefaultODataPathHandler"/>
        //public override ODataPath Parse(IEdmModel model, string serviceRoot, string odataPath)
        //{
        //    if (model == null)
        //    {
        //        throw Error.ArgumentNull("model");
        //    }

        //    if (serviceRoot == null)
        //    {
        //        throw Error.ArgumentNull("serviceRoot");
        //    }

        //    if (odataPath == null)
        //    {
        //        throw Error.ArgumentNull("odataPath");
        //    }

        //    ODataPath path = null;
        //    path = Parse(model, serviceRoot, odataPath, _resolverSettings, false);
        //    return path;
        //}

        //private static ODataPath Parse(
        //    IEdmModel model,
        //    string serviceRoot,
        //    string odataPath,
        //    ODataUriResolverSetttings resolverSettings,
        //    bool enableUriTemplateParsing)
        public override Microsoft.AspNet.OData.Routing.ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer)
        //public override Microsoft.OData.UriParser.ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer)
        {

            IEdmModel model = requestContainer.GetService(typeof(IEdmModel)) as IEdmModel;
            ODataUriParser uriParser;
            Uri serviceRootUri = null;
            Uri fullUri = null;
            NameValueCollection queryString = null;

            //if (enableUriTemplateParsing)
            //{
            //    uriParser = new Microsoft.Data.OData.Query.ODataUriParser(model, new Uri(odataPath, UriKind.Relative));
            //    uriParser.EnableUriTemplateParsing = true;
            //}
            //else
            //{
                Contract.Assert(serviceRoot != null);

                serviceRootUri = new Uri(
                    serviceRoot.EndsWith("/", StringComparison.Ordinal) ?
                        serviceRoot :
                        serviceRoot + "/");
                /*Из-за ошибки в mono, при вызове конструктора new Uri(serviceRootUri, odataPath);
                 * будем использовать конструктор new Uri($"{serviceRootUri}{odataPath}");
                 */
                fullUri = new Uri($"{serviceRootUri}{odataPath}");
                queryString = fullUri.ParseQueryString();
                uriParser = new ODataUriParser(model, serviceRootUri);
            //              uriParser = new ODataUriParser(model, serviceRootUri, fullUri);
            //}

            //uriParser. Resolver = resolverSettings.CreateResolver(model);

            Microsoft.OData.UriParser.ODataPath path;
            UnresolvedPathSegment unresolvedPathSegment = null;
            KeySegment id = null;
            try
            {
                path = uriParser.ParsePath();// fullUri);
            }
            catch (ODataUnrecognizedPathException ex)
            {
                //if (ex.ParsedSegments != null &&
                //    ex.ParsedSegments.Count() > 0 &&
                //    (ex.ParsedSegments.Last().EdmType is IEdmComplexType ||
                //     ex.ParsedSegments.Last().EdmType is IEdmEntityType) &&
                //    ex.CurrentSegment != ODataSegmentKinds.Count)
                //{
                //    if (ex.UnparsedSegments.Count() == 0)
                //    {
                //        path = new Microsoft.Data.OData.Query.SemanticAst.ODataPath(ex.ParsedSegments);
                //        unresolvedPathSegment = new UnresolvedPathSegment(ex.CurrentSegment);
                //    }
                //    else
                //    {
                //        // Throw ODataException if there is some segment following the unresolved segment.
                //        throw new ODataException(Error.Format(
                //            SRResources.InvalidPathSegment,
                //            ex.UnparsedSegments.First(),
                //            ex.CurrentSegment));
                //    }
                //}
                //else
                //{
                    throw;
               //}
            }

            if (//!enableUriTemplateParsing && 
                path.LastSegment is NavigationPropertyLinkSegment)
            {
                IEdmCollectionType lastSegmentEdmType = path.LastSegment.EdmType as IEdmCollectionType;

                if (lastSegmentEdmType != null)
                {
                    EntityIdSegment entityIdSegment = null;
                    bool exceptionThrown = false;

                    try
                    {
                        entityIdSegment = uriParser.ParseEntityId();

                        if (entityIdSegment != null)
                        {
                            // Create another ODataUriParser to parse $id, which is absolute or relative.
                            ODataUriParser parser = new ODataUriParser(model, serviceRootUri);
//                          Microsoft.Data.OData.Query.ODataUriParser parser = new Microsoft.Data.OData.Query.ODataUriParser(model, serviceRootUri, entityIdSegment.Id);
                            id = parser.ParsePath().LastSegment as KeySegment;
                        }
                    }
                    catch (ODataException)
                    {
                        // Exception was thrown while parsing the $id.
                        // We will throw another exception about the invalid $id.
                        exceptionThrown = true;
                    }

                    if (exceptionThrown ||
                        (entityIdSegment != null &&
                            (id == null ||
                                !(id.EdmType.IsOrInheritsFrom(lastSegmentEdmType.ElementType.Definition) ||
                                  lastSegmentEdmType.ElementType.Definition.IsOrInheritsFrom(id.EdmType)))))
                    {
                        throw new ODataException(Error.Format(SRResources.InvalidDollarId, queryString.Get("$id")));
                    }
                }
            }


            //ODataPath webAPIPath = ODataPathSegmentTranslator.TranslateODataLibPathToWebApiPath(
            //    path,
            //    model,
            //    unresolvedPathSegment,
            //    id,
            //    enableUriTemplateParsing,
            //    uriParser.ParameterAliasNodes);
            Microsoft.AspNet.OData.Routing.ODataPath webAPIPath =null; 
            //= ODataPathSegmentTranslator.Translate(
            //    model,
            //    path,
            //    null);

            CheckNavigableProperty(webAPIPath, model);
            return webAPIPath;

        }

        private static void CheckNavigableProperty(Microsoft.AspNet.OData.Routing.ODataPath path,  IEdmModel model)
        {
            Contract.Assert(path != null);
            Contract.Assert(model != null);

            foreach (ODataPathSegment segment in path.Segments)
            {
                NavigationPropertySegment navigationPathSegment = segment as NavigationPropertySegment;

                if (navigationPathSegment != null)
                {
                    if (EdmLibHelpers.IsNotNavigable(navigationPathSegment.NavigationProperty, model))
                    {
                        throw new ODataException(Error.Format(
                            SRResources.NotNavigablePropertyUsedInNavigation,
                            navigationPathSegment.NavigationProperty.Name));
                    }
                }
            }
        }


    }

    internal class ODataUriResolverSetttings
    {
        public bool CaseInsensitive { get; set; }

        public bool UnqualifiedNameCall { get; set; }

        public bool EnumPrefixFree { get; set; }

        public bool AlternateKeys { get; set; }

        public Microsoft.OData.UriParser.ODataUriResolver CreateResolver(IEdmModel model)
        {
            Microsoft.OData.UriParser.ODataUriResolver resolver;
            if (UnqualifiedNameCall && EnumPrefixFree)
            {
                resolver = new UnqualifiedCallAndEnumPrefixFreeResolver();
            }
            else if (UnqualifiedNameCall)
            {
                resolver = new Microsoft.OData.UriParser.UnqualifiedODataUriResolver();
            }
            else if (EnumPrefixFree)
            {
                resolver = new Microsoft.OData.UriParser.StringAsEnumResolver();
            }
            else if (AlternateKeys)
            {
                resolver = new Microsoft.OData.UriParser.AlternateKeysODataUriResolver(model);
            }
            else
            {
                resolver = new Microsoft.OData.UriParser.ODataUriResolver();
            }

            resolver.EnableCaseInsensitive = CaseInsensitive;
            return resolver;
        }
    }

    internal class UnqualifiedCallAndEnumPrefixFreeResolver : Microsoft.OData.UriParser.ODataUriResolver
    {
        private readonly Microsoft.OData.UriParser.StringAsEnumResolver _stringAsEnum = new Microsoft.OData.UriParser.StringAsEnumResolver();
        private readonly Microsoft.OData.UriParser.UnqualifiedODataUriResolver _unqualified = new Microsoft.OData.UriParser.UnqualifiedODataUriResolver();

        private bool _enableCaseInsensitive;

        public override bool EnableCaseInsensitive
        {
            get { return this._enableCaseInsensitive; }
            set
            {
                this._enableCaseInsensitive = value;
                _stringAsEnum.EnableCaseInsensitive = this._enableCaseInsensitive;
                _unqualified.EnableCaseInsensitive = this._enableCaseInsensitive;
            }
        }


        public override IEnumerable<IEdmOperation> ResolveBoundOperations(Microsoft.OData.Edm.IEdmModel model, string identifier, Microsoft.OData.Edm.IEdmType bindingType)
        {
            return _unqualified.ResolveBoundOperations(model, identifier, bindingType);
        }

        public override void PromoteBinaryOperandTypes(Microsoft.OData.UriParser.BinaryOperatorKind binaryOperatorKind, ref Microsoft.OData.UriParser.SingleValueNode leftNode, ref Microsoft.OData.UriParser.SingleValueNode rightNode, out Microsoft.OData.Edm.IEdmTypeReference typeReference)
        {
            _stringAsEnum.PromoteBinaryOperandTypes(binaryOperatorKind, ref leftNode, ref rightNode, out typeReference);
        }

        public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(Microsoft.OData.Edm.IEdmEntityType type, IDictionary<string, string> namedValues, Func<Microsoft.OData.Edm.IEdmTypeReference, string, object> convertFunc)
        {
            return _stringAsEnum.ResolveKeys(type, namedValues, convertFunc);
        }

        public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(Microsoft.OData.Edm.IEdmEntityType type, IList<string> positionalValues, Func<Microsoft.OData.Edm.IEdmTypeReference, string, object> convertFunc)
        {
            return _stringAsEnum.ResolveKeys(type, positionalValues, convertFunc);
        }


        public override IDictionary<IEdmOperationParameter, Microsoft.OData.UriParser.SingleValueNode> ResolveOperationParameters(IEdmOperation operation, IDictionary<string, Microsoft.OData.UriParser.SingleValueNode> input)
        {
            return _stringAsEnum.ResolveOperationParameters(operation, input);
        }
    }
}
