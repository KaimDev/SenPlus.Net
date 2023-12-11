namespace SenPlus.Interfaces;

public interface IBotToken
{
  public string Token { get; }
}

public class BotToken : IBotToken
{
  public string Token { get; }

  public BotToken(string Token)
  {
    this.Token = Token;
  }
}