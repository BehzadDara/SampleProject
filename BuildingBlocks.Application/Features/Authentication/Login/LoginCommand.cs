namespace BuildingBlocks.Application.Features.Authentication.Login;

public record LoginCommand(
    string UserName,
    string Password
    ) : ICommandQuery<string>;
