using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.SpecificationConfig;

namespace BuildingBlocks.Infrastructure.Implementations;

public class ReadOnlyRepository<TEntity>(
    DBContext dbContext
    ) : RepositoryProperties<TEntity>(dbContext), IReadOnlyRepository<TEntity> where TEntity : Entity
{
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.Specify(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<(int TotalCount, IList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        var query = SetAsNoTracking.Specify(specification);

        var totalCount = await query.CountAsync(cancellationToken);
        var data = await query.Skip(specification.Skip).Take(specification.Take).ToListAsync(cancellationToken);

        return (totalCount, data);
    }
}
