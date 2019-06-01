using System;
using Microsoft.AspNetCore.Http;

namespace LinkShortener.Api.Identity
{
    public class CookiesIdentifier : IHttpContextIdentifier
    {
        private const string _Key = "identity";

        public string GetOrCreateIdentity(HttpContext context)
        {
            if (!context.Request.Cookies.TryGetValue(_Key, out var identity))
            {
                identity = Guid.NewGuid().ToString();
                context.Response.Cookies.Append(_Key, identity);
            }

            return identity;
        }
    }
}