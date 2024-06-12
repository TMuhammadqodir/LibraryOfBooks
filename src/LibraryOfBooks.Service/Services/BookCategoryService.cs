using AutoMapper;
using LibraryOfBooks.Dataccess.IRepositories;
using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Service.DTOs.BookCategories;
using LibraryOfBooks.Service.Exceptions;
using LibraryOfBooks.Service.Extensions;
using LibraryOfBooks.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryOfBooks.Service.Services;

public class BookCategoryService : IBookCategoryService
{
    private readonly IMapper mapper;
    private readonly IRepository<BookCategory> bookCategoryRepository;

    public BookCategoryService(
        IMapper mapper,
        IRepository<BookCategory> bookCategoryRepository
    ){
        this.mapper = mapper;
        this.bookCategoryRepository = bookCategoryRepository;
    }
    public async ValueTask<BookCategoryResultDto> AddAsync(BookCategoryCreationDto dto)
    {
        var existBookCategory = await this.bookCategoryRepository.SelectAsync(bk => bk.Name.ToLower().Equals(dto.Name.ToLower()));
        if (existBookCategory is not null)
            throw new AlreadyExistException($"This bookCategory already exist with id : {dto.Name}");

        var mappedBookCategory = this.mapper.Map<BookCategory>(dto);

        await this.bookCategoryRepository.InsertAsync(mappedBookCategory);
        await this.bookCategoryRepository.SaveAsync();

        return this.mapper.Map<BookCategoryResultDto>(mappedBookCategory);
    }

    public async ValueTask<BookCategoryResultDto> ModifyAsync(BookCategoryUpdateDto dto)
    {
        var bookCategory = await this.bookCategoryRepository.SelectAsync(bk => bk.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This bookCategory is not found with id : {dto.Id}");

        if (!bookCategory.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase))
        {
            var existBookCategory = await this.bookCategoryRepository.SelectAsync(q => q.Name.ToLower().Equals(dto.Name.ToLower()));
            if (existBookCategory is not null)
                throw new AlreadyExistException($"This bookCategory already exist with id : {dto.Name}");
        }

        this.mapper.Map(dto, bookCategory);

        this.bookCategoryRepository.Update(bookCategory);
        await this.bookCategoryRepository.SaveAsync();

        return this.mapper.Map<BookCategoryResultDto>(bookCategory);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existBookCategory = await this.bookCategoryRepository.SelectAsync(q => q.Id.Equals(id))
            ?? throw new NotFoundException($"This bookCategory is not found with id : {id}");

        this.bookCategoryRepository.Delete(existBookCategory);
        await this.bookCategoryRepository.SaveAsync();

        return true;
    }

    public async ValueTask<BookCategoryResultDto> RetrieveByIdAsync(long id)
    {
        var existBookCategory = await this.bookCategoryRepository.SelectAsync(q => q.Id.Equals(id),
            includes: new[] { "Books" })
                ?? throw new NotFoundException($"This bookCategory is not found with id : {id}");

        return this.mapper.Map<BookCategoryResultDto>(existBookCategory);
    }

    public async ValueTask<IEnumerable<BookCategoryResultDto>> RetrieveAllAsync()
    {
        var allBookCategoryzes = await this.bookCategoryRepository.SelectAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<BookCategoryResultDto>>(allBookCategoryzes);
    }
}
