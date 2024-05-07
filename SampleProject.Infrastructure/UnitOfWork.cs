using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure;

public class UnitOfWork(
    BaseDBContext dbContext,
    ISampleModelRepository sampleModelRepository,
    IAnotherSampleModelRepository anotherSampleModelRepository
    ) : BaseUnitOfWork(dbContext), IUnitOfWork
{
    public ISampleModelRepository SampleModelRepository { get; init; } = sampleModelRepository;
    public IAnotherSampleModelRepository AnotherSampleModelRepository { get; init; } = anotherSampleModelRepository;
}
