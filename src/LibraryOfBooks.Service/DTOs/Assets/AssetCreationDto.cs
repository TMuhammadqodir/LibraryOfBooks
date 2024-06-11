using Microsoft.AspNetCore.Http;

namespace LibraryOfBooks.Service.DTOs.Assets;

public class AssetCreationDto
{
	public IFormFile FormFile { get; set; }
}