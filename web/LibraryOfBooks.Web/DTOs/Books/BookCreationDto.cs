using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace LibraryOfBooks.Web.DTOs.Books;

public class BookCreationDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public IBrowserFile File { get; set; }
    public IBrowserFile Image { get; set; }
}