using System.Collections.Generic;
using System.Linq;

namespace LinkShortener.Api.Repo.DomainModel
{
    public class LinksCollectionModel
    {
        public IReadOnlyCollection<LinkModel> Links { get; set; }

        public static LinksCollectionModel FromCollection(ICollection<LinkModel> links) => new LinksCollectionModel
        {
            Links = links.ToList()
        };
    }
}