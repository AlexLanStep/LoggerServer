using DB.Core;
using DB.Static;
using DryIoc;

namespace ManagerFiles;

public class LoggerSizeCalc
{
  public readonly string PathFile;
  public Task<int> TRun { get; set; }
  public string LoggerName { get; set; }
  public int LoggerId { get; set; }
  private const double _512 = 512.0;
  private const double _1k = 1024.0;
  private const double _4k = 4.0*_1k;
  private const double _1m = _1k * _1k;
  private const double _1g = _1m * _1k;
  private IContext _context;


  public LoggerSizeCalc(IContext context, string path)
  {
    _context = context;
    PathFile = path;

//    TRun = Task.Run(() => Run());
    Run();
  }

  /// <summary>
  ///   32ГБ - FC: 6663400
  ///  64ГБ - FC: 1332580
  /// </summary>
  /// 
  public int Run()
  {
    Console.WriteLine(PathFile);
    int _rezul = 0;
    var _paths = PathFile.Split("\\");
    int _icount1 = _paths.Count();
    LoggerName = _paths[_icount1 - 2].ToLower();
    string _tLoggerName = "t" + LoggerName;

    List<TLogger> _tlogger = _context.QueryT<TLogger>($"SELECT * FROM {_tLoggerName};");
    int _countTLog = _tlogger.Count();
    bool _isCreateTabl = _countTLog == 0;

    if (LoggerName == "u01311")
    { int ggg = 1; }

    var _files = Directory.GetFiles(PathFile).ToList();
    if (_files.Count == 0)
      return -1;

    var result = _files.Select(x => x.Split("\\")[_icount1].Substring(0, 8)).Distinct().ToList();

    List<TLogger> _lsTLog = new List<TLogger>();
    List<string> __ls = new();

    foreach (var file in result)
      __ls.Add(_files.Where(x => x.Contains(file)).Max().ToLower());

    var dublicateKeys = new HashSet<string>(__ls.Where(x => _tlogger.Exists(y => y.Path.ToLower() == x.ToLower())).Select(x => x.ToLower()));
    __ls.RemoveAll(x => dublicateKeys.Contains(x.ToLower()));

    if(__ls.Count == 0)
      return 0;

    LoggerId = GetLogInd.GetId(LoggerName);

    List<CarLogConnect> _carLogConnect = _context.QueryT<CarLogConnect>
      ($"SELECT carid, loggerid, datetime FROM carlogconnects where loggerid = {LoggerId} ORDER BY Datetime DESC;");

    foreach (var file in __ls)
    {

      DateTime _dt1;
      try
      {
        string _dt = Path.GetFileNameWithoutExtension(file);

        string _senddt = _dt.Substring(0, 4) + "-" + _dt.Substring(4, 2) + "-" + _dt.Substring(6, 2)
                              + " " + _dt.Substring(9, 2) + ":" + _dt.Substring(11, 2) + ":" + _dt.Substring(13, 2);
        _dt1 = DateTime.Parse(_senddt).AddHours(3);
      }
      catch (Exception)
      {
        _dt1 = new DateTime(1, 1, 1);
      }
      int _idCar = _carLogConnect.FirstOrDefault(x => x.DateTime <= _dt1, _carLogConnect.ElementAt(_carLogConnect.Count - 1)).CarID;


      var lines = File.ReadLines(file);

      if(lines?.Count()==0)
      {
        _lsTLog.Add(new TLogger() { Name = LoggerName, 
                                    CarID = _idCar, 
                                    DTSave = _dt1, 
                                    PercentMem = 0.0, 
                                    NameConfig = "----", 
                                    Path = file,
                                    TextFile = new string[0] { }
                                  });
        continue;
      }

      ///   32ГБ - FC: 6 663 400 + 256 = 6 663 656
      ///  64ГБ - FC: 13 325 800 + 256 = 13326056

      double _fc = 0.0;
      try
      {
        double.TryParse(lines.FirstOrDefault(x => x.Contains("FC:")).Split(":")[1].Trim(), out _fc);
      }
      catch (Exception) { }


      double _ps = 0.0;
      try
      {
        double.TryParse(lines.FirstOrDefault(x => x.Contains("PS:")).Split(":")[1].Trim(), out _ps);
        //////////
        ///  подставить значения ------
      }
      catch (Exception) {}

      if (_ps <= 1.0)
      {
        _ps = 0.0;
      } else if(_ps > 6000000.0 && _ps < 8000000.0)
      {
        _ps = 6663656.0;
      }
      else
      {
        _ps = 13326056.0;

      }

      double proc = Math.Abs(_ps)<1 ? 0.0 : Math.Round((_fc  / _ps) * 100, 2);
      proc = proc >99.99 ? 999.99 : proc;


      string _fu = "";
      try
      {
        _fu = lines.FirstOrDefault(x => x.Contains("FU:")).Split(":")[1].Replace("(", "").Replace(")", "").Trim();
      }
      catch (Exception) {}

      _lsTLog.Add(new TLogger() {Name = LoggerName, CarID = _idCar, DTSave = _dt1, 
                                    PercentMem = proc, NameConfig = _fu, Path= file, TextFile= lines.ToArray()});
    }
    var ind = 0;
    if (!_isCreateTabl)
    {
      DateTime __dt = _tlogger.ElementAt(_countTLog - 1).DTSave;

      var _ind = _lsTLog.FirstOrDefault(x => x.DTSave == __dt, null);
      if (_ind == null)
        _context.AddLoggerInfo(_lsTLog.ToArray());
      else
      {
        var __ii = _lsTLog.IndexOf(_ind);
        for (int i = 0; i < __ii; i++)
        {
          _lsTLog.RemoveAt(0);
        }
        _context.AddLoggerInfo(_lsTLog.ToArray());
      }
    }
    else
      _context.AddLoggerInfo(_lsTLog.ToArray());

//    _context.AddLoggerInfo(_lsTLog.ToArray());
    _rezul = 1;
    return _rezul;
  }
}



public class LoggerSize
{
  private readonly string _serverLoggers;
  private readonly ContainerManager _container;

  private readonly string _path;
  public LoggerSize(ContainerManager container, string path)
  {
    _container = container;
    _serverLoggers = path;
  }
  public void Run()
  {
    var _context = _container.DbContainer.Resolve<IContext>();
    var _procDb = _container.DbContainer.Resolve<IProcessingDb>();

    var __loggers = _context.QueryT<Logger>("select * from loggers;");
    if (__loggers.Count == 0)
      return;

    GetLogInd.Add(__loggers);

    var _dirs = Directory.GetDirectories(_serverLoggers).ToList();

    List<LoggerSizeCalc> _sizeCalc = new List<LoggerSizeCalc>();

    var _newdir = _dirs.Where(x => GetLogInd.GetId(Path.GetFileNameWithoutExtension(x).ToLower()) > 0)
      .Select(z => new LoggerSizeCalc(_context, z + @"\SOM")).ToList();


//    foreach (string dir in _newdir)
//      _sizeCalc.Add(new LoggerSizeCalc(dir + @"\SOM"));

  }
}

/*
      string _name = ((string)dan["name"]).ToLower();
      string _tlogger = "t" + _name;
      double _proc = Math.Round(((double)dan["proc"]), 2);
      string _car = ((string)dan["car"]).ToLower();
      DateTime _dtSave = ((DateTime)dan["dtsave"]);
      string _path = ((string)dan["path"]);
      string _nameconfig = ((string)dan["nameconfig"]);
*/
