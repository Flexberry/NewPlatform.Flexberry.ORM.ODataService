namespace NewPlatform.Flexberry.ORM.ODataService.Formatter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Formatter.Deserialization;
    using Microsoft.AspNet.OData.Formatter.Serialization;
    using Microsoft.AspNetCore.Http;

    /// <inheritdoc/>
    public class ExtendedODataDeserializerProvider : DefaultODataDeserializerProvider
    {
        public ExtendedODataDeserializerProvider(IServiceProvider rootContainer) : base(rootContainer)
        { }

        /// <inheritdoc/>
        public override ODataEdmTypeDeserializer GetEdmTypeDeserializer(Microsoft.OData.Edm.IEdmTypeReference edmType)
        {
            return base.GetEdmTypeDeserializer(edmType);
        }

        /// <inheritdoc/>
        public override ODataDeserializer GetODataDeserializer(Type type, HttpRequest request)
        {
            if (type == typeof(Uri))
            {
                return base.GetODataDeserializer(type, request);
            }

            if (type == typeof(ODataActionParameters) ||
                type == typeof(ODataUntypedActionParameters))
            {
                return new ExtendedODataActionPayloadDeserializer(this);
            }

            return new ExtendedODataEntityDeserializer(this);
        }
    }
}
