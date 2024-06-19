using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryOfBooks.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin, SuperAdmin")]
public class BaseController : ControllerBase
{
}
