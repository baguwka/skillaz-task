using System.Threading.Tasks;

namespace LinkShortener.Api.Repo
{
    public interface ICounterRepository
    {
        Task<long> IncrementLinksCounterAsync();
    }
}