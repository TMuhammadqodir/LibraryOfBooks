using AutoMapper;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Service.DTOs.Assets;
using LibraryOfBooks.Service.DTOs.BookCategories;
using LibraryOfBooks.Service.DTOs.Books;
using LibraryOfBooks.Service.DTOs.Users;

namespace LibraryOfBooks.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Asset, AssetCreationDto>().ReverseMap();
        CreateMap<Asset, AssetResultDto>().ReverseMap();

        CreateMap<Book, BookCreationDto>().ReverseMap();
        CreateMap<Book, BookUpdateDto>().ReverseMap();
        CreateMap<Book, BookResultDto>().ReverseMap();

        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserResponseDto>().ReverseMap();

        CreateMap<BookCategory, BookCategoryCreationDto>().ReverseMap();
        CreateMap<BookCategory, BookCategoryUpdateDto>().ReverseMap();
        CreateMap<BookCategory, BookCategoryResultDto>().ReverseMap();
    }
}
