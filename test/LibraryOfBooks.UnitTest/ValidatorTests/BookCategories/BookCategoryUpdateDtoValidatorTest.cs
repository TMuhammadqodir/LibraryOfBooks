using FluentValidation.TestHelper;
using LibraryOfBooks.Service.DTOs.BookCategories;
using LibraryOfBooks.Service.Validators.BookCategories;

namespace LibraryOfBooks.UnitTest.ValidatorTests.BookCategories;

public class BookCategoryUpdateDtoValidatorTest
{
    private readonly BookCategoryUpdateDtoValidator validator;

    public BookCategoryUpdateDtoValidatorTest()
    {
        this.validator = new BookCategoryUpdateDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Zero()
    {
        var model = new BookCategoryUpdateDto { Id = 0, Name = "Valid Name" };

        var result = this.validator.TestValidate(model);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Negative()
    {
        var model = new BookCategoryUpdateDto { Id = -1, Name = "Valid Name" };

        var result = this.validator.TestValidate(model);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new BookCategoryUpdateDto { Id = 1, Name = string.Empty };

        var result = this.validator.TestValidate(model);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        var model = new BookCategoryUpdateDto { Id = 1, Name = new string('a', 65) };

        var result = this.validator.TestValidate(model);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Model_Is_Valid()
    {
        var model = new BookCategoryUpdateDto { Id = 1, Name = "Valid Name" };

        var result = this.validator.TestValidate(model);

        Assert.True(result.IsValid);
    }
}
