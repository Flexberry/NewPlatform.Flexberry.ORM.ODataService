namespace NewPlatform.Flexberry.ORM.ODataService
{
    using System;

#if NETFRAMEWORK
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
#endif
#if NETSTANDARD
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// Валидатор с отключенной проверкой типов.
    /// </summary>
    public class DisabledBodyModelValidator : IObjectModelValidator
    {
        /// <inheritdoc/>
        public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model)
        {
        }
    }
#endif
}
