using FluentValidation;
using LibraryOfBooks.Service.DTOs.Books;

namespace LibraryOfBooks.Service.Validators.Books;

public class BookCreationDtoValidator : AbstractValidator<BookCreationDto>
{
    public BookCreationDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage("Title is required.")
            .Length(1, 128)
                .WithMessage("Title must be between 1 and 128 characters.");

        RuleFor(x => x.Author)
            .NotEmpty()
                .WithMessage("Author is required.")
            .Length(1, 64)
                .WithMessage("Author must be between 1 and 64 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage("Description is required.")
            .Length(1, 512)
                .WithMessage("Description must be between 1 and 512 characters.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
                .WithMessage("CategoryId must be greater than 0.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
                .WithMessage("UserId must be greater than 0.");

        RuleFor(x => x.File)
            .NotNull()
                .WithMessage("File is required.")
            .Must(f => f.Length > 0)
                .WithMessage("File cannot be empty.");

        RuleFor(x => x.Image)
            .NotNull()
                .WithMessage("Image is required.")
            .Must(i => i.Length > 0)
                .WithMessage("Image cannot be empty.");
    }
}
