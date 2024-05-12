using SampleProject.Domain.BaseInterfaces;

namespace SampleProject.Infrastructure.Implementations;

public class BaseUnitOfWork(BaseDBContext dbContext) : IBaseUnitOfWork
{
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
