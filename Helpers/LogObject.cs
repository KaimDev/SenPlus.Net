namespace SenPlus.Interfaces;

public class LogObject
{
  public string? MessageType { get; set; }
  
  public string? Message { get; set; }

  public string? Response { get; set; }

  public string? UserName { get; set; }

  public long? ChatId { get; set; }
}