using LibraryOfBooks.Dataccess.IRepositories;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Domain.Enums;
using LibraryOfBooks.Service.DTOs.Assets;
using LibraryOfBooks.Service.Extensions;
using LibraryOfBooks.Service.Helpers;
using LibraryOfBooks.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibraryOfBooks.Service.Services;

public class AssetService : IAssetService
{
    private readonly IRepository<Asset> repository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AssetService(IRepository<Asset> repository, IHttpContextAccessor httpContextAccessor)
    {
        this.repository = repository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<Asset> UploadAsync(AssetCreationDto dto, EUploadType type)
    {
        var webRootPath = Path.Combine(PathHelper.WebRootPath, type.ToString());

        if (!Directory.Exists(webRootPath))
            Directory.CreateDirectory(webRootPath);

        var fileExtention = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtention}";
        var filePath = Path.Combine(webRootPath, fileName);

        var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());
        await dto.FormFile.CopyToAsync(fileStream);

        var imageUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/{type}/{fileName}";

        var asset = new Asset()
        {
            FileName = fileName,
            FilePath = imageUrl,
        };

        await this.repository.InsertAsync(asset);
        await this.repository.SaveAsync();
        return asset;
    }

    public async ValueTask<bool> RemoveAsync(Asset assetment)
    {
        if (assetment is null)
            return false;

        var existAssetment = await repository.SelectAsync(a => a.Id.Equals(assetment.Id));
        if (existAssetment is null)
            return false;

        this.repository.Delete(existAssetment);
        var result = await this.repository.SaveAsync();
        return true;
    }
}
