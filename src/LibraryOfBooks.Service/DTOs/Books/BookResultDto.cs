using LibraryOfBooks.Service.DTOs.Assets;

namespace LibraryOfBooks.Service.DTOs.Books;

public class BookResultDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public string Description { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public AssetResultDto File { get; set; }
	public AssetResultDto Image { get; set; }
}
