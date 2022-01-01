using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SampleProject.Infrastructure.Authorization.Extensions;
using SampleProject.Infrastructure.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Authorization.Handle
{
    public class BasicAuthorizationHandler : AuthorizationHandler<BasicAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasicAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BasicAuthorizationRequirement requirement)
        {
            if (_httpContextAccessor == null) {
                context.Fail();
                return;
            }

            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var userIdentity = _httpContextAccessor.HttpContext.User.Identity;

            context.Succeed(requirement);

            await Task.CompletedTask;
        }
    }
}
