namespace SenPlus.Core;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

public static class SenPlusIoc
{
  public static IServiceProvider? ServiceProvider { get; private set; }

  public static void Configure()
  {
    var services = new ServiceCollection();

    // Register the bot
    var SenPlusBot = NewSenPlusBot();
    services.AddSingleton<TelegramBotClient>(SenPlusBot);

    // Register the ReceiverOptions
    services.AddSingleton(new ReceiverOptions
    {
      AllowedUpdates = Array.Empty<UpdateType>()
    });

    ServiceProvider = services.BuildServiceProvider();
  }

  public static TelegramBotClient GetBot()
  {
    if (ServiceProvider is null)
      throw new NullReferenceException("ServiceProvider is null");

    return ServiceProvider!.GetService<TelegramBotClient>()!;
  }

  public static SenPlusBuilder UseReceivingOptions(this SenPlusBuilder Builder)
  {
    if (ServiceProvider is null)
      throw new NullReferenceException("ServiceProvider is null");

    Builder._ReceiverOptions = ServiceProvider.GetService<ReceiverOptions>();
    return Builder;
  }

  private static TelegramBotClient NewSenPlusBot()
  {
    var Config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    string Token = Config.GetSection("ConnectionString").GetSection("Token").Value!;
    return new TelegramBotClient(Token);
  }
}
