using AutoMapper;
using FluentValidation;
using LibraryOfBooks.Dataccess.IRepositories;
using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Domain.Enums;
using LibraryOfBooks.Service.DTOs.Assets;
using LibraryOfBooks.Service.DTOs.Users;
using LibraryOfBooks.Service.Exceptions;
using LibraryOfBooks.Service.Extensions;
using LibraryOfBooks.Service.Helpers;
using LibraryOfBooks.Service.Interfaces;
using LibraryOfBooks.Service.Validators.Users;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryOfBooks.Service.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    private readonly IValidator<UserUpdateDto> userUpdateDtoValidator;
    private readonly IValidator<UserCreationDto> userCreationDtoValidator;

    public UserService(IMapper mapper,
        IRepository<User> repository,
        IValidator<UserUpdateDto> userUpdateDtoValidator,
        IValidator<UserCreationDto> userCreationDtoValidator)
    {
        this.mapper = mapper;
        this.userRepository = repository;
        this.userUpdateDtoValidator = userUpdateDtoValidator;
        this.userCreationDtoValidator = userCreationDtoValidator;
    }

    public async ValueTask<UserResultDto> AddAsync(UserCreationDto dto)
    {
        var resultValidator = await this.userCreationDtoValidator.ValidateAsync(dto);

        if (resultValidator.Errors.Any())
            throw new CustomException(499, resultValidator.Errors.FirstOrDefault().ToString());

        User user = await this.userRepository.SelectAsync(x => x.Phone.Equals(dto.Phone));
        if (user is not null)
            throw new AlreadyExistException($"This phone is already exist");

        var mappedUser = this.mapper.Map<User>(dto);

        mappedUser.PasswordHash = PasswordHash.Encrypt(dto.Password);
        await this.userRepository.InsertAsync(mappedUser);
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(mappedUser);
    }

    public async ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        var resultValidator = await this.userUpdateDtoValidator.ValidateAsync(dto);

        if (resultValidator.Errors.Any())
            throw new CustomException(499, resultValidator.Errors.FirstOrDefault().ToString());

        User existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This user is not found with Id = {dto.Id}");

        this.mapper.Map(dto, existUser);

        existUser.PasswordHash = PasswordHash.Encrypt(dto.Password);
        this.userRepository.Update(existUser);
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(existUser);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(id), includes: new[] { "Asset" })
            ?? throw new NotFoundException($"This user is not found with Id = {id}");

        this.userRepository.Delete(existUser);
        await this.userRepository.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
    {
        var users = await this.userRepository.SelectAll()
            .ToPaginate(@params)
            .ToListAsync();

        if (!string.IsNullOrEmpty(search))
            users = users.Where(user => user.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        return this.mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync()
    {
        var users = await this.userRepository.SelectAll().ToListAsync();
        return this.mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async ValueTask<UserResultDto> RetrieveByIdAsync(long id)
    {
        var existUser = await this.userRepository.SelectAsync(expression: u => u.Id.Equals(id))
            ?? throw new NotFoundException($"This user is not found with Id = {id}");

        return this.mapper.Map<UserResultDto>(existUser);
    }

    public async ValueTask<UserResultDto> UpgradeRoleAsync(long id, EUserRole role)
    {
        var existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(id))
            ?? throw new NotFoundException($"This user is not found with Id = {id}");

        existUser.UserRole = role;
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(existUser);
    }
}
