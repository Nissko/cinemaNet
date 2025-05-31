using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.JSInterop;

namespace Cinema.Client.Services.JwtService;

public class JwtService
{
    private readonly IJSRuntime _jsRuntime;

    public JwtService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<UserInfo> GetUserInfoFromToken()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (string.IsNullOrEmpty(token))
            return new UserInfo();

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken == null)
            return new UserInfo();

        var email = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ??
                    jsonToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

        var role = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ??
                   jsonToken.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

        return new UserInfo
        {
            Email = email,
            Role = role
        };
    }
}