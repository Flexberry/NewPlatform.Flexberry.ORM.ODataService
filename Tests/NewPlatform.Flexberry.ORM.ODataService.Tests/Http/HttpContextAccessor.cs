#if NETFRAMEWORK
// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Http
{
    using System.Web;

    /// <summary>
    /// Provides an implementation of <see cref="IHttpContextAccessor" /> based on the current execution context.
    /// </summary>
    public class HttpContextAccessor : IHttpContextAccessor
    {
        /// <inheritdoc/>
        public HttpContext HttpContext
        {
            get => HttpContext.Current;

            set => HttpContext.Current = value;
        }
    }
}
#endif
