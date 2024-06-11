using LibraryOfBooks.Domain.Commons;

namespace LibraryOfBooks.Domain.Entities;

internal class BookCategory : Auditable
{
    public string Name { get; set; }
}
