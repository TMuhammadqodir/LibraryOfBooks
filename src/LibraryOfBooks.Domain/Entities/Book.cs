using LibraryOfBooks.Domain.Commons;

namespace LibraryOfBooks.Domain.Entities;

internal class Book : Auditable
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
}
