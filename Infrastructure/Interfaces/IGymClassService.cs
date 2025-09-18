using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IGymClassService
{
  Task<GymClassResult> CreateGymClassAsync(CreateGymClassRequest request);
  Task<GymClassResult<IEnumerable<GymClass>>> GetAllGymClassesAsync();
}
