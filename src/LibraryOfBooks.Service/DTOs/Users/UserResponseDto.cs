using LibraryOfBooks.Domain.Enums;

namespace LibraryOfBooks.Service.DTOs.Users;

public class UserResponseDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public EUserRole UserRole { get; set; }
    public string Token { get; set; }
}
