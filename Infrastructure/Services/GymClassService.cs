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
        Image = request.Image,
        Title = request.Title,
        Description = request.Description,
        Date = request.Date,
        Location = request.Location,
        Instructor = request.Instructor,
        MaxNumOfParticipants = request.MaxNumOfParticipants
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

  public async Task<GymClassResult> EditGymClassAsync(EditGymClassRequest request)
  {
    var existingGymClassEntity = await _gymClassRepository.GetAsync(x => x.Id == request.Id);
    if (!existingGymClassEntity.Success)
    {
      return new GymClassResult { Success = false, Error = "Could not find any Gym Class with this ID." };
    }

    try
    {
      var gymClassEntity = existingGymClassEntity.Result;

      gymClassEntity!.Image = request.Image;
      gymClassEntity.Title = request.Title;
      gymClassEntity.Description = request.Description;
      gymClassEntity.Date = request.Date;
      gymClassEntity.Location = request.Location;
      gymClassEntity.Instructor = request.Instructor;
      gymClassEntity.MaxNumOfParticipants = request.MaxNumOfParticipants;

      var result = await _gymClassRepository.UpdateAsync(gymClassEntity);
      if (!result.Success)
      {
        return new GymClassResult { Success = false, Error = result.Error };
      }

      return new GymClassResult { Success = true };
    }
    catch (Exception ex)
    {
      return new GymClassResult { Success = false, Error = ex.Message };
    }
  }

  public async Task<GymClassResult> RemoveGymClassAsync(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return new GymClassResult { Success = false, Error = "Gym Class ID cannot be null or empty." };
    }

    try
    {
      var gymClass = await _gymClassRepository.GetAsync(x => x.Id == id);

      if (!gymClass.Success)
      {
        return new GymClassResult { Success = false, Error = "Could not find any Gym Class with this ID." };
      }

      var result = await _gymClassRepository.DeleteAsync(gymClass.Result!);
      if (!result.Success)
      {
        return new GymClassResult { Success = false, Error = result.Error };
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
      Image = entity.Image,
      Title = entity.Title,
      Description = entity.Description,
      Date = entity.Date,
      Location = entity.Location,
      Instructor = entity.Instructor,
      MaxNumOfParticipants = entity.MaxNumOfParticipants
    });

    return new GymClassResult<IEnumerable<GymClass>> { Success = true, Result = gymClasses };
  }
}