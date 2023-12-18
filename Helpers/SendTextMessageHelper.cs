namespace SenPlus.Helpers;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class SendTextMessageHelper
{
  public long? chatId { get; set; } = null;

  public string? text { get; set; } = null;

  public int? messageThreadId { get; set; } = null;

  public ParseMode? parseMode { get; set; } = null;

  public List<MessageEntity>? entities { get; set; } = null;

  public bool? disableWebPagePreview { get; set; } = null;

  public bool? disableNotification { get; set; } = null;

  public bool? protectContent { get; set; } = null;
  
  public int? replyToMessageId { get; set; } = null;

  public bool? allowSendingWithoutReply { get; set; } = null;

  public IReplyMarkup? replyMarkup { get; set; } = null;
}