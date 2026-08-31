using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zetatech.Accelerate.Cache;
using Zetatech.Accelerate.Jobs.Abstractions;

namespace Zetatech.Accelerate.Jobs;

public sealed class InMemoryCacheCleanerJob : BasePeriodicJob
{
    private InMemoryCache _inMemoryCache;

    public InMemoryCacheCleanerJob(ICache inMemoryCache) : base(TimeSpan.FromSeconds(15), false)
    {
        if (inMemoryCache == null)
        {
            throw new ArgumentException("The provided cache service must be a valid instance", nameof(inMemoryCache));
        }

        if (inMemoryCache is InMemoryCache)
        {
            _inMemoryCache = inMemoryCache as InMemoryCache;
        }
        else
        {
            throw new NotSupportedException("The provided cache service is not supported");
        }
    }

    protected override async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        var expiredObjects = _inMemoryCache.Dictionary.Values.Where(x => x.IsExpired);

        foreach (var expiredObject in expiredObjects)
        {
            _ = _inMemoryCache.Dictionary.TryRemove(expiredObject.Key, out var _);
        }
    }
}
