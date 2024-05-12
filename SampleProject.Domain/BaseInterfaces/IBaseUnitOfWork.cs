namespace SampleProject.Domain.BaseInterfaces;

public interface IBaseUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}
