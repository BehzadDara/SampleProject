using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interfaces;

public interface ISampleModelRepository : IBaseRepository<SampleModel>
{
    public Task<int> GetTotalCount(CancellationToken cancellationToken = default);
}
