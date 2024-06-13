using FluentValidation.TestHelper;
using LibraryOfBooks.Service.DTOs.BookCategories;
using LibraryOfBooks.Service.Validators.BookCategories;

namespace LibraryOfBooks.UnitTest.ValidatorTests.BookCategories;

public class BookCategoryCreationDtoValidatorTest
{
    private readonly BookCategoryCreationDtoValidator validator;

    public BookCategoryCreationDtoValidatorTest()
    {
        this.validator = new BookCategoryCreationDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new BookCategoryCreationDto { Name = string.Empty };

        var result = this.validator.TestValidate(model);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        var model = new BookCategoryCreationDto { Name = new string('a', 65) };

        var result = this.validator.TestValidate(model);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var model = new BookCategoryCreationDto { Name = "Valid" };

        var result = this.validator.TestValidate(model);

        Assert.True(result.IsValid);
    }
}
