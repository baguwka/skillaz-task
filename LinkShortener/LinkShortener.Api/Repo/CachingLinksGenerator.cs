using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Shortener.Lib;
using Shortener.Lib.Ids;

namespace LinkShortener.Api.Repo
{
    public class CachingLinksGenerator : ILinksIdGenerator
    {
        private readonly IMemoryCache _Cache;
        private readonly ILinksIdGenerator _RealGenerator;

        public CachingLinksGenerator(IMemoryCache cache, ILinksIdGenerator realGenerator)
        {
            _Cache = cache;
            _RealGenerator = realGenerator ?? throw new ArgumentNullException(nameof(realGenerator));
        }

        public Task<long> GetNextIdAsync(string url)
        {
            return _Cache.GetOrCreateAsync(url, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return _RealGenerator.GetNextIdAsync(url);
            });
        }
    }
}