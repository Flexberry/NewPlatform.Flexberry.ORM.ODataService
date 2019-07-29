namespace NewPlatform.Flexberry.ORM.ODataService.Formatter
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Web.Http;
    using Microsoft.AspNet.OData.Routing;
    using Microsoft.OData.Edm;
    using Microsoft.AspNet.OData.Formatter.Deserialization;
    using System;
    using Microsoft.AspNet.OData;
    using NewPlatform.Flexberry.ORM.ODataService.Expressions;
    using ICSSoft.STORMNET;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using Microsoft.OData;
    using Microsoft.OData.UriParser;

    /// <inheritdoc />
    public class ExtendedODataActionPayloadDeserializer : ODataDeserializer
    {
        private static readonly MethodInfo _castMethodInfo = typeof(Enumerable).GetMethod("Cast");

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataActionPayloadDeserializer"/> class.
        /// </summary>
        /// <param name="deserializerProvider">The deserializer provider to use to read inner objects.</param>
        public ExtendedODataActionPayloadDeserializer(ODataDeserializerProvider deserializerProvider)
            : base(ODataPayloadKind.Parameter)
        {
            DeserializerProvider = deserializerProvider ?? throw new ArgumentNullException(nameof(deserializerProvider));
        }

        /// <summary>
        /// Gets the deserializer provider to use to read inner objects.
        /// </summary>
        public ODataDeserializerProvider DeserializerProvider { get; private set; }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling",
            Justification = "The majority of types referenced by this method are EdmLib types this method needs to know about to operate correctly")]
        public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
        {
            if (messageReader == null)
            {
                throw new ArgumentNullException(nameof(messageReader));
            }

            IEdmAction action = GetAction(readContext);
            if (action == null)
            {
                throw new ArgumentException("Contract assertion not met: action != null", nameof(readContext));
            }

            // Create the correct resource type;
            Dictionary<string, object> payload;
            if (type == typeof(ODataActionParameters))
            {
                payload = new ODataActionParameters();
            }
            else
            {
                payload = new ODataUntypedActionParameters(action);
            }

            ODataParameterReader reader = messageReader.CreateODataParameterReader(action);

            while (reader.Read())
            {
                string parameterName = null;
                IEdmOperationParameter parameter = null;

                switch (reader.State)
                {
                    case ODataParameterReaderState.Value:
                        parameterName = reader.Name;
                        parameter = action.Parameters.SingleOrDefault(p => p.Name == parameterName);
                        // ODataLib protects against this but asserting just in case.
                        if (parameter == null)
                        {
                            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' not found.", parameterName), "value");
                        }

                        if (parameter.Type.IsPrimitive())
                        {
                            payload[parameterName] = reader.Value;
                        }
                        else
                        {
                            ODataEdmTypeDeserializer deserializer = DeserializerProvider.GetEdmTypeDeserializer(parameter.Type);
                            payload[parameterName] = deserializer.ReadInline(reader.Value, parameter.Type, readContext);
                        }
                        break;

                    case ODataParameterReaderState.Collection:
                        parameterName = reader.Name;
                        parameter = action.Parameters.SingleOrDefault(p => p.Name == parameterName);
                        // ODataLib protects against this but asserting just in case.
                        if (parameter == null)
                        {
                            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' not found.", parameterName), "value");
                        }

                        IEdmCollectionTypeReference collectionType = parameter.Type as IEdmCollectionTypeReference;
                        if (collectionType == null)
                        {
                            throw new ArgumentException("Contract assertion not met: collectionType != null", "value");
                        }

                        ODataCollectionValue value = ReadCollection(reader.CreateCollectionReader());
                        ODataCollectionDeserializer collectionDeserializer = (ODataCollectionDeserializer)DeserializerProvider.GetEdmTypeDeserializer(collectionType);
                        payload[parameterName] = collectionDeserializer.ReadInline(value, collectionType, readContext);
                        break;
                }
            }

            return payload;
        }

        internal static IEdmAction GetAction(ODataDeserializerContext readContext)
        {
            if (readContext == null)
            {
                throw Error.ArgumentNull("readContext");
            }

            Microsoft.AspNet.OData.Routing.ODataPath path = readContext.Path;
            if (path == null || path.Segments.Count == 0)
            {
                throw new SerializationException(SRResources.ODataPathMissing);
            }

            IEdmAction action = null;
            if (path.PathTemplate == "~/unboundaction")
            {
                // only one segment, it may be an unbound action
                OperationSegment unboundActionSegment = path.Segments.Last() as OperationSegment;
                if (unboundActionSegment != null)
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            if (action == null)
            {
                string message = Error.Format(SRResources.RequestNotActionInvocation, path.ToString());
                throw new SerializationException(message);
            }

            return action;
        }

        internal static ODataCollectionValue ReadCollection(ODataCollectionReader reader)
        {
            List<object> items = new List<object>();
            string typeName = null;

            while (reader.Read())
            {
                if (ODataCollectionReaderState.Value == reader.State)
                {
                    items.Add(reader.Item);
                }
                else if (ODataCollectionReaderState.CollectionStart == reader.State)
                {
                    typeName = reader.Item.ToString();
                }
            }

            return new ODataCollectionValue { Items = items, TypeName = typeName };
        }

        private DataObject CreateDataObject(DataObjectEdmModel model, IEdmEntityTypeReference entityTypeReference, ODataEntityReferenceLinks entry, out Type objType)
        {
            IEdmEntityType entityType = entityTypeReference.EntityDefinition();
            objType = model.GetDataObjectType(model.GetEdmEntitySet(entityType).Name);
            var obj = (DataObject)Activator.CreateInstance(objType);

            return obj;
        }

    }
}
