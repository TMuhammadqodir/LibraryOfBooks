using LibraryOfBooks.Service.DTOs.Comments;
using LibraryOfBooks.Service.Interfaces;
using LibraryOfBooks.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryOfBooks.WebApi.Controllers;

public class CommentsController : BaseController
{
    private readonly ICommentService commentService;
    public CommentsController(ICommentService commentService)
    {
        this.commentService = commentService;
    }


    [HttpPost("create")]
    public async ValueTask<IActionResult> PostAsync(CommentCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Succes",
            Data = await commentService.AddCommentBookAsync(dto)
        });

    [HttpPost("update")]
    public async ValueTask<IActionResult> UpdateAsync(CommentUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await commentService.ModifyCommentBookAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await commentService.DeleteCommentBookAsync(id)
        });
}
