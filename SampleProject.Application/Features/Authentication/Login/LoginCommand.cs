using BuildingBlocks.Application.Features;

namespace SampleProject.Application.Features.Authentication.Login;

public record LoginCommand(
    string UserName,
    string Password
    ) : ICommandQuery<string>;
