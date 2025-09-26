namespace Infrastructure.Models;

public class GymClass
{
  public string Id { get; set; } = null!;
  public string? Image { get; set; }
  public string Title { get; set; } = null!;
  public string Description { get; set; } = null!;
  public DateTime Date { get; set; }
  public string Location { get; set; } = null!;
  public string Instructor { get; set; } = null!;
  public int MaxNumOfParticipants { get; set; }
}