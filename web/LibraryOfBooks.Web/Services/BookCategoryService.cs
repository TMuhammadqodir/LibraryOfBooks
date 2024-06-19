using LibraryOfBooks.Web.DTOs.BookCategories;
using LibraryOfBooks.Web.DTOs.Responses;
using System.Net.Http.Json;

namespace LibraryOfBooks.Web.Services;

public class BookCategoryService
{
    private readonly HttpClient _httpClient;

    public BookCategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<BookCategoryResultDto>> GetAllBookCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("api/bookcategories/get-all");
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<Response<IEnumerable<BookCategoryResultDto>>>();

        return responseData?.Data ?? Enumerable.Empty<BookCategoryResultDto>();
    }

    public async Task<BookCategoryResultDto> GetBookCategoryByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"api/bookcategories/get/{id}");
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<Response<BookCategoryResultDto>>();

        return responseData?.Data;
    }

    public async Task<BookCategoryResultDto> CreateBookCategoryAsync(BookCategoryCreationDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/bookcategories/create", dto);
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<Response<BookCategoryResultDto>>();

        return responseData?.Data;
    }

    public async Task<BookCategoryResultDto> UpdateBookCategoryAsync(BookCategoryUpdateDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/bookcategories/update", dto);
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<Response<BookCategoryResultDto>>();

        return responseData?.Data;
    }

    public async Task<bool> DeleteBookCategoryAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"api/bookcategories/delete/{id}");
        response.EnsureSuccessStatusCode();

        return true;
    }
}
