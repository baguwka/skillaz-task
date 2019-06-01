using System.Threading.Tasks;

namespace Shortener.Lib.Ids
{
    public interface ILinksIdGenerator
    {
        Task<long> GetNextIdAsync(string url);
    }
}