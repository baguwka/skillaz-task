using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LinkShortener.Api.Repo.DomainModel
{
    public class LinkModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Identity { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenId { get; set; }
        public long ViewCount { get; set; }
    }
}