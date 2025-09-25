namespace Infrastructure.Models;

public class CreateGymClassRequest
{
  public string? Image { get; set; }
  public string Title { get; set; } = null!;
  public string Description { get; set; } = null!;
  public DateTime Date { get; set; }
  public string Location { get; set; } = null!;
}