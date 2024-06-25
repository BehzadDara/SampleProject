using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Domain.Models;

namespace BuildingBlocks.Infrastructure.Implementations;

public class RepositoryProperties<TEntity>(
    DBContext dbContext
    ) where TEntity : Entity
{
    protected readonly DBContext _dbContext = dbContext;

    protected DbSet<TEntity> Set => _dbContext.Set<TEntity>();

    protected IQueryable<TEntity> SetAsNoTracking
    {
        get
        {
            var query = Set.AsNoTracking();

            if (typeof(TEntity).IsSubclassOf(typeof(TrackableEntity)))
            {
                query = query.Where(e => !(e as TrackableEntity)!.IsDeleted);
            }

            return query;
        }
    }
}
