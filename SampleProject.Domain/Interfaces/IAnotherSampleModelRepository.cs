using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interfaces;

public interface IAnotherSampleModelRepository : IBaseReadOnlyRepository<AnotherSampleModel>
{
    public Task<int> GetTotalCount(CancellationToken cancellationToken = default);
}
