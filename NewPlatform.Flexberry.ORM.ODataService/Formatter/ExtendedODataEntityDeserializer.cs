namespace NewPlatform.Flexberry.ORM.ODataService.Formatter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Formatter.Serialization;
    using Microsoft.AspNet.OData.Formatter.Deserialization;
    using Microsoft.OData.Edm;

    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using Expressions;
    using Microsoft.OData;


    /// <summary>
    /// Десериализатор для чтения передаваемых данных OData.
    /// </summary>
    public class ExtendedODataEntityDeserializer : ODataResourceDeserializer
    {
        /// <summary>
        /// Строковая константа, которая используется для доступа свойствам запроса.
        /// </summary>
        public const string Dictionary = "ExtendedODataEntityDeserializer_Dictionary";

        /// <summary>
        /// Строковая константа, которая используется для доступа свойствам запроса.
        /// </summary>
        public const string OdataBindNull = "ExtendedODataEntityDeserializer_OdataBindNull";

        /// <summary>
        /// Строковая константа, которая используется для доступа свойствам запроса.
        /// </summary>
        public const string ReadException = "ExtendedODataEntityDeserializer_ReadException";

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="deserializerProvider">Провайдер.</param>
        public ExtendedODataEntityDeserializer(ODataDeserializerProvider deserializerProvider)
            : base(deserializerProvider)
        {
        }

        /// <summary>
        /// Выполняет чтение передаваемых данных OData.
        /// </summary>
        /// <param name="messageReader">messageReader, который будет использован для чтения.</param>
        /// <param name="type">Тип передаваемых данных.</param>
        /// <param name="readContext">Состояние и установки, используемые при чтении.</param>
        /// <returns>Преобразованные данные.</returns>
        public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
        {
            object obj = null;
            try
            {
                obj = base.Read(messageReader, type, readContext);
            }
            catch (Exception ex)
            {
                if (ex is ODataException && ex.ToString().IndexOf("odata.bind") != -1)
                {
                    readContext.Request.HttpContext.Items.Add(OdataBindNull, readContext.ResourceEdmType);
                    return null;
                }

                readContext.Request.HttpContext.Items.Add(ReadException, ex);
            }

            return obj;
        }

        /// <summary>
        /// Десериалезует <paramref name="structuralProperty"/> в <paramref name="entityResource"/>.
        /// </summary>
        /// <param name="resource">Объект, в который  structural property будет прочитано.</param>
        /// <param name="structuralProperty">Объект содержащий structural properties.</param>
        /// <param name="entityType">Тип сущности.</param>
        /// <param name="readContext">Состояние и установки, используемые при чтении.</param>
        public override void ApplyStructuralProperty(object resource, ODataProperty structuralProperty, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
        {
            if (resource == null)
            {
                throw Error.ArgumentNull("entityResource");
            }

            if (structuralProperty == null)
            {
                throw Error.ArgumentNull("structuralProperty");
            }

            DeserializationHelpers.ApplyProperty(structuralProperty, structuredType, resource, DeserializerProvider, readContext);
        }
        
    }
}
