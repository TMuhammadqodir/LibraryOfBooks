using LibraryOfBooks.Web.DTOs.Books;
using LibraryOfBooks.Web.DTOs.Responses;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LibraryOfBooks.Web.Services;

public class BookService
{
    private readonly HttpClient _httpClient;

    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<BookResultDto>> CreateBookAsync(BookCreationDto book)
    {
        var content = new MultipartFormDataContent();

        content.Add(new StringContent(book.Title), "Title");
        content.Add(new StringContent(book.Author), "Author");
        content.Add(new StringContent(book.Description), "Description");
        content.Add(new StringContent(book.CategoryId.ToString()), "CategoryId");
        content.Add(new StringContent(book.UserId.ToString()), "UserId");
        if (book.File != null)
        {
            var fileContent = new StreamContent(book.File.OpenReadStream(book.File.Size));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(book.File.ContentType); // content type added
            content.Add(fileContent, "File", book.File.Name);
        }

        if (book.Image != null)
        {
            var imageContent = new StreamContent(book.Image.OpenReadStream(book.Image.Size));
            imageContent.Headers.ContentType = new MediaTypeHeaderValue(book.Image.ContentType); // content type added
            content.Add(imageContent, "Image", book.Image.Name);
        }

        var response = await _httpClient.PostAsync("api/books/create", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Error creating book: {errorContent}");
        }

        return await response.Content.ReadFromJsonAsync<Response<BookResultDto>>();
    }

    public async Task<Response<BookResultDto>> UpdateBookAsync(BookUpdateDto book)
    {
        var response = await _httpClient.PutAsJsonAsync("api/books/update", book);
        return await response.Content.ReadFromJsonAsync<Response<BookResultDto>>();
    }

    public async Task<Response<bool>> DeleteBookAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"api/books/delete/{id}");
        return await response.Content.ReadFromJsonAsync<Response<bool>>();
    }

    public async Task<Response<BookResultDto>> GetBookAsync(long id)
    {
        var response = await _httpClient.GetFromJsonAsync<Response<BookResultDto>>($"api/books/get/{id}");
        return response;
    }

    public async Task<Response<IEnumerable<BookResultDto>>> GetAllBooksAsync(string search = null)
    {
        var response = await _httpClient.GetFromJsonAsync<Response<IEnumerable<BookResultDto>>>($"api/books/get-all?search={search}");
        return response;
    }

    public async Task<Response<IEnumerable<BookResultDto>>> GetByUserIdBooksAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Response<IEnumerable<BookResultDto>>>($"api/books/get-by-user-id");
        return response;
    }

    public async Task<Response<IEnumerable<BookResultDto>>> GetBooksByCategoryIdAsync(long categoryId)
    {
        var response = await _httpClient.GetFromJsonAsync<Response<IEnumerable<BookResultDto>>>($"api/books/get-by-categoryId/{categoryId}");
        return response;
    }

    public async Task<Response<bool>> AddFavoriteBookAsync(long bookId)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/books/add-favorite-book?bookId={bookId}", new { });
        return await response.Content.ReadFromJsonAsync<Response<bool>>();
    }

    public async Task<Response<bool>> DeleteFavoriteBookAsync(long bookId)
    {
        var response = await _httpClient.DeleteAsync($"api/books/delete-favorite-book?bookId={bookId}");
        return await response.Content.ReadFromJsonAsync<Response<bool>>();
    }

    public async Task<Response<IEnumerable<BookResultDto>>> GetAllFavoriteBooksAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Response<IEnumerable<BookResultDto>>>($"api/books/get-all-favorite");
        return response;
    }
}
