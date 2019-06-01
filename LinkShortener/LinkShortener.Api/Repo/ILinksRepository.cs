using System.Threading.Tasks;
using LinkShortener.Api.Repo.DomainModel;

namespace LinkShortener.Api.Repo
{
    public interface ILinksRepository
    {
        Task AddLinkAsync(LinkModel link);
        Task<LinkModel> GetLinkByShortenIdAndIncCounterAsync(string shortenId);
        Task<LinksCollectionModel> GetAllLinksForIdentityAsync(string identity);
    }
}