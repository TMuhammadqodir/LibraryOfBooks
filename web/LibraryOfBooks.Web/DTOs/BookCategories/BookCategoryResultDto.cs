using LibraryOfBooks.Web.DTOs.Books;

namespace LibraryOfBooks.Web.DTOs.BookCategories;

public class BookCategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<BookResultDto> Books { get; set; }
}
