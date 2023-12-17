using System.Resources;

namespace SenPlus.Resources;

public static class BotMessages
{
  public static ResourceManager _ResourceManager;

  static BotMessages() => _ResourceManager = new($"{typeof(BotMessages).Namespace}.{typeof(BotMessages).Name}", typeof(BotMessages).Assembly);

  public static string? GetMessageByKey(string Key) => _ResourceManager.GetString(Key);

  public static string Ok() => _ResourceManager!.GetString("Ok")!;
}