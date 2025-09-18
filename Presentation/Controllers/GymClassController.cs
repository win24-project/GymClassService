using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GymClassController(IGymClassService gymClassService) : ControllerBase
{
  private readonly IGymClassService _gymClassService = gymClassService;

  [HttpPost]
  public async Task<IActionResult> Create(CreateGymClassRequest request)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = await _gymClassService.CreateGymClassAsync(request);
    return result != null ? Ok(result) : StatusCode(500, result!.Error);
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var result = await _gymClassService.GetAllGymClassesAsync();
    return Ok(result);
  }
}