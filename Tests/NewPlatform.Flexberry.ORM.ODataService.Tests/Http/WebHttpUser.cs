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

    using static ICSSoft.Services.CurrentUserService;

    /// <summary>
    /// Класс, представляющий текущего аутентифицированного пользователя на основе заголовков.
    /// </summary>
    public class WebHttpUser : IUser
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
                string contextLogin = Headers?["username"];

                if (!string.IsNullOrEmpty(contextLogin) && !string.Equals(contextLogin, login, StringComparison.InvariantCultureIgnoreCase))
                {
                    login = contextLogin;
                }

                return login;
            }

            set => login = value;
        }

        /// <inheritdoc />
        public string Domain { get; set; }

        /// <inheritdoc />
        public string FriendlyName
        {
            get
            {
                string contextFriendlyName = Headers?["name"];

                if (!string.IsNullOrEmpty(contextFriendlyName) && !string.Equals(contextFriendlyName, friendlyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    friendlyName = contextFriendlyName;
                }

                return friendlyName;
            }

            set => friendlyName = value;
        }

#if NETFRAMEWORK
        private HttpContext Context => contextAccessor.HttpContext;

        private NameValueCollection Headers => Context?.Request.Headers;
#elif NETCOREAPP
        private HttpContext Context => contextAccessor.HttpContext ?? actionContextAccessor.ActionContext?.HttpContext;

        private IHeaderDictionary Headers => Context?.Request.Headers;
#endif
    }
}
