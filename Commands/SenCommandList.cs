using Telegram.Bot.Types;

namespace SenPlus.Commands;

public static class SenCommandList
{
  public static readonly BotCommand[] Commands =
  {
    new BotCommand { Command = "start", Description = "Start the bot"     },
    new BotCommand { Command = "help",  Description = "Show help"         },
    new BotCommand { Command = "echo",  Description = "Echo the message." },
  };
}
