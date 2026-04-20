using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Hotel2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Rooms working!");
    }
}