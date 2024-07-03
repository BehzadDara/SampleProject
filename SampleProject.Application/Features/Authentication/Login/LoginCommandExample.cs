using Swashbuckle.AspNetCore.Filters;

namespace SampleProject.Application.Features.Authentication.Login;

public class LoginCommandExample : IExamplesProvider<LoginCommand>
{
    public LoginCommand GetExamples()
    {
        return new LoginCommand("MyUsername", "MyPassword");
    }
}
