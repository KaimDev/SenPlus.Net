namespace SenPlus.Core;

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class SenPlusBuilder
{
  public TelegramBotClient _Bot;
  public Func<ITelegramBotClient, Update, CancellationToken, Task>? _HandleUpdate;
  public Func<ITelegramBotClient, Exception, CancellationToken, Task>? _HandlePollingError;
  public ReceiverOptions? _ReceiverOptions;

  public SenPlusBuilder(TelegramBotClient bot)
  {
    _Bot = bot;
  }

  public SenPlus Build()
  {
    return new SenPlus(_Bot, _HandleUpdate, _HandlePollingError, _ReceiverOptions);
  }
}
