namespace SampleProject.Application.BaseFeatures.Authentication.Login;

public record LoginCommand(
    string UserName,
    string Password
    ) : IBaseCommandQuery<string>;
