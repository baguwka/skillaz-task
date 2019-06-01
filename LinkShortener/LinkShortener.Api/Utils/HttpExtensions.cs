using Microsoft.AspNetCore.Http;

namespace LinkShortener.Api.Utils
{
    public static class HttpExtensions
    {
        public static string GetBaseUrl(this HttpRequest requestContext) =>
            $"{requestContext.Scheme}://{requestContext.Host}{requestContext.PathBase}";
    }
}