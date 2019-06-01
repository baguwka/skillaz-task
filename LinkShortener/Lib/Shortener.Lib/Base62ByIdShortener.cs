using System;
using System.IO;
using System.Threading.Tasks;

namespace Shortener.Lib
{
    public class Base62ByIdShortener : IShortener
    {
        private readonly ILinksIdGenerator _LinksIdGenerator;

        public Base62ByIdShortener(ILinksIdGenerator linksIdGenerator)
        {
            _LinksIdGenerator = linksIdGenerator ?? throw new ArgumentNullException(nameof(linksIdGenerator));
        }

        public async Task<string> ShortenAsync(string originalLink)
        {
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
