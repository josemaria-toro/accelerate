using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Data.Abstractions;

public partial class BaseRepository<TEntity>
{
    private void Commit()
    {
        _logger.LogDebug("Committing pending changes");

        try
        {
            SaveChanges(true);
        }
        catch (Exception ex)
        {
            throw new DataException("An error is encountered while committing pending changes", ex);
        }
    }
    private void Rollback()
    {
        _logger.LogDebug("Undoing pending changes");

        try
        {
            var entityEntries = ChangeTracker.Entries()
                                             .Where(x => x.State != EntityState.Unchanged);

            foreach (var entityEntry in entityEntries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entityEntry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        entityEntry.State = EntityState.Unchanged;
                        break;
                }
            }

            SaveChanges(true);
        }
        catch (DbUpdateException ex)
        {
            throw new DataException("An error is encountered while undoing pending changes", ex);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while undoing pending changes", ex);
        }
    }
    protected void SavePendingChanges()
    {
        _semaphore.Wait();

        try
        {
            Commit();
        }
        catch (DataException cex)
        {
            try
            {
                Rollback();
            }
            catch (DataException rex)
            {
                throw new AggregateException(cex, rex);
            }

            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}