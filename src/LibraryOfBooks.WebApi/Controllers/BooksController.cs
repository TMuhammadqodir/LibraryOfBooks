using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Service.DTOs.Books;
using LibraryOfBooks.Service.Helpers;
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
    public async Task<IActionResult> PostAsync([FromForm] BookCreationDto dto)
    {
        dto.UserId = HttpContextHelper.GetUserId();

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.AddAsync(dto)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] BookUpdateDto dto)
    { 
        dto.UserId = HttpContextHelper.GetUserId();

        return Ok(new Response
           {
               StatusCode = 200,
               Message = "Success",
               Data = await this.bookService.ModifyAsync(dto)
           });
    }
    
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
    public async ValueTask<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.RetrieveAllAsync()
        });

    
    [HttpGet("get-by-user-id")]
    public async ValueTask<IActionResult> GetByUserIdAsync()
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.bookService.RetrieveByUserIdAsync()
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
    public async ValueTask<IActionResult> AddFavoriteBookAsync(long bookId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.AddFavoriteBookAsync(bookId)
        });

    
    [HttpDelete("delete-favorite-book")]
    public async ValueTask<IActionResult> DeleteFavoriteBookAsync(long bookId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.DeleteFavoriteBookAsync(bookId)
        });


    [HttpGet("get-all-favorite")]
    public async ValueTask<IActionResult> GetAllFavoriteAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.GetAllFavoriteBookAsync()
        });
}
