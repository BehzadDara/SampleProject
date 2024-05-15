using BuildingBlocks.Domain.Interfaces;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interfaces;

public interface ISampleModelRepository : IRepository<SampleModel>
{
    public Task<int> GetTotalCount(CancellationToken cancellationToken = default);
}
