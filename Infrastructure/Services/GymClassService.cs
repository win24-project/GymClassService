using Data.Entities;
using Data.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class GymClassService(IGymClassRepository gymClassRepository) : IGymClassService
{
  private readonly IGymClassRepository _gymClassRepository = gymClassRepository;

  public async Task<GymClassResult> CreateGymClassAsync(CreateGymClassRequest request)
  {
    try
    {
      var gymClassEntity = new GymClassEntity
      {
        Title = request.Title,
        Description = request.Description,
        Date = request.Date
      };

      var gymClassResult = await _gymClassRepository.AddAsync(gymClassEntity);
      if (!gymClassResult.Success)
      {
        return new GymClassResult { Success = false, Error = gymClassResult.Error };
      }

      return new GymClassResult { Success = true };
    }
    catch (Exception ex)
    {
      return new GymClassResult { Success = false, Error = ex.Message };
    }
  }

  public async Task<GymClassResult<IEnumerable<GymClass>>> GetAllGymClassesAsync()
  {
    var result = await _gymClassRepository.GetAllAsync();
    var gymClasses = result.Result?.Select(entity => new GymClass
    {
      Id = entity.Id,
      Title = entity.Title,
      Description = entity.Description,
      Date = entity.Date
    });

    return new GymClassResult<IEnumerable<GymClass>> { Success = true, Result = gymClasses };
  }
}