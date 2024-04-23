using SampleProject.Infrastructure.Implementations;
using SampleProject.Infrastructure.Repositories;

namespace SampleProject.Infrastructure;

public class UnitOfWork(BaseDBContext dbContext) : BaseUnitOfWork(dbContext)
{
    public SampleModelRepository SampleModelRepository { get; init; } = new(dbContext);
}
