using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Data.Abstractions;

public partial class BaseRepository<TEntity>
{
    public IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression = null,
                                      Int32? skip = null,
                                      Int32? take = null)
    {
        _logger.LogDebug("Selecting entities from the data source");

        try
        {
            var entities = _entities.AsQueryable();

            if (expression != null)
            {
                entities = entities.Where(expression);
            }

            return entities.Skip<TEntity>(skip ?? 0)
                           .Take<TEntity>(take ?? 100);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting entities from the data source", ex);
        }
    }
    public async Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, Boolean>> expression = null,
                                                       Int32? skip = null,
                                                       Int32? take = null,
                                                       CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => Select(expression, skip, take), cancellationToken);
    }
}