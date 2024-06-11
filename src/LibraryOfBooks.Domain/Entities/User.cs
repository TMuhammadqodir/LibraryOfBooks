using LibraryOfBooks.Domain.Commons;
using LibraryOfBooks.Domain.Enums;
using System.Data;

namespace LibraryOfBooks.Domain.Entities;

internal class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; } = default!;
    public EUserRole UserRole { get; set; } = EUserRole.User;

    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
}
