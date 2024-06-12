using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Service.DTOs.BookCategories;

namespace LibraryOfBooks.Service.Interfaces;

public interface IBookCategoryService
{
    ValueTask<BookCategoryResultDto> AddAsync(BookCategoryCreationDto dto);
    ValueTask<BookCategoryResultDto> ModifyAsync(BookCategoryUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<BookCategoryResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<BookCategoryResultDto>> RetrieveAllAsync();

}
