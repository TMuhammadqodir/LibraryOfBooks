using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Service.DTOs.Books;
using LibraryOfBooks.Service.Interfaces;
using LibraryOfBooks.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryOfBooks.WebApi.Controllers;

public class BooksController : BaseController
{
    private readonly IBookService bookService;
    public BooksController(IBookService bookService)
    {
        this.bookService = bookService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromQuery] BookCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromQuery] BookUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.DeleteAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.RetrieveByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.RetrieveAllAsync(@params, search)
        });


    [AllowAnonymous]
    [HttpGet("get-by-categoryId/{categoryId:long}")]
    public async ValueTask<IActionResult> GetAllByCategoryIdAsync(long categoryId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.RetrieveAllByCategoryIdAsync(categoryId)
        });

    [HttpPost("add-favorite-book")]
    public async ValueTask<IActionResult> AddFavoriteBookAsync(long userId, long bookId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.AddFavoriteBookAsync(userId, bookId)
        });

    [HttpDelete("delete-favorite-book")]
    public async ValueTask<IActionResult> DeleteFavoriteBookAsync(long userId, long bookId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.DeleteFavoriteBookAsync(userId, bookId)
        });

    [HttpGet("get-all-favorite")]
    public async ValueTask<IActionResult> GetAllFavoriteAsync(long userId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.GetAllFavoriteBookAsync(userId)
        });
}
