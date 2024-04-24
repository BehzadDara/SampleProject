using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure.Implementations;
using SampleProject.Infrastructure.Repositories;

namespace SampleProject.Infrastructure;

public class UnitOfWork(BaseDBContext dbContext, ISampleModelRepository sampleModelRepository) : BaseUnitOfWork(dbContext), IUnitOfWork
{
    public ISampleModelRepository SampleModelRepository { get; init; } = sampleModelRepository;
}
