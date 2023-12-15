namespace SenPlus.Helpers;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SenPlus.Builders;
using Telegram.Bot;
using Telegram.Bot.Types;

public static class SenPlusDevelopmentLogger
{
  private static string DeveloperChatId = String.Empty;

  public static SenPlusBuilder AddDevelopmentLogger(this SenPlusBuilder Builder)
  {
    GetDeveloperChatId();
    Builder._HandleUpdate += HandleUpdateAsync;

    // Send Message to Developer Chat when the bot starts
    Builder._Bot.SendTextMessageAsync(
      chatId: DeveloperChatId,
      text: "Bot Started");

    return Builder;
  }

  // Get the developer chat id from the appsettings.json file
  private static void GetDeveloperChatId()
  {
    var Configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();

    DeveloperChatId = Configuration.GetSection("Develoment").GetSection("DeveloperChatId").Value!;
  }

  private static async Task HandleUpdateAsync(ITelegramBotClient BotClient, Update Update, CancellationToken CancellationToken)
  {
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (Update.Message is not { } Message)
      return;
    // Only process text messages
    if (Message.Text is not { } MessageText)
      return;

    // New String Log to Json
    var LogObject = new
    {
      MessageType = Update.Type,
      Message     = MessageText,
      UserName    = Message.From?.Username,
      ChatId      = Message.Chat.Id
    };

    string? LogJson = JsonConvert.SerializeObject(LogObject, Formatting.Indented);
    
    // Echo received message text
    await BotClient.SendTextMessageAsync(
      chatId: DeveloperChatId,
      text: LogJson,
      cancellationToken: CancellationToken);
  }
}