using AutoMapper;
using FluentValidation;
using LibraryOfBooks.Dataccess.IRepositories;
using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Service.DTOs.BookCategories;
using LibraryOfBooks.Service.Exceptions;
using LibraryOfBooks.Service.Extensions;
using LibraryOfBooks.Service.Interfaces;
using LibraryOfBooks.Service.Validators.BookCategories;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace LibraryOfBooks.Service.Services;

public class BookCategoryService : IBookCategoryService
{
    private readonly IMapper mapper;
    private readonly IRepository<BookCategory> bookCategoryRepository;
    private readonly IValidator<BookCategoryUpdateDto> bookCategoryUpdateDtoValidator;
    private readonly IValidator<BookCategoryCreationDto> bookCategoryCreationDtoValidator;

    public BookCategoryService(
        IMapper mapper,
        IRepository<BookCategory> bookCategoryRepository,
        IValidator<BookCategoryUpdateDto> bookCategoryUpdateDtoValidator,
        IValidator<BookCategoryCreationDto> bookCategoryCreationDtoValidator)
    {
        this.mapper = mapper;
        this.bookCategoryRepository = bookCategoryRepository;
        this.bookCategoryCreationDtoValidator = bookCategoryCreationDtoValidator;
        this.bookCategoryUpdateDtoValidator = bookCategoryUpdateDtoValidator;
    }
    public async ValueTask<BookCategoryResultDto> AddAsync(BookCategoryCreationDto dto)
    {
        var resultValidator = await this.bookCategoryCreationDtoValidator.ValidateAsync(dto);

        if (resultValidator.Errors.Any())
            throw new CustomException(499, resultValidator.Errors.FirstOrDefault().ToString());

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
        var resultValidator = await this.bookCategoryUpdateDtoValidator.ValidateAsync(dto);

        if (resultValidator.Errors.Any())
            throw new CustomException(499, resultValidator.Errors.FirstOrDefault().ToString());

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
