using System.Threading.Tasks;

namespace Shortener.Lib
{
    public interface IShortener
    {
        Task<string> ShortenAsync(string originalLink);
    }
}