
namespace DB.Static;
public static class GetLogHistoryProc
{/// <summary>
///  <int, LogHistoryProc>  - индекс Loggera  имстория логгера 
/// </summary>
  static private ConcurrentDictionary<int, LogHistoryProc[]> _loggerHistoryProc = new();

  public static void Add()
  {
    foreach (var it in InfoDan.LoggerInfos)
    {
      LogHistoryProc[] _lsHis = it.Value.Select(x=> new LogHistoryProc(x.DTSave, x.PercentMem, x.Path, x.Name)).ToArray();
      _loggerHistoryProc.AddOrUpdate(GetLogInd.GetId(it.Key), _lsHis, (k, v) => _lsHis);
    }
  }
  public static LogHistoryProc[] Get(int key)
        => _loggerHistoryProc.ContainsKey(key)? _loggerHistoryProc[key]: new LogHistoryProc[0];
  
  public static LogHistoryProc[] Get(int key, int count)
        => _loggerHistoryProc.ContainsKey(key)
            ? _loggerHistoryProc[key].ToList().GetRange(0, Math.Min(_loggerHistoryProc[key].Count(), count)).ToArray()
            : new LogHistoryProc[0]; 

}
