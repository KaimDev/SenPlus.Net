namespace SenPlus.Helpers;

using Telegram.Bot.Types;


public static class TelegramUpdateExtensions
{
  public static bool IsMessageNotEmpty(this Update Update)
  {
    if (Update.Message is not null)
      if (Update.Message.Text is not null)
        if (!string.IsNullOrEmpty(Update.Message.Text))
          return true;

    return false;
  }

  public static bool IsCommand(this Update Update)
  {
    return Update.IsMessageNotEmpty() && Update.Message!.Text!.StartsWith('/');
  }

  public static string GetCommandName(this Update Update)
  {
    return Update.Message!.Text!.Split(' ').First().Substring(0);
  }

  public static string GetCommandArguments(this Update Update)
  {
    return Update.Message!.Text!.Split(' ').Skip(1).FirstOrDefault() ?? string.Empty;
  }
}