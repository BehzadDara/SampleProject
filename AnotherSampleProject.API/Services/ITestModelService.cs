using AnotherSampleProject.API.Model;

namespace AnotherSampleProject.API.Services;

public interface ITestModelService
{
    Task<List<TestModel>> GetAll(CancellationToken cancellationToken);
    Task Add(string name, CancellationToken cancellationToken);
}
