using SampleProject.Domain.BaseModels;

namespace SampleProject.Domain.BaseInterfaces;

public interface IBaseRepository<TEntity> : IBaseReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
