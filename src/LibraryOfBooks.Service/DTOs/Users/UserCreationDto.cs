﻿using Microsoft.AspNetCore.Http;

namespace LibraryOfBooks.Service.DTOs.Users;

public class UserCreationDto
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
}