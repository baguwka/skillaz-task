using System;
using System.Threading.Tasks;
using LinkShortener.Api.Repo.DomainModel;
using LinkShortener.Api.Repo.MongoDb;
using MongoDB.Driver;

namespace LinkShortener.Api.Repo
{
    public class MongoLinksRepository : ILinksRepository
    {
        private const string _CollectionKey = "links";
        private readonly IMongoDbProvider _DbProvider;

        public MongoLinksRepository(IMongoDbProvider dbProvider)
        {
            _DbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
        }

        public async Task AddLinkAsync(LinkModel link)
        {
            var collection = _DbProvider.Db.GetCollection<LinkModel>(_CollectionKey);
            await collection.InsertOneAsync(link);
        }

        public async Task<LinkModel> GetLinkByShortenIdAndIncCounterAsync(string shortenId)
        {
            var collection = _DbProvider.Db.GetCollection<LinkModel>(_CollectionKey);
            var filter = Builders<LinkModel>.Filter.Eq(nameof(LinkModel.ShortenId), shortenId);
            var update = Builders<LinkModel>.Update.Inc(model => model.ViewCount, 1);
            var foundLink = await collection.FindOneAndUpdateAsync(filter, update);
            return foundLink;
        }

        public async Task<LinksCollectionModel> GetAllLinksForIdentityAsync(string identity)
        {
            var collection = _DbProvider.Db.GetCollection<LinkModel>(_CollectionKey);
            var filter = Builders<LinkModel>.Filter.Eq(nameof(LinkModel.Identity), identity);
            var found = await collection.FindAsync(filter);
            var list = await found.ToListAsync();
            return LinksCollectionModel.FromCollection(list);
        }
    }
}