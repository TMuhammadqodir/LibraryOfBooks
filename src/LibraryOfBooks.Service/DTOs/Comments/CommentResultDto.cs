namespace LibraryOfBooks.Service.DTOs.Comments;

public class CommentResultDto
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
    public long BookId { get; set; }
}