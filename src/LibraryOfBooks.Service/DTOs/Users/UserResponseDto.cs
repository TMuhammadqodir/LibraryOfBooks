using LibraryOfBooks.Domain.Enums;

namespace LibraryOfBooks.WebApi.Models;

public class UserResponseDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public EUserRole UserRole { get; set; }
    public string Token { get; set; }
}
