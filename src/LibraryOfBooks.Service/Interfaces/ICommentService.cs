using LibraryOfBooks.Service.DTOs.Books;
using LibraryOfBooks.Service.DTOs.Comments;

namespace LibraryOfBooks.Service.Interfaces;

public interface ICommentService
{
    ValueTask<CommentResultDto> AddCommentBookAsync(CommentCreationDto dto);
    ValueTask<bool> DeleteCommentBookAsync(long id);
    ValueTask<CommentResultDto> ModifyCommentBookAsync(CommentUpdateDto dto);
}
