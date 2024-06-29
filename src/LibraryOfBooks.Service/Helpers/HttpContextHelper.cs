using Microsoft.AspNetCore.Http;

namespace LibraryOfBooks.Service.Helpers; 

public static class HttpContextHelper
{
    private static IHttpContextAccessor _httpContextAccessor;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public static long? GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
        return userIdClaim != null ? Convert.ToInt64(userIdClaim) : (long?)null;
    }
}
