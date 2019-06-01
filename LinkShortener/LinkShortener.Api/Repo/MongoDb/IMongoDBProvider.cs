using MongoDB.Driver;

namespace LinkShortener.Api.Repo.MongoDb
{
    public interface IMongoDbProvider
    {
        IMongoDatabase Db { get; }
    }
}