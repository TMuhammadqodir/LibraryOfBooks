using LibraryOfBooks.Web.DTOs.Responses;
using LibraryOfBooks.Web.DTOs.Users;
using LibraryOfBooks.Web.Enums;

namespace LibraryOfBooks.Web.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserResponseDto>> CreateUserAsync(UserCreationDto dto);
        Task<Response<UserResponseDto>> UpdateUserAsync(UserUpdateDto dto);
        Task<Response<bool>> DeleteUserAsync(long id);
        Task<Response<UserResponseDto>> GetUserByIdAsync(long id);
        Task<Response<IEnumerable<UserResultDto>>> GetAllUsersAsync();
        Task<Response<UserResponseDto>> UpgradeRoleAsync(long id, EUserRole role);
    }
}
