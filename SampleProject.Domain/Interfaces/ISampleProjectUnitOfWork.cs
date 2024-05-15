using BuildingBlocks.Domain.Interfaces;

namespace SampleProject.Domain.Interfaces;

public interface ISampleProjectUnitOfWork : IUnitOfWork
{
    public ISampleModelRepository SampleModelRepository { get; init; }
    public IAnotherSampleModelRepository AnotherSampleModelRepository { get; init; }
}