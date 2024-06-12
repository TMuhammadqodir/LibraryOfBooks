using LibraryOfBooks.Service.DTOs.Books;

namespace LibraryOfBooks.Service.DTOs.BookCategories;

public class BookCategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<BookResultDto> Books { get; set; }
}
