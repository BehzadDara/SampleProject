using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseModels;
using SampleProject.Domain.BaseSpecificationConfig;
using System.Data;

namespace SampleProject.Infrastructure.Implementations;

public class BaseReadOnlyRepository<TEntity>(
    BaseDBContext dbContext
    ) : IBaseReadOnlyRepository<TEntity> where TEntity : Entity
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

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.Specify(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<(int TotalCount, IList<TEntity> Data)> ListAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        var query = SetAsNoTracking.Specify(specification);

        var totalCount = await query.CountAsync(cancellationToken);
        var data = await query.Skip(specification.Skip).Take(specification.Take).ToListAsync(cancellationToken);

        return (totalCount, data);
    }
}
