﻿using LibraryOfBooks.Domain.Enums;

namespace LibraryOfBooks.Service.DTOs.Users;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public EUserRole UserRole { get; set; }
}