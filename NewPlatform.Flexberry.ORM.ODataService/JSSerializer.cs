namespace NewPlatform.Flexberry.ORM.ODataService
{
    using System;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Internal singleton serializer to JSON format.
    /// </summary>
    internal static class JSSerializer
    {
        /// <summary>
        /// Singleton instance of serializer.
        /// </summary>
        private static readonly Lazy<JavaScriptSerializer> _serializer = new Lazy<JavaScriptSerializer>(() => new JavaScriptSerializer { MaxJsonLength = int.MaxValue });

        /// <summary>
        /// Converts the object to JSON string.
        /// </summary>
        /// <param name="object">The object to serialize.</param>
        /// <returns>JSON string that represents the object.</returns>
        public static string Serialize(object @object)
        {
            return _serializer.Value.Serialize(@object);
        }

        /// <summary>
        /// Converts the specified JSON string to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the resulting object.</typeparam>
        /// <param name="json">The JSON string to be deserialized.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json)
        {
            return _serializer.Value.Deserialize<T>(json);
        }
    }
}
