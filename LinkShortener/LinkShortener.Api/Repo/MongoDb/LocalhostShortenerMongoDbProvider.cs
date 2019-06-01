using MongoDB.Driver;

namespace LinkShortener.Api.Repo.MongoDb
{
    public class LocalhostShortenerMongoDbProvider : IMongoDbProvider
    {
        private const string _DbKey = "Shortener";
        public IMongoDatabase Db { get; }

        public LocalhostShortenerMongoDbProvider()
        {
            var client = new MongoClient();
            Db = client.GetDatabase(_DbKey);
        }
    }
}