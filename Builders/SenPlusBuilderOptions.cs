namespace SenPlus.Builders;

using Microsoft.Extensions.DependencyInjection;
using SenPlus.Commands;
using SenPlus.Constants;
using SenPlus.Core;
using SenPlus.Handlers;
using SenPlus.Helpers;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

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
      if (SenPlus._Commands is not null)
      {
        try
        {
          var Command = SenPlus._Commands[Update.GetCommandName()];
          await Command(BotClient, Update, CancellationToken);
        }
        catch (KeyNotFoundException)
        {
          await BotClient.SendTextMessageAsync(
            chatId: Update.Message!.Chat.Id,
            text: "Command not found",
            cancellationToken: CancellationToken);
        }
      }
    }
    else if (Update.IsMessageNotEmpty())
    {
      var chatId = Update.Message!.Chat.Id;

      Console.WriteLine($"Received a '{Update.Message.Text}' message in chat {chatId}.");

      // Echo received message text
      await BotClient.SendTextMessageAsync(
          chatId: chatId,
          text: "You said:\n" + Update.Message.Text,
          cancellationToken: CancellationToken);
    }
    else
    {
      await BotClient.SendTextMessageAsync(
        chatId: Update.ChatMember!.From.Id,
        text: "Not is command or message",
        cancellationToken: CancellationToken);
    }
  }
}
