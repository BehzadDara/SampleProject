using SampleProject.Domain.BaseModels;
using SampleProject.Domain.BaseSpecificationConfig;

namespace SampleProject.Domain.BaseInterfaces;

public interface IBaseReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<(int TotalCount, IList<TEntity> Data)> ListAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default);
}

