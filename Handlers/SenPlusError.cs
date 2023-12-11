namespace SenPlus.Handlers;

using Telegram.Bot.Exceptions;
using Telegram.Bot;

public class SenPlusError
{
  public static Task HandlePollingErrorAsync(ITelegramBotClient BotClient, Exception Exception, CancellationToken CancellationToken)
  {
    var ErrorMessage = Exception switch
    {
      ApiRequestException apiRequestException
          => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
      _ => Exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
  }
}
