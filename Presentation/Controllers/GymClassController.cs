using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api")]
[ApiController]
public class GymClassController(IGymClassService gymClassService) : ControllerBase
{
  private readonly IGymClassService _gymClassService = gymClassService;

  [Authorize(Roles = "Admin")]
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

  [Authorize(Roles = "Admin")]
  [HttpPost("edit")]
  public async Task<IActionResult> Edit(EditGymClassRequest request)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = await _gymClassService.EditGymClassAsync(request);
    if (!result.Success)
    {
      return BadRequest(new { error = result.Error });
    }

    return Ok(result);
  }

  [Authorize(Roles = "Admin")]
  [HttpPost("delete/{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return BadRequest();
    }

    var result = await _gymClassService.RemoveGymClassAsync(id);
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