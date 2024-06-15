using LibraryOfBooks.Service.DTOs.Users;

namespace LibraryOfBooks.Service.Interfaces;

public interface IAuthService
{
    ValueTask<UserResponseDto> GenerateTokenAsync(string userName, string originalPassword);
}
