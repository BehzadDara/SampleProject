using SampleProject.Domain.BaseInterfaces;

namespace SampleProject.Domain.Interfaces;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ISampleModelRepository SampleModelRepository { get; init; }
    public IAnotherSampleModelRepository AnotherSampleModelRepository { get; init; }
}
