﻿using FluentValidation.TestHelper;
using LibraryOfBooks.Service.DTOs.Users;
using LibraryOfBooks.Service.Validators.Users;

namespace LibraryOfBooks.UnitTest.ValidatorTests.Users
{
    public class UserUpdateDtoValidatorTest
    {
        private readonly UserUpdateDtoValidator validator;

        public UserUpdateDtoValidatorTest()
        {
            this.validator = new UserUpdateDtoValidator();
        }

        [Fact]
        public void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoeexample",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new UserUpdateDto
            {
                Id = 0,
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoeexample",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            var model = new UserUpdateDto
            {
                Id = -1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoeexample",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = string.Empty,
                LastName = "Doe",
                UserName = "johndoeexample",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Too_Long()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = new string('a', 65),
                LastName = "Doe",
                UserName = "johndoeexample",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_LastName_Is_Empty()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = "John",
                LastName = string.Empty,
                UserName = "johndoeexample",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_LastName_Is_Too_Long()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = "John",
                LastName = new string('a', 65),
                UserName = "johndoeexample",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = new string('a', 70),
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid_Format()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "",
                Password = "password123"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoeexample",
                Password = string.Empty
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
            var model = new UserUpdateDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoeexample",
                Password = "12345"
            };

            var result = this.validator.TestValidate(model);

            Assert.False(result.IsValid);
        }
    }
}