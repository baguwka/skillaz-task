using System.Collections.Generic;
using System.Linq;
using LinkShortener.Api.Repo.DomainModel;
using Newtonsoft.Json;

namespace LinkShortener.Api.Model
{
    public class GetLinksResponseModel
    {
        [JsonProperty("links")]
        public List<LinkResponseModel> Links { get; set; }

        public static GetLinksResponseModel FromDomainModel(string serverUrl, LinksCollectionModel model)
        {
            return new GetLinksResponseModel
            {
                Links = model.Links
                    .Select(linkModel => LinkResponseModel.FromDomainModel(serverUrl, linkModel))
                    .ToList()
            };
        }
    }
}