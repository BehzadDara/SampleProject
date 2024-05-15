using BuildingBlocks.Domain.Interfaces;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interfaces;

public interface IAnotherSampleModelRepository : IReadOnlyRepository<AnotherSampleModel>
{
    public Task<int> GetTotalCount(CancellationToken cancellationToken = default);
}
