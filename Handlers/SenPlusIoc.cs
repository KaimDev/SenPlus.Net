namespace SenPlus.Handlers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SenPlus.Builders;
using SenPlus.Interfaces;

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

public static class SenPlusIoc
{
  public static IServiceProvider? ServiceProvider { get; private set; }

  public static void Configure()
  {
    var services = new ServiceCollection();

    // Register Token
    IBotToken Token = GetTokenFromSettings();
    services.AddSingleton(typeof(IBotToken), Token);

    // Register the bot
    var SenPlusBot = NewSenPlusBot(Token);
    services.AddSingleton(typeof(TelegramBotClient), SenPlusBot);
    
    // Register the ReceiverOptions
    services.AddSingleton(new ReceiverOptions
    {
      AllowedUpdates = Array.Empty<UpdateType>()
    });

    ServiceProvider = services.BuildServiceProvider();
  }

  public static SenPlusBuilder AddReceivingOptions(this SenPlusBuilder Builder)
  {
    if (ServiceProvider is null)
      throw new NullReferenceException("ServiceProvider is null");

    Builder._ReceiverOptions = ServiceProvider.GetService<ReceiverOptions>();
    return Builder;
  }

  public static TelegramBotClient GetBot()
  {
    if (ServiceProvider is null)
      throw new NullReferenceException("ServiceProvider is null");

    return ServiceProvider!.GetService<TelegramBotClient>()!;
  }

  private static TelegramBotClient NewSenPlusBot(IBotToken BotToken)
  {
    return new TelegramBotClient(BotToken.Token);
  }

  private static IBotToken GetTokenFromSettings()
  {
    var Config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    return new BotToken(Config.GetSection("ConnectionString").GetSection("Token").Value!);
  }
}
