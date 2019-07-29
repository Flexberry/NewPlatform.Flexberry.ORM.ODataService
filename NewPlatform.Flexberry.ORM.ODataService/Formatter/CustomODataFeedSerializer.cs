namespace NewPlatform.Flexberry.ORM.ODataService.Formatter
{
    using Microsoft.AspNet.OData.Formatter.Serialization;
    using Microsoft.OData.Edm;

    /// <summary>
    /// OData serializer for serializing a collection of <see cref="IEdmEntityType" />
    /// </summary>
    internal class CustomODataFeedSerializer
    {
        /// <summary>
        /// Name for count property in Request.
        /// </summary>
        public const string Count = "CustomODataFeedSerializer_Count";

        /// <returns>
        /// The number of items in the feed.
        /// </returns>
        /// <summary>
        /// Initializes a new instance of <see cref="ODataFeedSerializer"/>.
        /// </summary>
        /// <param name="serializerProvider">The <see cref="ODataSerializerProvider"/> to use to write nested entries.</param>
        public CustomODataFeedSerializer(CustomODataSerializerProvider serializerProvider)
        {
        }
    }
}
