using FluentValidation;
using LibraryOfBooks.Service.DTOs.Users;

namespace LibraryOfBooks.Service.Validators.Users;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");

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

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithMessage("Email is not valid.");

        RuleFor(x => x.Phone)
            .NotEmpty()
                .WithMessage("Phone number is required.")
            .Matches(@"^\+\d{1,3}\s?\d{4,14}$")
                .WithMessage("Phone number is not valid.");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Password is required.")
            .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");
    }
}