using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Domain.BaseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleProject.Application.BaseFeatures.Authentication.Login;

public class LoginCommandHandler(IConfiguration config) : IBaseCommandQueryHandler<LoginCommand, string>
{
    public async Task<BaseResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseResult<string>();

        var isValidUser = false;
        await Task.Run(() =>
        {
            isValidUser = IsValidUser(request);
        }, cancellationToken);

        if (!isValidUser)
        {
            result.NotFound();
            return result;
        }

        var token = string.Empty;
        await Task.Run(() =>
        {
            token = GenerateToken(request.UserName);
        }, cancellationToken);

        result.AddValue(token);
        result.Success();
        return result;
    }

    private static bool IsValidUser(LoginCommand request)
    {
        if (request.UserName == request.Password)
        {
            return true;
        }
        return false;
    }

    private string GenerateToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "AlternativeKey"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,username)
        };

        var token = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
