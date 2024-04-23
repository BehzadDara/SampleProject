namespace SampleProject.Domain.BaseInterfaces;

public interface IBaseUnitOfWork
{
    Task<bool> CompleteAsync(CancellationToken cancellationToken = default);
}
