using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleProject.Infrastructure.Authorization.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetHeaderValue(this IHttpContextAccessor httpContextAccessor, string key)
        {
            return httpContextAccessor.HttpContext.Request.Headers[key].FirstOrDefault();
        }
    }
}
