
namespace DB.Static;

public static class InfoDan
{

  public static ConcurrentDictionary<string, LoggerOneInfo[]> LoggerInfos = new();
  public static ConcurrentDictionary<string, CarOneInfo> CarOneInfos = new();
  public static ConcurrentDictionary<string, object> ParamsInfo = new();


  // public static List<CarLogConnect> GetCarLogConnects() => CarLogConnects;
  public static ConcurrentDictionary<string, LoggerOneInfo[]> GetLoggerInfoAll() => LoggerInfos;
  public static LoggerOneInfo[]? GetLoggerInfoName(string name)
          => LoggerInfos.ContainsKey(name) ? LoggerInfos[name] : null;
  public static ConcurrentDictionary<string, CarOneInfo> CarOneInfosAll() => CarOneInfos;
  public static CarOneInfo? GetCarOneInfosName(string name)
          => CarOneInfos.ContainsKey(name) ? CarOneInfos[name] : null;
  public static object? GetInfo(string name) => ParamsInfo.ContainsKey(name) ? ParamsInfo[name] : null;
  public static void SetInfo(string name, object d) => ParamsInfo.AddOrUpdate(name, d, (_, _) => d);
  
}
