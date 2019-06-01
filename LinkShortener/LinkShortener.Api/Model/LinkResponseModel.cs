using System;
using LinkShortener.Api.Repo.DomainModel;
using Newtonsoft.Json;

namespace LinkShortener.Api.Model
{
    public class LinkResponseModel
    {
        [JsonProperty("originalUrl")]
        public string OriginalUrl { get; set; }

        [JsonProperty("shortenUrl")]
        public string ShortenUrl { get; set; }

        [JsonProperty("viewCount")]
        public long ViewCount { get; set; }

        public static LinkResponseModel FromDomainModel(string serverUrl, LinkModel domainModel)
        {
            var url = new UriBuilder(serverUrl)
            {
                Path = $"/l/{domainModel.ShortenId}"
            }.Uri.AbsoluteUri;

            return new LinkResponseModel
            {
                OriginalUrl = domainModel.OriginalUrl,
                ShortenUrl = url,
                ViewCount = domainModel.ViewCount
            };
        }
    }
}