using SampleProject.Domain.BaseModels;
using SampleProject.Domain.BaseSpecificationConfig;
using System.Data;

namespace SampleProject.Domain.BaseInterfaces;

public interface IBaseRepository<TEntity> : IBaseReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
