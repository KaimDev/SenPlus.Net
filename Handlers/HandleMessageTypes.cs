namespace SenPlus.Handlers;

using SenPlus.Core;

using SenPlus.Helpers;

using Telegram.Bot;
using Telegram.Bot.Types;

using static SenPlus.Constants.SenMessageNames;
using static SenPlus.Resources.BotMessages;

public static class HandleMessageTypes
{
  public static async Task HandleCommandTypeAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
  {
    if (SenPlus._Commands is not null)
    {
      SendTextMessageHelper SendObject = new()
      {
        chatId = Update.Message!.Chat.Id
      };

      try
      {
        var Command = SenPlus._Commands[Update.GetCommandName()];
        await Command(BotClient, Update, CancellationToken);
        return;
      }
      catch (KeyNotFoundException)
      {
        SendObject.text = GetMessageByKey(CommandNotFound)!;
        SendObject.replyToMessageId = Update.Message.MessageId;
      }

      await BotClient.SendTextMesageWithObjectAsync(SendObject, CancellationToken);
    }
  }

  public static async Task HandleMessageTypeAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
  {
    SendTextMessageHelper SendObject = new()
    {
      chatId = Update.Message!.Chat.Id,
      replyToMessageId = Update.Message.MessageId
    };

    if (Update.IsMessageNotEmpty())
    {
      Console.WriteLine($"Received a '{Update.Message.Text}' message in chat {SendObject.chatId}.");

      SendObject.text = "You said:\n" + Update.Message.Text;
    }
    else
    {
      SendObject.text = GetMessageByKey(NotIsCommandOrMessage)!;
    }

    await BotClient.SendTextMesageWithObjectAsync(SendObject, CancellationToken);
  }
}