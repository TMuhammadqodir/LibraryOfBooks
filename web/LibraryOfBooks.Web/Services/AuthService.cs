using LibraryOfBooks.Web.DTOs.Responses;
using LibraryOfBooks.Web.DTOs.Users;
using LibraryOfBooks.Web.Interfaces;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace LibraryOfBooks.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthService(HttpClient httpClient, IConfiguration configuration, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response<UserResponseDto>> GenerateTokenAsync(string userName, string password)
        {
            var loginRequest = new LoginRequest { UserName = userName, Password = password };

            var response = await httpClient.PostAsJsonAsync("api/auth/login", loginRequest);

            var userResponse = await response.Content.ReadFromJsonAsync<Response<UserResponseDto>>();

            if (userResponse != null && userResponse.StatusCode == 200)
            {
                var token = userResponse.Data.Token; // Assuming the token is inside the Data property
                await localStorage.SetItemAsync("authToken", token);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                ((CustomAuthenticationStateProvider)authenticationStateProvider).NotifyUserAuthentication(userName);
            }

            return userResponse;
        }

        public async Task LogoutAsync()
        {
            await localStorage.RemoveItemAsync("authToken");
            httpClient.DefaultRequestHeaders.Authorization = null;
            ((CustomAuthenticationStateProvider)authenticationStateProvider).NotifyUserLogout();
        }
    }
}
