using Microsoft.AspNetCore.Http;

namespace LibraryOfBooks.Web.DTOs.Assets;

public class AssetCreationDto
{
	public IFormFile FormFile { get; set; }
}