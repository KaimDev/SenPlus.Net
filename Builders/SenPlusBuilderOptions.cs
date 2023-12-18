namespace SenPlus.Builders;

using Microsoft.Extensions.DependencyInjection;

using SenPlus.Commands;

using SenPlus.Constants;

using SenPlus.Handlers;

using SenPlus.Helpers;

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

using static SenPlus.Handlers.HandleMessageTypes;

public static class SenPlusBuilderOptions
{
  public static SenPlusBuilder AddHandleUpdate(this SenPlusBuilder Builder)
  {
    Builder._HandleUpdate = HandleUpdateAsync;
    return Builder;
  }

  public static SenPlusBuilder AddHandlePollingError(this SenPlusBuilder Builder)
  {
    Builder._HandlePollingError = SenPlusError.HandlePollingErrorAsync;
    return Builder;
  }

  public static SenPlusBuilder AddReceivingOptions(this SenPlusBuilder Builder)
  {
    if (SenPlusIoc.ServiceProvider is null)
      throw new NullReferenceException("ServiceProvider is null");

    Builder._ReceiverOptions = SenPlusIoc.ServiceProvider.GetService<ReceiverOptions>();
    return Builder;
  }

  public static SenPlusBuilder AddCommandList(this SenPlusBuilder Builder)
  {
    Builder._Bot.SetMyCommandsAsync(SenCommandList.Commands);
    return Builder;
  }

  public static SenPlusBuilder AddCommandMethods(this SenPlusBuilder Builder)
  {
    Builder._Commands = new()
    {
      { SenCommandNames.start, new StartCommand().ExecuteCommandAsync },
      { SenCommandNames.help,  new HelpCommand().ExecuteCommandAsync  }
    };

    return Builder;
  }

  private static async Task HandleUpdateAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
  {
    if (Update.IsCommand())
    {
      await HandleCommandTypeAsync(BotClient, Update, CancellationToken);
    }
    else
    {
      await HandleMessageTypeAsync(BotClient, Update, CancellationToken);
    }
  }
}
