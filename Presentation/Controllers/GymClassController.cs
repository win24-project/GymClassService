using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api")]
[ApiController]
public class GymClassController(IGymClassService gymClassService) : ControllerBase
{
  private readonly IGymClassService _gymClassService = gymClassService;

  [HttpPost("create")]
  public async Task<IActionResult> Create(CreateGymClassRequest request)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = await _gymClassService.CreateGymClassAsync(request);

    if (!result.Success)
    {
      return BadRequest(new { error = result.Error });
    }

    return Ok(result);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll()
  {
    var result = await _gymClassService.GetAllGymClassesAsync();

    if (!result.Success)
    {
      return BadRequest(new { error = result.Error });
    }

    return Ok(result);
  }
}