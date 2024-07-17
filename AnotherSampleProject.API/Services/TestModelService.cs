using AnotherSampleProject.API.Model;
using Microsoft.EntityFrameworkCore;

namespace AnotherSampleProject.API.Services;

public class TestModelService(AnotherSampleProjectDBContext context) : ITestModelService
{
    public async Task<List<TestModel>> GetAll(CancellationToken cancellationToken)
    {
        return await context.TestModels.ToListAsync(cancellationToken);
    }

    public async Task Add(string name, CancellationToken cancellationToken)
    {
        var test = TestModel.Create(name);
        context.TestModels.Add(test);
        await context.SaveChangesAsync(cancellationToken);
    }
}
