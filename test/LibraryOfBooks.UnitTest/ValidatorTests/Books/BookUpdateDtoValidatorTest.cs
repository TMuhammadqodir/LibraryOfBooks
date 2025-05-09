﻿using FluentValidation.TestHelper;
using LibraryOfBooks.Service.DTOs.Books;
using LibraryOfBooks.Service.Validators.Books;
using Microsoft.AspNetCore.Http;
using Moq;

namespace LibraryOfBooks.UnitTest.ValidatorTests.Books
{
    public class BookUpdateDtoValidatorTest
    {
        private readonly BookUpdateDtoValidator validator;

        public BookUpdateDtoValidatorTest()
        {
            this.validator = new BookUpdateDtoValidator();
        }

        private IFormFile CreateMockFile(int length)
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(length);
            return fileMock.Object;
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new BookUpdateDto
            {
                Id = 0,
                Title = "Valid Title",
                Author = "Valid Author",
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            var model = new BookUpdateDto
            {
                Id = -1,
                Title = "Valid Title",
                Author = "Valid Author",
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = string.Empty,
                Author = "Valid Author",
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Too_Long()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = new string('a', 129),
                Author = "Valid Author",
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Author_Is_Empty()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = "Valid Title",
                Author = string.Empty,
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Author_Is_Too_Long()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = "Valid Title",
                Author = new string('a', 65),
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = "Valid Title",
                Author = "Valid Author",
                Description = string.Empty,
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Too_Long()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = "Valid Title",
                Author = "Valid Author",
                Description = new string('a', 513),
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_CategoryId_Is_Zero()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = "Valid Title",
                Author = "Valid Author",
                Description = "Valid Description",
                CategoryId = 0,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_UserId_Is_Zero()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = "Valid Title",
                Author = "Valid Author",
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 0,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            var model = new BookUpdateDto
            {
                Id = 1,
                Title = "Valid Title",
                Author = "Valid Author",
                Description = "Valid Description",
                CategoryId = 1,
                UserId = 1,
                File = CreateMockFile(100),
                Image = CreateMockFile(100)
            };

            var result = this.validator.TestValidate(model);

            Assert.True(result.IsValid);
        }
    }
}
