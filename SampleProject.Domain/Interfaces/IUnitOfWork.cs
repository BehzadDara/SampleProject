using SampleProject.Domain.BaseInterfaces;

namespace SampleProject.Domain.Interfaces;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ISampleModelRepository SampleModelRepository { get; init; }
}
