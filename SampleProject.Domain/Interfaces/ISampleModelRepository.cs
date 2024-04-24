using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseSpecificationConfig;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interfaces;

public interface ISampleModelRepository : IBaseRepository<SampleModel>
{
    public Task<Tuple<int, IList<SampleModel>>> GetByFilter(BaseSpecification<SampleModel> specification, CancellationToken cancellationToken = default);
}
