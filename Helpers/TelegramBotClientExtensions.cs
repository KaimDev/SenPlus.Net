namespace SenPlus.Helpers;

using Telegram.Bot;

public static class TelegramBotClientExtensions
{
  public static async Task SendTextMesageWithObjectAsync(this ITelegramBotClient Bot, SendTextMessageHelper Object, CancellationToken CancellationToken)
  {
    await Bot.SendTextMessageAsync(
      chatId: Object.chatId,
      text: Object.text,
      parseMode: Object.parseMode,
      entities: Object.entities,
      disableWebPagePreview: Object.disableWebPagePreview,
      disableNotification: Object.disableNotification,
      replyToMessageId: Object.replyToMessageId,
      allowSendingWithoutReply: Object.allowSendingWithoutReply,
      replyMarkup: Object.replyMarkup,
      cancellationToken: CancellationToken
    );
  }
}