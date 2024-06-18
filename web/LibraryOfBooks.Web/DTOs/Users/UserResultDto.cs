using LibraryOfBooks.Web.Enums;

namespace LibraryOfBooks.Web.DTOs.Users;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public EUserRole UserRole { get; set; }
}
