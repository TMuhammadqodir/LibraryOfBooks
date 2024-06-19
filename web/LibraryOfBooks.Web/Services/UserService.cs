using LibraryOfBooks.Web.DTOs.Responses;
using LibraryOfBooks.Web.DTOs.Users;
using LibraryOfBooks.Web.Enums;
using LibraryOfBooks.Web.Interfaces;
using System.Net.Http.Json;

namespace LibraryOfBooks.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Response<UserResponseDto>> CreateUserAsync(UserCreationDto dto)
        {
            var response = await httpClient.PostAsJsonAsync("api/users/create", dto);
            return await response.Content.ReadFromJsonAsync<Response<UserResponseDto>>();
        }

        public async Task<Response<UserResponseDto>> UpdateUserAsync(UserUpdateDto dto)
        {
            var response = await httpClient.PutAsJsonAsync("api/users/update", dto);
            return await response.Content.ReadFromJsonAsync<Response<UserResponseDto>>();
        }

        public async Task<Response<bool>> DeleteUserAsync(long id)
        {
            var response = await httpClient.DeleteAsync($"api/users/delete/{id}");
            return await response.Content.ReadFromJsonAsync<Response<bool>>();
        }

        public async Task<Response<UserResponseDto>> GetUserByIdAsync(long id)
        {
            var response = await httpClient.GetAsync($"api/users/get/{id}");
            return await response.Content.ReadFromJsonAsync<Response<UserResponseDto>>();
        }

        public async Task<Response<IEnumerable<UserResultDto>>> GetAllUsersAsync()
        {
            var response = await httpClient.GetAsync("api/users/get-all");
            return await response.Content.ReadFromJsonAsync<Response<IEnumerable<UserResultDto>>>();
        }

        public async Task<Response<UserResponseDto>> UpgradeRoleAsync(long id, EUserRole role)
        {
            var response = await httpClient.PatchAsync($"api/users/upgrade-role?id={id}&role={role}", null);
            return await response.Content.ReadFromJsonAsync<Response<UserResponseDto>>();
        }
    }
}
