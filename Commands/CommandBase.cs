namespace SenPlus.Commands;

using Telegram.Bot;
using Telegram.Bot.Types;

public abstract class CommandBase
{
  public abstract Task ExecuteCommandAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken);
}
