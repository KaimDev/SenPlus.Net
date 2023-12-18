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

using static SenPlus.Constants.SenMessageNames;
using static SenPlus.Resources.BotMessages;

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
    SendTextMessageHelper SendObject = new()
    {
      chatId = Update.Message!.Chat.Id
    };

    if (Update.IsCommand())
    {
      if (SenPlus._Commands is not null)
      {
        try
        {
          var Command = SenPlus._Commands[Update.GetCommandName()];
          await Command(BotClient, Update, CancellationToken);
          return;
        }
        catch (KeyNotFoundException)
        {
          SendObject.text = GetMessageByKey(CommandNotFound)!;
        }
      }
    }
    else if (Update.IsMessageNotEmpty())
    {
      var chatId = Update.Message!.Chat.Id;

      Console.WriteLine($"Received a '{Update.Message.Text}' message in chat {chatId}.");

      SendObject.text = "You said:\n" + Update.Message.Text;
    }
    else
    {
      SendObject.text = GetMessageByKey(NotIsCommandOrMessage)!;
      SendObject.replyToMessageId = Update.Message!.MessageId;
    }

    await BotClient.SendTextMesageWithObjectAsync(SendObject, CancellationToken);
  }
}
