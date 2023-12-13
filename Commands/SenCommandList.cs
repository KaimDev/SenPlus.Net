namespace SenPlus.Commands;

using Telegram.Bot.Types;
using static SenPlus.Constants.SenCommandNames;

public static class SenCommandList
{
  public static readonly IEnumerable<BotCommand> Commands = new List<BotCommand>
  {
    new BotCommand { Command = start, Description = "Start the bot" },
    new BotCommand { Command = help,  Description = "Show help"     },
  };
}
