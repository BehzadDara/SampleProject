using SampleProject.Domain.BaseModels;

namespace SampleProject.Domain.BaseInterfaces;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    /*Task<TEntity> GetAsync(Specification<T> spec, CancellationToken cancellationToken = default);
    Task<IList<TEntity>> ListAsync(Specification<T> spec, CancellationToken cancellationToken = default);*/
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
