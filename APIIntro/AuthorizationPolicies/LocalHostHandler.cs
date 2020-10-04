using APIIntro.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace APIIntro.AuthorizationPolicies
{
    public class LocalHostHandler : AuthorizationHandler<LocalHostRequirement>
    {
        private IHttpContextAccessor _contextAccessor;

        public LocalHostHandler(IHttpContextAccessor contextAccessor) { _contextAccessor = contextAccessor; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       LocalHostRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext.Request.IsLocal())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public class LocalHostRequirement : IAuthorizationRequirement
    {
    }
}
