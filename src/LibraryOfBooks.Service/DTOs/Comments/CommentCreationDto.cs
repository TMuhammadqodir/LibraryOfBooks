using LibraryOfBooks.Domain.Entities;

namespace LibraryOfBooks.Service.DTOs.Comments;

public class CommentCreationDto
{
    public string Text { get; set; }
    public long UserId { get; set; }
    public long BookId { get; set; }
}
