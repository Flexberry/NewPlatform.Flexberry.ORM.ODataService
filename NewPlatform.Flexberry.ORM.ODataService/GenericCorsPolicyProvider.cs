namespace NewPlatform.Flexberry.ORM.ODataService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    //using System.Net.Cors;
    //using System.Web.Http.Cors;
    using Microsoft.AspNetCore.Cors.Infrastructure;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Implementation of <see cref="ICorsPolicyProvider"/> that adds value from "Origin" request header to the
    /// "Access-Control-Allow-Origin" response header.
    /// </summary>
    /// <seealso cref="ICorsPolicyProvider" />
    public sealed class GenericCorsPolicyProvider : ICorsPolicyProvider
    {
        /// <summary>
        /// Gets the <see cref="T:System.Web.Cors.CorsPolicy" />.
        /// Adds value from "Origin" request header to the "Access-Control-Allow-Origin" response header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="T:System.Web.Cors.CorsPolicy" />.</returns>
        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var result = new CorsPolicyBuilder().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().Build();
            // new CorsPolicy { AllowAnyHeader = true, AllowAnyMethod = true, AllowAnyOrigin = false };

            IEnumerable<string> origin;
            if (request.Headers.TryGetValues("Origin", out origin))
            {
                var firstOrigin = origin.FirstOrDefault();
                if (!string.IsNullOrEmpty(firstOrigin))
                    result.Origins.Add(firstOrigin);
            }

            return Task.FromResult(result);
        }

        public Task<CorsPolicy> GetPolicyAsync(HttpContext context, string policyName)
        {
            throw new System.NotImplementedException();
        }
    }
}