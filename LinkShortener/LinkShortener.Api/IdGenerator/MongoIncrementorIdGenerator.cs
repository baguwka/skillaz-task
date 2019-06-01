using System;
using System.Threading.Tasks;
using LinkShortener.Api.Repo;
using Shortener.Lib.Ids;

namespace LinkShortener.Api.IdGenerator
{
    public class MongoIncrementorIdGenerator : ILinksIdGenerator
    {
        private readonly ICounterRepository _CounterRepo;

        public MongoIncrementorIdGenerator(ICounterRepository counterRepo)
        {
            _CounterRepo = counterRepo ?? throw new ArgumentNullException(nameof(counterRepo));
        }

        public Task<long> GetNextIdAsync(string url)
        {
            return _CounterRepo.IncrementLinksCounterAsync();
        }
    }
}