using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Service.DTOs.Books;

namespace LibraryOfBooks.Service.Interfaces;

public interface IBookService
{
    ValueTask<BookResultDto> AddAsync(BookCreationDto dto);
    ValueTask<BookResultDto> ModifyAsync(BookUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<BookResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<BookResultDto>> RetrieveByUserIdAsync();
    ValueTask<IEnumerable<BookResultDto>> RetrieveAllAsync(string search = null);
    ValueTask<IEnumerable<BookResultDto>> RetrieveAllByCategoryIdAsync(long categoryId);
    ValueTask<bool> AddFavoriteBookAsync(long bookId);
    ValueTask<bool> DeleteFavoriteBookAsync(long bookId);
    ValueTask<IEnumerable<BookResultDto>> GetAllFavoriteBookAsync();
}
