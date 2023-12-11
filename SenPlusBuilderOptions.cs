namespace SenPlus.Core;

using Telegram.Bot;
using Telegram.Bot.Types;

public static class SenPlusBuilderOptions
{
  public static SenPlusBuilder UseHandleUpdate(this SenPlusBuilder Builder)
  {
    Builder._HandleUpdate = HandleUpdateAsync;
    return Builder;
  }

  public static SenPlusBuilder UseHandlePollingError(this SenPlusBuilder Builder)
  {
    Builder._HandlePollingError = SenPlusError.HandlePollingErrorAsync;
    return Builder;
  }

  private static async Task HandleUpdateAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
  {
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (Update.Message is not { } message)
      return;
    // Only process text messages
    if (message.Text is not { } messageText)
      return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    // Echo received message text
    Message SentMessage = await BotClient.SendTextMessageAsync(
        chatId: chatId,
        text: "You said:\n" + messageText,
        cancellationToken: CancellationToken);
  }
}
