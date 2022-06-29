namespace DB.Static;


public static class GetLogInd  // : GetCarLoggerIndex<Logger>
{
  static private ConcurrentDictionary<string, int> _strInt = new();
  static private ConcurrentDictionary<int, string> _intStr = new();
  static public int GetId(string key) => _strInt.ContainsKey(key) ? _strInt[key] : -1;
  static public string GetName(int key) => _intStr.ContainsKey(key) ? _intStr[key] : "";
  static public (string, int)[] GetAllName()
  {
    List<(string, int)> _rez = new();
    foreach (var it in _strInt)
      _rez.Add((it.Key, it.Value));
    return _rez.ToArray();
  }
  static public (int, string)[] GetAllId()
  {
    List<(int, string) > _rez = new();
    foreach (var it in _intStr)
      _rez.Add((it.Key, it.Value));
    return _rez.ToArray();
  }

  public static void Add(List<Logger> dan)
  {
    if (dan == null)
      return;

    foreach (var it in dan)
    {
      _strInt.AddOrUpdate(it.Name, it.LoggerID, (k, v)=> it.LoggerID);
      _intStr.AddOrUpdate(it.LoggerID, it.Name, (k, v) => it.Name);
    }
  }
  public static int Get() => _strInt.Count();

}