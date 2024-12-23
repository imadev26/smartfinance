using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Models;
using SmartFinanceAPI.Data;

namespace SmartFinanceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserSettingsController : ControllerBase
{
    // Same pattern as UserController, but with UserSettings entity
} 