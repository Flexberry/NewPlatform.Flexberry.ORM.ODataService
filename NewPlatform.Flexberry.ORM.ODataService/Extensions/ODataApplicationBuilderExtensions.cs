#if NETSTANDARD
namespace NewPlatform.Flexberry.ORM.ODataService.Extensions
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Common;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using NewPlatform.Flexberry.ORM.ODataService.Middleware;

    /// <summary>
    /// Provides extension methods for <see cref="IApplicationBuilder"/> to add ODataService routes.
    /// </summary>
    public static class ODataApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds ODataService to the <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder "/> to use.</param>
        /// <param name="configureRoutes">A callback to configure MVC routes.</param>
        /// <param name="maxTopValue">Sets the max value of $top that a client can request in route builder.</param>
        /// <returns>The <see cref="IApplicationBuilder "/>.</returns>
        public static IApplicationBuilder UseODataService(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes, int? maxTopValue = int.MaxValue)
        {
            if (app == null)
            {
                throw Error.ArgumentNull(nameof(app));
            }

            VerifyODataServiceIsRegistered(app);

            app.Use(async (context, next) =>
            {
                await RewriteResponse(context, next);
            });

            return app
                .UseODataBatching()
                .UseMiddleware<RequestHeadersHookMiddleware>()
                .UseMvc(builder =>
                {
                    builder.Select().Expand().Filter().OrderBy().MaxTop(maxTopValue).Count();
                })
                .UseMvc(configureRoutes);
        }

        private static void VerifyODataServiceIsRegistered(IApplicationBuilder app)
        {
            // We use the IPerRouteContainer to verify if AddOData() was called before calling UseODataService
            if (app.ApplicationServices.GetService(typeof(IPerRouteContainer)) == null)
            {
                throw Error.InvalidOperation(SRResources.MissingODataServices, nameof(IPerRouteContainer));
            }
        }

        /// <summary>
        /// Removing of extra symbols from response.
        /// </summary>
        /// <param name="context">Context of request.</param>
        /// <param name="next">Next middleware.</param>
        /// <returns>Formed task.</returns>
        public static async Task RewriteResponse(HttpContext context, Func<Task> next)
        {
            /* Same code for NETFRAMEWORK is placed on NewPlatform.Flexberry.ORM.ODataService.Handlers.PostPatchHandler.*/
            using (var responseBodyStream = new MemoryStream())
            {
                Stream bodyStream = context.Response.Body;

                try
                {
                    context.Response.Body = responseBodyStream;

                    await next();

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    using StreamReader sr = new StreamReader(responseBodyStream);
                    var responseBody = sr.ReadToEnd();

                    //Modify the response in some way.
                    if (context.Response.ContentType != null &&
                        (context.Response.ContentType.Contains("application/json")
                        || context.Response.ContentType.Contains("application/xml")
                        || context.Response.ContentType.Contains("multipart/mixed")))
                    {
                        responseBody = responseBody
                            .Replace("(____.", "(")
                            .Replace("\"____.", "\"")
                            .Replace("____.", ".")
                            .Replace(" Namespace=\"____\"", " Namespace=\"\"");
                    }

                    using (MemoryStream newStream = new MemoryStream())
                    {
                        using StreamWriter sw = new StreamWriter(newStream);
                        sw.Write(responseBody);
                        sw.Flush();

                        newStream.Seek(0, SeekOrigin.Begin);

                        await newStream.CopyToAsync(bodyStream);
                    }
                }
                finally
                {
                    context.Response.Body = bodyStream;
                }
            }
        }
    }
}
#endif
