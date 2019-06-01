using System.Threading.Tasks;

namespace Shortener.Lib
{
    public interface ILinksIdGenerator
    {
        Task<long> GetNextIdAsync(string url);
    }
}