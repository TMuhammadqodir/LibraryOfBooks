using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Domain.Enums;
using LibraryOfBooks.Service.DTOs.Users;
using System.Data;

namespace LibraryOfBooks.Service.Interfaces;

public interface IUserService
{
    ValueTask<UserResultDto> AddAsync(UserCreationDto dto);
    ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<UserResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null);
    ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync();
    ValueTask<UserResultDto> UpgradeRoleAsync(long id, EUserRole role);
}
