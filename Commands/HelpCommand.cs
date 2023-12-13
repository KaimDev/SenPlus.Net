using Telegram.Bot;
using Telegram.Bot.Types;

namespace SenPlus.Commands;

public class HelpCommand : CommandBase
{
  public override async Task ExecuteCommandAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
  {
    await BotClient.SendTextMessageAsync(
      chatId: Update.Message!.Chat.Id,
      text: "Help command",
      cancellationToken: CancellationToken);
  }
}
