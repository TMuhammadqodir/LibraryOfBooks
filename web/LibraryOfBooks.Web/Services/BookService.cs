﻿using LibraryOfBooks.Web.DTOs.Books;
using LibraryOfBooks.Web.DTOs.Responses;
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
        var response = await _httpClient.PostAsJsonAsync("api/books/create", book);

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

    public async Task<Response<IEnumerable<BookResultDto>>> GetBooksByCategoryIdAsync(long categoryId)
    {
        var response = await _httpClient.GetFromJsonAsync<Response<IEnumerable<BookResultDto>>>($"api/books/get-by-categoryId/{categoryId}");
        return response;
    }

    public async Task<Response<bool>> AddFavoriteBookAsync(long userId, long bookId)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/books/add-favorite-book?userId={userId}&bookId={bookId}", new { });
        return await response.Content.ReadFromJsonAsync<Response<bool>>();
    }

    public async Task<Response<bool>> DeleteFavoriteBookAsync(long userId, long bookId)
    {
        var response = await _httpClient.DeleteAsync($"api/books/delete-favorite-book?userId={userId}&bookId={bookId}");
        return await response.Content.ReadFromJsonAsync<Response<bool>>();
    }

    public async Task<Response<IEnumerable<BookResultDto>>> GetAllFavoriteBooksAsync(long userId)
    {
        var response = await _httpClient.GetFromJsonAsync<Response<IEnumerable<BookResultDto>>>($"api/books/get-all-favorite?userId={userId}");
        return response;
    }
}