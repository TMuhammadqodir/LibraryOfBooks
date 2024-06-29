using Blazored.LocalStorage;
using LibraryOfBooks.Web.DTOs.Responses;
using LibraryOfBooks.Web.DTOs.Users;
using LibraryOfBooks.Web.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(HttpClient httpClient, IConfiguration configuration, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<Response<UserResponseDto>> GenerateTokenAsync(string userName, string password)
    {
        var loginRequest = new LoginRequest { UserName = userName, Password = password };

        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);

        var userResponse = await response.Content.ReadFromJsonAsync<Response<UserResponseDto>>();

        if (userResponse != null && userResponse.StatusCode == 200)
        {
            var token = userResponse.Data.Token;
            await _localStorage.SetItemAsync("authToken", token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (_authenticationStateProvider is CustomAuthenticationStateProvider customAuthProvider)
            {
                await customAuthProvider.MarkUserAsAuthenticated(token);
            }
        }

        return userResponse;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        
        if (_authenticationStateProvider is CustomAuthenticationStateProvider customAuthProvider)
        {
            await customAuthProvider.MarkUserAsLoggedOut();
        }
    }
}
