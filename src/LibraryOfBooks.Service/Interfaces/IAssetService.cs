using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Domain.Enums;
using LibraryOfBooks.Service.DTOs.Assets;

namespace LibraryOfBooks.Service.Interfaces;

public interface IAssetService
{
    ValueTask<Asset> UploadAsync(AssetCreationDto dto, EUploadType type);
    ValueTask<bool> RemoveAsync(Asset asset);
}
