namespace SenPlus.Core;

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class SenPlus
{
  public TelegramBotClient _Bot { get; private init; }

  public Func<ITelegramBotClient, Update, CancellationToken, Task>? _HandleUpdate { get; set; }

  public Func<ITelegramBotClient, Exception, CancellationToken, Task>? _HandlePollingError { get; set; }
  
  public ReceiverOptions? _ReceiverOptions { get; set; }

  public static Dictionary<string, Func<ITelegramBotClient, Update, CancellationToken, Task>>? _Commands { get; set; } = null;

  public SenPlus(TelegramBotClient bot)
  {
    _Bot = bot;
  }

  public SenPlus(
    TelegramBotClient Bot,
    Func<ITelegramBotClient, Update, CancellationToken, Task>? HandleUpdate = null,
    Func<ITelegramBotClient, Exception, CancellationToken, Task>? HandlePollingError = null,
    ReceiverOptions? ReceiverOptions = null,
    Dictionary<string, Func<ITelegramBotClient, Update, CancellationToken, Task>>? Commands = null)
  {
    _Bot = Bot;
    _HandleUpdate = HandleUpdate;
    _HandlePollingError = HandlePollingError;
    _ReceiverOptions = ReceiverOptions;
    _Commands = Commands;
  }

  public async Task StartReceivingAsync()
  {
    using CancellationTokenSource Cts = new();

    _Bot.StartReceiving(
    updateHandler: _HandleUpdate,
    pollingErrorHandler: _HandlePollingError,
    receiverOptions: _ReceiverOptions,
    cancellationToken: Cts.Token
    );

    var me = await _Bot.GetMeAsync();

    Console.WriteLine($"Start listening for @{me.Username}");
    Console.ReadLine();

    // Send cancellation request to stop bot
    Cts.Cancel();
  }
}
