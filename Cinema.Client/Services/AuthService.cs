using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace Cinema.Client.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _jsRuntime;
    
    public AuthService(HttpClient http, IJSRuntime jsRuntime)
    {
        _http = http ?? throw new ArgumentNullException(nameof(http));
        _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
    }

    public async Task Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { Email = email, Password = password });
        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

        if (result != null)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
        }
    }

    public async Task Register(string email, string password)
    {
        await _http.PostAsJsonAsync("auth/register", new { Email = email, Password = password });
    }

    public async Task Logout()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        _http.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> IsUserAuthenticated()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        return !string.IsNullOrEmpty(token);
    }

    public async Task SetAuthorizationHeader()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

public class AuthResponse
{
    public string Token { get; set; }
    public string Email { get; set; }
}