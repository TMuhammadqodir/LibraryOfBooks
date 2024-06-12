using LibraryOfBooks.Domain.Commons;
using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Service.Exceptions;

namespace LibraryOfBooks.Service.Extensions;

public static class CollectionExtension
{
    public static IEnumerable<T> ToPaginate<T>(this IEnumerable<T> values, PaginationParams @params)
    {
        var source = values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);
        return source;
    }

    public static IQueryable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams @params)
    {
        var source = values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);
        return source;
    }
}
