namespace DB.Core;

public interface IProcessingDb
{
  void InicialProccessing();
  void NewDanXml();
  void AddLoggerInfo(Dictionary<string, object> dan);
  void LoadCarLogdt();
}
public class ProcessingDb : Context, IProcessingDb
{
  public ProcessingDb(IConfigDB iconfigDB, IParsingXml xml) : base(iconfigDB, xml)
  {
  }

  public void InicialProccessing()
  {
    if (AllTable().Where(x => x is "cars" or "loggers" or "carlogconnects").Count() != 3)
    {
      DropAllTablesAsync();
      CreateTables();
    }
//    Task.Run(() => LoadCarLogdt());
    LoadCarLogdt();
  }

  public void LoadCarLogdt()
  {

    CarId();
    foreach (var item in _carId)
    {
      int i = item.Value;
      string s = $"select loggerid, datetime from CarLogConnects where carid = {i} order by datetime desc limit 1;";
      var sl = QueryT<LoggerDateTime>(s);
      if (sl != null && sl.Count()>0) 
        _carlogIddatetime.AddOrUpdate(item.Key, sl[0], (k, v)=> sl[0]);
    }
    LoggerId();
    foreach (var item in _loggerId)
    {
      string name = item.Key;
      int i = item.Value;
      string _tlogname = $"t{name}";
      string s = $"select * from {_tlogname};";
      CreateTLogger(_tlogname);

      var sl = QueryT<TLogger>(s);
      if (sl != null)
        _loggerlsdatetime.AddOrUpdate(name, sl, (k, v)=> sl);
    }
  }

  public void NewDanXml()
  {
//    Task _t0 = AddCarAsync(_xml.DanParsingXml.Select(x => x.Item2).Distinct().ToArray());
//    Task _t1 = AddLoggerAsync(_xml.DanParsingXml.Select(x => x.Item1).Distinct().ToArray());
//    Task.WaitAll(_t0, _t1);

    AddCarAsync(_xml.DanParsingXml.Select(x => x.Item2).Distinct().ToArray());
    AddLoggerAsync(_xml.DanParsingXml.Select(x => x.Item1).Distinct().ToArray());

    AddLoggerCarDateTimeAsync();
  }
  public void AddCarAsync(string[] dCar)
  {
    void action(string[] dcar)
    {
      
      var _cars = dcar.Where(y => y.ToLower() != QueryT<Car>("select * from cars;")
                                            .Select(x => x.Name.ToLower())
                                            .ToArray()
                                            .FirstOrDefault(z => z == y.ToLower()))
                        .Select(q => q.ToLower())
                        .ToArray()
                        .Select(x => new Car() { Name = x })
                        .ToList<Car>()
                        .ToArray();
      InsertMultiple<Car>(_cars);
      CarId();
    }
    action(dCar);

//    await Task.Run(() => action(dCar));
  }
  public void AddLoggerAsync(string[] dLog)
  {
    void action(string[] dcar)
    {
      var _logs = dcar.Where(y => y.ToLower() != QueryT<Logger>("select name from loggers;")
                                            .Select(x => x.Name.ToLower())
                                            .ToArray()
                                            .FirstOrDefault(z => z == y.ToLower()))
                        .Select(q => q.ToLower()).ToArray();

      InsertMultiple<Logger>(_logs.Select(x => new Logger() { Name = x, SendDt="" }).ToList<Logger>().ToArray());
      LoggerId();
      NewTablesTLogger();
    }
//    await Task.Run(() => action(dLog));
    action(dLog);
  }
  public void NewTablesTLogger() 
  {
    // Создание таблиц
    var _lsLogger = QueryT<Logger>("select name, loggerid from loggers;");

    if (_lsLogger == null)
      return;

    var lsTabl = AllTable();

    string[] _newNameTLogs = _lsLogger.Select(x => x.Name)
                                      .ToArray()
                                      .Where(y => ("t"+ y.ToLower()) != lsTabl.FirstOrDefault(z => z.ToLower() == ("t"+y.ToLower())))
                                      .Select(q => "t" + q.ToLower())
                                      .ToArray();

    List <Task> _t = new();

    foreach (var item in _newNameTLogs)
      _t.Add(ComExecuteAsync(new string(CreatLoggerMem(item))));

    foreach (var __t in _t)
      Task.WaitAny(__t);
  }

  private int _testDict(string s)
  {
    try
    {
      return _carlogIddatetime[s.ToLower()].LoggerID;
    }
    catch (Exception)
    {
      return -1;
    }
  }
  public void AddLoggerCarDateTimeAsync()
  {
    DateTime _dt = _xml.NewFileSizeDate.Datetime;
//    CarId();
//    LoggerId();
    LoadCarLogdt();
    List<CarLogConnect> _carLogDateTime = QueryT<CarLogConnect>("SELECT * FROM CarLogConnects;");

    List<CarLogConnect> _carLogConnect = new List<CarLogConnect>();

    if (_carLogDateTime.Count == 0)
    {
      _carLogConnect.AddRange(from (string, string) item in _xml.DanParsingXml
                              select new CarLogConnect()
                              {
                                CarID = _carId[item.Item2.ToLower()],
                                LoggerID = _loggerId[item.Item1.ToLower()],
                                DateTime = _dt
                              });
      if (_carLogConnect.Count != 0)
        InsertMultiple<CarLogConnect>(_carLogConnect.ToArray());
      return;
    }
    _carLogConnect.AddRange(from item in _xml.DanParsingXml
                            let __idcar = _carId[item.Item2.ToLower()]
                            let __idlogger = _loggerId[item.Item1.ToLower()]
                            where _testDict(item.Item2) != __idlogger
                            select new CarLogConnect() { CarID = __idcar, LoggerID = __idlogger, DateTime = _dt });

    //_carLogConnect.AddRange(from item in _xml.DanParsingXml
    //                        let __idcar = _carId[item.Item2.ToLower()]
    //                        let __idlogger = _loggerId[item.Item1.ToLower()]
    //                        where _carlogIddatetime[item.Item2.ToLower()].LoggerID != __idlogger
    //                        select new CarLogConnect() { CarID = __idcar, LoggerID = __idlogger, DateTime = _dt });


    if (_carLogConnect.Count != 0)
      InsertMultiple<CarLogConnect>(_carLogConnect.ToArray());
  }
  public void AddLoggerInfo(Dictionary<string, object> dan)
  {
    if (dan == null || dan.Count() < 6)
      return;

    if (!(dan.ContainsKey("name") && dan.ContainsKey("proc") && dan.ContainsKey("car")))
      return;

    string _name = ((string)dan["name"]).ToLower();
    string _tlogger = "t" + _name;
    double _proc = Math.Round( ((double) dan["proc"]), 2);
    string _car =  ((string) dan["car"]).ToLower();
    DateTime _dtSave = ((DateTime)dan["dtsave"]);
    string _path = ((string)dan["path"]);
    string _nameconfig = ((string)dan["nameconfig"]);

    CarId();

    InsertMultiple<TLogger>(new TLogger()
    { 
      Name = _name, 
      CarID = _carId[_car], 
      DTSave = _dtSave, 
      PercentMem = _proc,
      Path = _path,
      NameConfig = _nameconfig
    }, _tlogger);
  }



}

