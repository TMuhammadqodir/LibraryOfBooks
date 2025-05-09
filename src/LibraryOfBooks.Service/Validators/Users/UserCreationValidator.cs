﻿using FluentValidation;
using LibraryOfBooks.Service.DTOs.Users;

namespace LibraryOfBooks.Service.Validators.Users;

public class UserCreationDtoValidator : AbstractValidator<UserCreationDto>
{
    public UserCreationDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithMessage("First name is required.")
            .Length(1, 64)
                .WithMessage("First name must be between 1 and 64 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage("Last name is required.")
            .Length(1, 64)
                .WithMessage("Last name must be between 1 and 64 characters.");

        RuleFor(x => x.UserName)
            .NotEmpty()
                .WithMessage("Email is required.")
            .Length(1, 64)
                .WithMessage("Last userName must be between 1 and 64 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().
                WithMessage("Password is required.")
            .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");
    }
}