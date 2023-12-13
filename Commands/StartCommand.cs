namespace SenPlus.Commands;

using Telegram.Bot;
using Telegram.Bot.Types;

public class StartCommand : CommandBase
{
  public override Task ExecuteCommandAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
  {
    return BotClient.SendTextMessageAsync(
      chatId: Update.Message!.Chat.Id,
      text: "Start command",
      cancellationToken: CancellationToken);
  }
}