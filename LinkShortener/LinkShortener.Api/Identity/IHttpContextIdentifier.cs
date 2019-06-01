using Microsoft.AspNetCore.Http;

namespace LinkShortener.Api.Identity
{
    public interface IHttpContextIdentifier
    {
        string GetOrCreateIdentity(HttpContext context);
    }
}