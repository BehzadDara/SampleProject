using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseSpecificationConfig;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interfaces;

public interface ISampleModelRepository : IBaseRepository<SampleModel>
{
    public Task<(int TotalCount, IList<SampleModel> Data)> GetByFilter(BaseSpecification<SampleModel> specification, CancellationToken cancellationToken = default);
}
