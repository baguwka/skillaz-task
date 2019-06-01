using System;
using System.Threading.Tasks;
using LinkShortener.Api.Repo.DomainModel;
using LinkShortener.Api.Repo.MongoDb;
using MongoDB.Driver;

namespace LinkShortener.Api.Repo
{
    public class MongoCounterRepository : ICounterRepository
    {
        private readonly IMongoDbProvider _MongoDbProvider;

        public MongoCounterRepository(IMongoDbProvider mongoDbProvider)
        {
            _MongoDbProvider = mongoDbProvider ?? throw new ArgumentNullException(nameof(mongoDbProvider));
        }

        public async Task<long> IncrementLinksCounterAsync()
        {
            var counters = _MongoDbProvider.Db.GetCollection<CounterModel>("counter");
            var query = Builders<CounterModel>.Filter.Eq("Key", "linksCount");
            var result = await counters.FindOneAndUpdateAsync(query, Builders<CounterModel>.Update.Inc("Count", 1), new FindOneAndUpdateOptions<CounterModel>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            });

            return result.Count;
        }
    }
}