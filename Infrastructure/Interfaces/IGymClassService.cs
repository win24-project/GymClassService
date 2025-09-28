using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IGymClassService
{
  Task<GymClassResult> CreateGymClassAsync(CreateGymClassRequest request);
  Task<GymClassResult> EditGymClassAsync(EditGymClassRequest request);
  Task<GymClassResult> RemoveGymClassAsync(string id);
  Task<GymClassResult<IEnumerable<GymClass>>> GetAllGymClassesAsync();
}