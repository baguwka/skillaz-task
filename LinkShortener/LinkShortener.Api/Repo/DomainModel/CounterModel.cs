using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LinkShortener.Api.Repo.DomainModel
{
    public class CounterModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Key { get; set; }
        public long Count { get; set; }
    }
}