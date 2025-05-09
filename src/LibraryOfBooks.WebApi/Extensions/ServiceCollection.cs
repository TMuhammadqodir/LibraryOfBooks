﻿using FluentValidation;
using LibraryOfBooks.Dataccess.IRepositories;
using LibraryOfBooks.Dataccess.Repositories;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Service.DTOs.BookCategories;
using LibraryOfBooks.Service.DTOs.Books;
using LibraryOfBooks.Service.DTOs.Users;
using LibraryOfBooks.Service.Helpers;
using LibraryOfBooks.Service.Interfaces;
using LibraryOfBooks.Service.Mappers;
using LibraryOfBooks.Service.Services;
using LibraryOfBooks.Service.Validators.BookCategories;
using LibraryOfBooks.Service.Validators.Books;
using LibraryOfBooks.Service.Validators.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LibraryOfBooks.WebApi.Extensions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {

        //HttpContexts
        services.AddHttpContextAccessor();

        //Services
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IBookCategoryService, BookCategoryService>();

        //Validators
        services.AddScoped<IValidator<BookUpdateDto>, BookUpdateDtoValidator>();
        services.AddScoped<IValidator<BookCreationDto>, BookCreationDtoValidator>();
        services.AddScoped<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
        services.AddScoped<IValidator<UserCreationDto>, UserCreationDtoValidator>();
        services.AddScoped<IValidator<BookCategoryUpdateDto>, BookCategoryUpdateDtoValidator>();
        services.AddScoped<IValidator<BookCategoryCreationDto>, BookCategoryCreationDtoValidator>();

        //Mappers
        services.AddAutoMapper(typeof(MappingProfile));


        //Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // HttpContextHelper ni konfiguratsiya qilish
        var serviceProvider = services.BuildServiceProvider();
        var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        HttpContextHelper.Configure(httpContextAccessor);

        //Swagger Lowercase
        services.AddRouting(options => options.LowercaseUrls = true);
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}
