﻿using LibraryOfBooks.Domain.Configurations;
using LibraryOfBooks.Domain.Enums;
using LibraryOfBooks.Service.DTOs.Users;
using LibraryOfBooks.Service.Interfaces;
using LibraryOfBooks.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryOfBooks.WebApi.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async ValueTask<IActionResult> PostAsync([FromBody] UserCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async ValueTask<IActionResult> PutAsync([FromBody] UserUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RemoveAsync(id)
        });

    [HttpGet("get/{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RetrieveByIdAsync(id)
        });



    [Authorize(Roles = "Admin, SuperAdmin")]
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync( 
        [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.RetrieveAllAsync(@params)
        });


    [Authorize(Roles = "SuperAdmin")]
    [HttpPatch("upgrade-role")]
    public async ValueTask<IActionResult> UpgradeRoleAsync(long id, EUserRole role)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UpgradeRoleAsync(id, role)
        });
}
