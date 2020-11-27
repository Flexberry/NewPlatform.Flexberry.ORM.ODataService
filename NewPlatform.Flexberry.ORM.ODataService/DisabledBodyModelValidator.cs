#if NETFRAMEWORK
namespace NewPlatform.Flexberry.ORM.ODataService
{
    using System;
    using System.Web.Http.Validation;

    /// <summary>
    /// Валидатор с отключенной проверкой типов.
    /// </summary>
    public class DisabledBodyModelValidator : DefaultBodyModelValidator
    {
        /// <inheritdoc/>
        public override bool ShouldValidateType(Type type)
        {
            return false;
        }
    }
}
#endif
