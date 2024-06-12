using AutoMapper;
using LibraryOfBooks.Dataccess.IRepositories;
using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Domain.Enums;
using LibraryOfBooks.Service.DTOs.Assets;
using LibraryOfBooks.Service.DTOs.Books;
using LibraryOfBooks.Service.Exceptions;
using LibraryOfBooks.Service.Extensions;
using LibraryOfBooks.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryOfBooks.Service.Services;

public class BookService : IBookService
{
    private readonly IMapper mapper;
    private readonly IAssetService assetService;
    private readonly IRepository<Book> bookRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Favorite> favoriteRepository;
    private readonly IRepository<BookCategory> categoryRepository;

    public BookService(IMapper mapper,
        IRepository<Book> bookRepository,
        IRepository<BookCategory> categoryRepository,
        IRepository<User> userRepository,
        IRepository<Favorite> favoriteRepository,
        IAssetService assetService)
    {
        this.mapper = mapper;
        this.assetService = assetService;
        this.bookRepository = bookRepository;
        this.categoryRepository = categoryRepository;
        this.userRepository = userRepository;
        this.favoriteRepository = favoriteRepository;
    }

    public async ValueTask<BookResultDto> AddAsync(BookCreationDto dto)
    {
        var category = await this.categoryRepository.SelectAsync(c => c.Id.Equals(dto.CategoryId))
            ?? throw new NotFoundException("Category is not found");

        var user = await this.userRepository.SelectAsync(c => c.Id.Equals(dto.UserId))
            ?? throw new NotFoundException("user is not found");

        var updloadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, EUploadType.Image);
        var updloadedFile = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.File }, EUploadType.File);

        var mappedBook = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Description = dto.Description,
            Category = category,
            CategoryId = dto.CategoryId,
            User = user,
            UserId = dto.UserId,
            Image = updloadedImage,
            ImageId = updloadedImage.Id,
            File = updloadedFile,
            FileId = updloadedFile.Id,
        };
        await this.bookRepository.InsertAsync(mappedBook);
        await this.bookRepository.SaveAsync();

        return this.mapper.Map<BookResultDto>(mappedBook);
    }

    public async ValueTask<BookResultDto> RetrieveByIdAsync(long id)
    {
        var book = await this.bookRepository.SelectAsync(expression: b => b.Id.Equals(id),
            includes: new[] { "Image", "File" })
            ?? throw new NotFoundException("This book is not found");

        return this.mapper.Map<BookResultDto>(book);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var book = await this.bookRepository.SelectAsync(b => b.Id.Equals(id),
            includes: new[] { "Image", "File" })
            ?? throw new NotFoundException("This book is not found");

        this.bookRepository.Delete(book);
        await this.assetService.RemoveAsync(book.Image);
        await this.assetService.RemoveAsync(book.File);
        await this.bookRepository.SaveAsync();
        return true;
    }

    public async ValueTask<BookResultDto> ModifyAsync(BookUpdateDto dto)
    {
        var book = await this.bookRepository.SelectAsync(b => b.Id.Equals(dto.Id),
            includes: new[] { "Image", "File" })
            ?? throw new NotFoundException("This book is not found");

        var user = await this.userRepository.SelectAsync(c => c.Id.Equals(dto.UserId))
            ?? throw new NotFoundException("user is not found");

        var category = await this.categoryRepository.SelectAsync(c => c.Id.Equals(dto.CategoryId))
           ?? throw new NotFoundException("Category is not found");

        var updloadedImage = new Asset();
        if (dto.Image is not null)
        {
            await this.assetService.RemoveAsync(book.Image);
            updloadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, EUploadType.Image);
        }

        var updloadedFile = new Asset();
        if (dto.File is not null)
        {
            await this.assetService.RemoveAsync(book.File);
            updloadedFile = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.File }, EUploadType.File);
        }

        if (updloadedImage.Id > 0)
        {
            book.Image ??= new Asset();

            book.ImageId = updloadedImage.Id;
            book.Image.FileName = updloadedImage.FileName;
            book.Image.FilePath = updloadedImage.FilePath;
        }

        if (updloadedFile.Id > 0)
        {
            book.File ??= new Asset();

            book.FileId = updloadedFile.Id;
            book.File.FileName = updloadedImage.FileName;
            book.File.FilePath = updloadedImage.FilePath;
        }

        book.Title = dto.Title;
        book.Description = dto.Description;
        book.CategoryId = dto.CategoryId;
        book.UserId = dto.UserId;
        book.Author = dto.Author;

        this.bookRepository.Update(book);
        await this.bookRepository.SaveAsync();

        return this.mapper.Map<BookResultDto>(book);
    }

    public async ValueTask<IEnumerable<BookResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
    {
        var books = await this.bookRepository.SelectAll(includes: new[] { "Image", "File" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
            books = books.Where(user => user.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        return this.mapper.Map<IEnumerable<BookResultDto>>(books);
    }

    public async ValueTask<IEnumerable<BookResultDto>> RetrieveAllByCategoryIdAsync(long categoryId)
    {
        var books = await this.bookRepository.SelectAll(q => q.CategoryId.Equals(categoryId),
            includes: new[] { "Image", "File" })
            .ToListAsync();

        return this.mapper.Map<IEnumerable<BookResultDto>>(books);
    }

    public async ValueTask<bool> AddFavoriteBookAsync(long userId, long bookId)
    {
        var user = await userRepository.SelectAsync(u => u.Id.Equals(userId))
            ?? throw new NotFoundException($"This user not found with {userId}");

        var book = await bookRepository.SelectAsync(b => b.Id.Equals(bookId))
            ?? throw new NotFoundException($"This book not found with {bookId}");

        var favorite = await this.favoriteRepository.SelectAsync(f => f.UserId.Equals(userId)
            && f.BookId.Equals(bookId));
        if (favorite is not null) throw new AlreadyExistException("This favorite already exist");

        var Favorite = new Favorite()
        {
            UserId = userId,
            BookId = bookId,
        };

        var result = await this.favoriteRepository.InsertAsync(Favorite);
        await this.favoriteRepository.SaveAsync();

        return true;
    }

    public async ValueTask<bool> DeleteFavoriteBookAsync(long userId, long bookId)
    {
        var user = await userRepository.SelectAsync(u => u.Id.Equals(userId))
            ?? throw new NotFoundException($"This user not found with {userId}");

        var book = await bookRepository.SelectAsync(b => b.Id.Equals(bookId))
            ?? throw new NotFoundException($"This book not found with {bookId}");

        var favorite = await this.favoriteRepository.SelectAsync(f => f.UserId.Equals(userId)
            && f.BookId.Equals(bookId))
            ?? throw new NotFoundException("This favorite not found");

        this.favoriteRepository.Delete(favorite);
        await this.favoriteRepository.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<BookResultDto>> GetAllFavoriteBookAsync(long userId)
    {
        var user = await userRepository.SelectAsync(u => u.Id.Equals(userId))
            ?? throw new NotFoundException($"This user not found with {userId}");

        var favorites = await this.favoriteRepository.SelectAll(f => f.UserId.Equals(userId),
            includes: new[] { "Book" })
            .ToListAsync();

        var books = favorites.Select(f => f.Book).ToList();

        return this.mapper.Map<IEnumerable<BookResultDto>>(books);
    }
}
