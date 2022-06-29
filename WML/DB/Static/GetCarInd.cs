namespace DB.Static;

public static class GetCarInd  //: GetCarLoggerIndex<Car>
{

  static private ConcurrentDictionary<string, int> _strInt = new();
  static private ConcurrentDictionary<int, string> _intStr = new();
  static public  int GetId(string key) => _strInt.ContainsKey(key) ? _strInt[key] : -1;
  static public string GetName(int key) => _intStr.ContainsKey(key) ? _intStr[key] : "";
  public static void Add(List<Car> dan)
  {
    if (dan == null)
      return;

    foreach (var it in dan)
    {
      _strInt.AddOrUpdate(it.Name, it.CarID, (k, v) => it.CarID);
      _intStr.AddOrUpdate(it.CarID, it.Name, (k, v) => it.Name);
    }
  }
  static public (string, int)[] GetAllName()
  {
    List<(string, int)> _rez = new();
    foreach (var it in _strInt)
      _rez.Add((it.Key, it.Value));
    return _rez.ToArray();
  }
  static public (int, string)[] GetAllId()
  {
    List<(int, string)> _rez = new();
    foreach (var it in _intStr)
      _rez.Add((it.Key, it.Value));
    return _rez.ToArray();
  }
}