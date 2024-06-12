using LibraryOfBooks.Service.DTOs.BookCategories;
using LibraryOfBooks.Service.Interfaces;
using LibraryOfBooks.Service.Services;
using LibraryOfBooks.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryOfBooks.WebApi.Controllers;

public class BookCategoriesController : BaseController
{
    private readonly IBookCategoryService bookCategoryService;
    public BookCategoriesController(IBookCategoryService bookCategoryService)
    {
        this.bookCategoryService = bookCategoryService;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> PostAsync(BookCategoryCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Succes",
            Data = await bookCategoryService.AddAsync(dto)
        });

    [HttpPost("update")]
    public async ValueTask<IActionResult> UpdateAsync(BookCategoryUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await bookCategoryService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await bookCategoryService.DeleteAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode=200,
            Message = "Success",
            Data = await bookCategoryService.RetrieveByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await bookCategoryService.RetrieveAllAsync()
        });
}
