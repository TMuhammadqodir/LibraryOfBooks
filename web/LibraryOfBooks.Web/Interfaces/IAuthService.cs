using LibraryOfBooks.Web.DTOs.Responses;
using LibraryOfBooks.Web.DTOs.Users;

namespace LibraryOfBooks.Web.Interfaces;

public interface IAuthService
{
    Task<Response<UserResponseDto>> GenerateTokenAsync(string userName, string password);
    Task LogoutAsync();
}
