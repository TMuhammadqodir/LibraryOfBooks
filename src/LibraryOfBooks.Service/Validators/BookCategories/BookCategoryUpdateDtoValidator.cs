using FluentValidation;
using LibraryOfBooks.Service.DTOs.BookCategories;

namespace LibraryOfBooks.Service.Validators.BookCategories;

public class BookCategoryUpdateDtoValidator : AbstractValidator<BookCategoryUpdateDto>
{
    public BookCategoryUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name is required.")
            .Length(1, 64)
                .WithMessage("Name must be between 1 and 64 characters.");
    }
}