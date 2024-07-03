using Swashbuckle.AspNetCore.Filters;

namespace SampleProject.Application.Features.Authentication.Login;

public class TokenExample : IExamplesProvider<string>
{
    public string GetExamples()
    {
        return "SampleToken";
    }
}
