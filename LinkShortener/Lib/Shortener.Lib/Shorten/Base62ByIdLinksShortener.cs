using System;
using System.Threading.Tasks;
using Shortener.Lib.Exceptions;
using Shortener.Lib.Ids;

namespace Shortener.Lib.Shorten
{
    public class Base62ByIdLinksShortener : ILinksShortener
    {
        private readonly ILinksIdGenerator _LinksIdGenerator;
        private readonly IUrlValidator _UrlValidator;

        public Base62ByIdLinksShortener(ILinksIdGenerator linksIdGenerator, IUrlValidator urlValidator)
        {
            _LinksIdGenerator = linksIdGenerator ?? throw new ArgumentNullException(nameof(linksIdGenerator));
            _UrlValidator = urlValidator;
        }

        /// <inheritdoc />
        public async Task<string> ShortenAsync(string originalLink)
        {
            if (string.IsNullOrWhiteSpace(originalLink))
                throw new UrlIsMissingException($"Url is required");

            if (!_UrlValidator.IsValid(originalLink))
                throw new UrlIsInvalidException($"Url \'{originalLink}\' have incorrect format");

            var id = await _LinksIdGenerator.GetNextIdAsync(originalLink);
            return ToBase62(id);
        }

        private string ToBase62(long input)
        {
            var s = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var hash = "";
            while (input > 0)
            {
                hash = s[(int) (input % 62)] + hash;
                input /= 62;
            }

            return hash;
        }
    }
}
