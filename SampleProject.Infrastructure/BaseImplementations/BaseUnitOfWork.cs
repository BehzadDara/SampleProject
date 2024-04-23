using SampleProject.Domain.BaseInterfaces;
using SampleProject.Infrastructure.Repositories;

namespace SampleProject.Infrastructure.Implementations;

public class BaseUnitOfWork(BaseDBContext dbContext) : IBaseUnitOfWork
{
    public async Task<bool> CompleteAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
