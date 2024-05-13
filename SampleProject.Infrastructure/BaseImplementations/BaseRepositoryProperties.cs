using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseModels;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure.BaseImplementations;

public class BaseRepositoryProperties<TEntity>(
    BaseDBContext dbContext
    ) where TEntity : Entity
{
    protected readonly BaseDBContext _dbContext = dbContext;

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
