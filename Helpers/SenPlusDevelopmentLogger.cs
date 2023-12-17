namespace SenPlus.Helpers;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SenPlus.Builders;
using Telegram.Bot;
using Telegram.Bot.Types;

using static SenPlus.Resources.BotMessages;
using static SenPlus.Constants.SenMessageNames;
using SenPlus.Interfaces;

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
    LogObject LogObject = new ()
    {
      MessageType = Update.Type.ToString(),
      UserName = Update.Message?.From?.Username,
      ChatId = Update.Message?.Chat?.Id
    };

    if (Update.IsMessageNotEmpty())
    {
      LogObject.Message = Update.Message!.Text!;
      LogObject.Response = Ok();
    }
    else
    {
      LogObject.Response = GetMessageByKey(NotIsCommandOrMessage)!;
    }

    string? LogJson = JsonConvert.SerializeObject(LogObject, Formatting.Indented);

    await BotClient.SendTextMessageAsync(
      chatId: DeveloperChatId,
      text: LogJson);
  }
}