
namespace DB.Static;

public static class GetLoggerSendDt
{
  static private ConcurrentDictionary<int, Logger> _logger = new();
  public static void Add(List<Logger> dan)
  {
    if (dan == null)
      return;

    foreach (var it in dan)
      _logger.AddOrUpdate(it.LoggerID, it, (k, v) => it);
  }

  public static Logger? GetLogger(int i) => _logger.ContainsKey(i) ? _logger[i] : null;
  public static string GetLoggerDt(int i) => _logger.ContainsKey(i) ? _logger[i].SendDt : "";


}



