using LibraryOfBooks.Web.DTOs.Responses;
using LibraryOfBooks.Web.DTOs.Users;
using LibraryOfBooks.Web.Interfaces;
using System.Net.Http.Json;

namespace LibraryOfBooks.Web.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public AuthService(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        this.configuration = configuration;
    }

    public async Task<Response<UserResponseDto>> GenerateTokenAsync(string userName, string password)
    {
        var response = await httpClient.PostAsJsonAsync("api/auth/login", new { userName = userName, password = password });

        var userResponse = await response.Content.ReadFromJsonAsync<Response<UserResponseDto>>();

        return userResponse;
    }
}