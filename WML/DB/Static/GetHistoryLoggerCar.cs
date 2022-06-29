
using DB.Core;

namespace DB.Static;

public class HistoryCar
{
  public HistoryCar(int carID, string carName, DateTime dateTime)
  {
    CarID = carID;
    CarName = carName;
    DateTime = dateTime;
  }

  public int CarID { get; set; }
  public string CarName { get; set; }
  public DateTime DateTime { get; set; }
}

public class HistoryLogger
{
  public HistoryLogger(int loggerID, string loggerName, DateTime dateTime)
  {
    LoggerID = loggerID;
    LoggerName = loggerName;
    DateTime = dateTime;
  }

  public int LoggerID { get; set; }
  public string LoggerName { get; set; }
  public DateTime DateTime { get; set; } 
}

public static class GetHistoryLoggerCar
{
  static private ConcurrentDictionary<int, HistoryCar[]> _loggerHistoryCar = new();
  static private ConcurrentDictionary<int, HistoryLogger[]> _carHistoryLogger = new();

  static public void Add(CarLogConnect[] d)
  {

    var _cars = d.Select(x => x.CarID).Distinct().ToList();
    var _loggers = d.Select(x => x.LoggerID).Distinct().ToList();
    foreach (var car in _cars)
    {
      HistoryLogger[] loggers = d.Where(x=>x.CarID==car).Select(z=> new HistoryLogger(z.LoggerID, GetLogInd.GetName(z.LoggerID), z.DateTime)).ToArray();
      _carHistoryLogger.AddOrUpdate(car, loggers, (k, v) => loggers);
    }

    foreach (var log in _loggers)
{
      HistoryCar[] cars = d.Where(x => x.LoggerID == log).Select(z => new HistoryCar(z.CarID, GetCarInd.GetName(z.CarID), z.DateTime)).ToArray();
      _loggerHistoryCar.AddOrUpdate(log, cars, (k, v) => cars);

    }

  }

  static public HistoryCar[] HistoryLogger(int i)
  {
    var rez = _loggerHistoryCar.ContainsKey(i) ? _loggerHistoryCar[i] : new HistoryCar[0];
    return rez.Reverse().ToArray();
  } 
  static public HistoryLogger[] HistoryCar(int i)
  {
    var rez = _carHistoryLogger.ContainsKey(i) ? _carHistoryLogger[i] : new HistoryLogger[0];
    return rez.Reverse().ToArray();
  } 
}
