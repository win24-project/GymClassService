namespace Infrastructure.Models;

public class GymClassResult
{
  public bool Success { get; set; }
  public string? Error { get; set; }
}

public class GymClassResult<T> : GymClassResult
{
  public T? Result { get; set; }
}