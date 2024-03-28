namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Http
{
    using System;
#if NETFRAMEWORK
    using System.Collections.Specialized;
    using System.Web;
#endif

    using Microsoft.AspNetCore.Http;
#if NETCOREAPP
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
#endif
    using NewPlatform.Flexberry.ORM.CurrentUserService;

    /// <summary>
    /// Класс, представляющий текущего аутентифицированного пользователя на основе заголовков.
    /// </summary>
    public class WebHttpUser : ICurrentUser
    {
        private readonly IHttpContextAccessor contextAccessor;

#if NETCOREAPP
        private readonly IActionContextAccessor actionContextAccessor;
#endif

        private string login;

        private string friendlyName;

#if NETFRAMEWORK
        /// <summary>
        /// Initializes a new instance of the <see cref="WebHttpUser" /> class.
        /// </summary>
        /// <param name="contextAccessor"><see cref="HttpContext" /> provider.</param>
        public WebHttpUser(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
#elif NETCOREAPP
        /// <summary>
        /// Initializes a new instance of the <see cref="WebHttpUser" /> class.
        /// </summary>
        /// <param name="contextAccessor"><see cref="HttpContext" /> provider.</param>
        /// <param name="actionContextAccessor"><see cref="ActionContext" /> provider.</param>
        public WebHttpUser(IHttpContextAccessor contextAccessor, IActionContextAccessor actionContextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.actionContextAccessor = actionContextAccessor;
        }
#endif

        /// <inheritdoc />
        public string Login
        {
            get
            {
                /* HttpContext is not thread-safety.
                 * According to https://www.thecodebuzz.com/httpcontext-best-practices-in-net-csharp-thread-safe/
                 * it is bad to keep HttpContext in situation of multy-threading at constructor. Only IHttpContextAccessor can be saved at constructor.
                 * Also login is not saved bacause of multy-threading. This instance is used for all threads and it leads to errors if some values are kept.
                 */
#if NETFRAMEWORK
                HttpContext context = contextAccessor.HttpContext;
                NameValueCollection headers = context?.Request.Headers;
#elif NETCOREAPP
                HttpContext context = contextAccessor.HttpContext ?? actionContextAccessor.ActionContext?.HttpContext;
                IHeaderDictionary headers = context?.Request.Headers;
#endif

                return headers?["username"];
            }

            set
            {
                login = value;
            }
        }

        /// <inheritdoc />
        public string Domain { get; set; }

        /// <inheritdoc />
        public string FriendlyName
        {
            get
            {
#if NETFRAMEWORK
                HttpContext context = contextAccessor.HttpContext;
                NameValueCollection headers = context?.Request.Headers;
#elif NETCOREAPP
                HttpContext context = contextAccessor.HttpContext ?? actionContextAccessor.ActionContext?.HttpContext;
                IHeaderDictionary headers = context?.Request.Headers;
#endif

                return headers?["name"];
            }

            set => friendlyName = value;
        }
    }
}
