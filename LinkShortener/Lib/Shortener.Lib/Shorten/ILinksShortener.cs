using System.Threading.Tasks;
using Shortener.Lib.Exceptions;

namespace Shortener.Lib.Shorten
{
    public interface ILinksShortener
    {
        /// <exception cref="UrlIsInvalidException"></exception>
        /// <exception cref="UrlIsMissingException"></exception>
        Task<string> ShortenAsync(string originalLink);
    }
}