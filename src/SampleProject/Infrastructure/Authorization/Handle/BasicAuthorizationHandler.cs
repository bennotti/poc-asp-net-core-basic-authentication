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
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BasicAuthorizationRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var userIdentity = context.User.Identity;

            context.Succeed(requirement);

            await Task.CompletedTask;
        }
    }
}
