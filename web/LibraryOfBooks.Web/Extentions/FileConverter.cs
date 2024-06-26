using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace LibraryOfBooks.Web.Extentions;

public static class FileConverter
{
    public static async Task<IFormFile> ToIFormFileAsync(this IBrowserFile browserFile)
    {
        if (browserFile == null)
            return null;

        using var stream = browserFile.OpenReadStream(browserFile.Size);
        var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        ms.Position = 0;

        var formFile = new FormFile(ms, 0, browserFile.Size, browserFile.Name, browserFile.Name)
        {
            Headers = new HeaderDictionary(),
            ContentType = browserFile.ContentType
        };

        formFile.ContentDisposition = $"form-data; name=\"{browserFile.Name}\"; filename=\"{browserFile.Name}\"";

        return formFile;
    }
}
