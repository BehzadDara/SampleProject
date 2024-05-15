using BuildingBlocks.Infrastructure.Implementations;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Infrastructure;

public class SampleProjectUnitOfWork(
    DBContext dbContext,
    ISampleModelRepository sampleModelRepository,
    IAnotherSampleModelRepository anotherSampleModelRepository
    ) : UnitOfWork(dbContext), ISampleProjectUnitOfWork
{
    public ISampleModelRepository SampleModelRepository { get; init; } = sampleModelRepository;
    public IAnotherSampleModelRepository AnotherSampleModelRepository { get; init; } = anotherSampleModelRepository;
}
