using BuildingBlocks.Domain.Interfaces;

namespace BuildingBlocks.Infrastructure.Implementations;

public class UnitOfWork(DBContext dbContext) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
