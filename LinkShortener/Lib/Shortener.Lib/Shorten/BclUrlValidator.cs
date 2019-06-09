using System;

namespace Shortener.Lib.Shorten
{
    public class BclUrlValidator : IUrlValidator
    {
        public bool IsValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}