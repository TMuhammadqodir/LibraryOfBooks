using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LibraryOfBooks.Web.DTOs.Books;

public class BookCreationDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public long CategoryId { get; set; }

    public long? UserId { get; set; }

    [Required(ErrorMessage = "File is required")]
    public IBrowserFile File { get; set; }
    
    [Required(ErrorMessage = "Image is required")]
    public IBrowserFile Image { get; set; }
}