using LibraryOfBooks.Domain.Commons;

namespace LibraryOfBooks.Domain.Entities;

public class Book : Auditable
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public long FileId { get; set; }
    public Asset File { get; set; }

    public long? ImageId { get; set; }
    public Asset Image { get; set; }

    public long CategoryId { get; set; }
    public BookCategory Category { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
