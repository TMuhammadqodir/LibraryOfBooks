using System.ComponentModel.DataAnnotations;

namespace LibraryOfBooks.Web.DTOs.Users;

public class UserCreationDto
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "First Name must be between 1 and 64 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Last Name must be between 1 and 64 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "User Name is required")]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "User Name must be between 1 and 64 characters")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }
}