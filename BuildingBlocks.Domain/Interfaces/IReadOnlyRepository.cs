using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.SpecificationConfig;

namespace BuildingBlocks.Domain.Interfaces;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<(int TotalCount, IList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
}

