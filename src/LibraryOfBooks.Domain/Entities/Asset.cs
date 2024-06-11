using LibraryOfBooks.Domain.Commons;

namespace LibraryOfBooks.Domain.Entities;

internal class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}
