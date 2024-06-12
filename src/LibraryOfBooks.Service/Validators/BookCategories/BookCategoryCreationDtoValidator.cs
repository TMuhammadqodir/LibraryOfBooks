using FluentValidation;
using LibraryOfBooks.Service.DTOs.BookCategories;

namespace LibraryOfBooks.Service.Validators.BookCategories;

public class BookCategoryCreationDtoValidator : AbstractValidator<BookCategoryCreationDto>
{
    public BookCategoryCreationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name is required.")
            .Length(1, 64)
                .WithMessage("Name must be between 1 and 64 characters.");
    }
}
