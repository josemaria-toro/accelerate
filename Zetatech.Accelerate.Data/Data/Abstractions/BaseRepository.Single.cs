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
    public TEntity Single(Expression<Func<TEntity, Boolean>> expression = null,
                          Int32? skip = null)
    {
        _logger.LogDebug("Selecting the first entity in the data source");

        try
        {
            var entities = _entities.AsQueryable();

            if (expression != null)
            {
                entities = entities.Where(expression);
            }

            return entities.Skip<TEntity>(skip ?? 0)
                           .FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data source", ex);
        }
    }
    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, Boolean>> expression = null,
                                           Int32? skip = null,
                                           CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => Single(expression, skip), cancellationToken);
    }
}