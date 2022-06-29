namespace DB.Core;
public interface IDbInfo
{
  void AddLoggerCar();
}
public class DbInfo : IDbInfo
{
  private IContext _contDb;
  private ConcurrentDictionary<int, List<(int, DateTime)>> _carLogTime = new();

  public DbInfo(IContext contDb)
  {
    _contDb = contDb;
    InfoDan.SetInfo("loaddb", 0);
  }

  #region ===> Add <===
  public void AddLoggerCar()
  {

    InfoDan.SetInfo("loaddb", 1);
    var _logs = _contDb.QueryT<Logger>("SELECT * From Loggers");
    GetLogInd.Add(_logs);
    GetLoggerSendDt.Add(_logs);

    var _cars = _contDb.QueryT<Car>("SELECT * From Cars");
    GetCarInd.Add(_cars);
    var _carLogConnect = _contDb.QueryT<CarLogConnect>("SELECT * From CarLogConnects");

    _addCarLogConnects(new List<CarLogConnect>(_carLogConnect));
    var logName = DB.Static.InfoDan.LoggerInfos.Keys.ToList<string>();
    logName.Sort();
    DB.Static.InfoDan.SetInfo("log", logName[0]);
    
    GetLogHistoryProc.Add();
    GetHistoryLoggerCar.Add(_carLogConnect.ToArray());

    InfoDan.SetInfo("loaddb", 2);
  }

  private void _addCarLogConnects(List<CarLogConnect> dan)
  {
    List<CarLogConnect> CarLogConnects = new();
    _carLogTime.Clear();
    InfoDan.LoggerInfos.Clear();
    TLogger _tlogger= new TLogger();
    int[] _orderCar = dan.Select(x => x.CarID).OrderBy(z=>z).ToArray();
    int[] _orderLog = dan.Select(x => x.LoggerID).OrderBy(z => z).ToArray();

    foreach (var (it, _idt) in from it in _orderCar
                               let _idt = (from c in dan.Where(x => x.CarID == it)
                                              select (c.LoggerID, c.DateTime)).ToList()
                                              select (it, _idt))
        _carLogTime.TryAdd(it, _idt);

    var _logs = _contDb.QueryT<Logger>("SELECT * From Loggers");

    foreach (var it in _logs)
    {
      var __x = _contDb.QueryT<TLogger>($"SELECT * FROM t{it.Name} ORDER BY  dtsave DESC")
                                          .Select(z => new LoggerOneInfo(z, it.Name))
                                          .ToArray();
      InfoDan.LoggerInfos.AddOrUpdate(it.Name, __x, (_, _)=> __x);
    }

    CarLogConnects = _contDb.QueryT<CarLogConnect>("SELECT * FROM CarLogConnects");

      
    var _idCars = CarLogConnects.ToList().DistinctBy(x => x.CarID).Select(y=>y.CarID).ToList(); //.OrderBy(x => x.CarID).Distinct().ToList<int>();
    InfoDan.CarOneInfos.Clear();

    foreach (var it in _idCars)
    {
      CarOneInfo _carOneInfo = new CarOneInfo();

      var _loggerX = _contDb.QueryT<Logger>($"Select * From loggers where loggerid in (SELECT loggerid FROM carlogconnects " +
              $"where CarId={it} and((datetime) IN (SELECT max(datetime) FROM public.carlogconnects where carid ={it})))").ToArray();

      _carOneInfo.Id = it;
      _carOneInfo.Name = GetCarInd.GetName(it);

      var _xxx0 = CarLogConnects.Where(x => x.CarID == it)
                                              .OrderByDescending(z => z.DateTime)
                                              .Select(q => new SCarLogConnect(q)).ToList<SCarLogConnect>();
      if (_xxx0?.Count() > 0)
      {
        int _idlog = -1;
        int _countMax = _xxx0.Count() - 1;
        List<SCarLogConnect> _z = new();
        for (int i = 0; i < _countMax; i++)
        {
          if (_xxx0[i].LoggerID != _xxx0[i + 1].LoggerID)
          {
            _idlog = _xxx0[i].LoggerID;
            _z.Add(new SCarLogConnect(_idlog, _xxx0[i].CarID, _xxx0[i].DateTime));
          }
        }
        _z.Add(new SCarLogConnect(_xxx0[_countMax].LoggerID, _xxx0[_countMax].CarID, _xxx0[_countMax].DateTime));
        _carOneInfo.CarLogConnects = _z;
      }
      else
        _carOneInfo.CarLogConnects = new List<SCarLogConnect>();

      if ((_loggerX?.Count() > 0)
              && InfoDan.LoggerInfos.ContainsKey(_loggerX[0].Name)
              && InfoDan.LoggerInfos[_loggerX[0].Name].Count() > 0)
      {
        var z0 = InfoDan.LoggerInfos[_loggerX[0].Name]
                  .ToList().OrderByDescending(z => z.DTSave).ToList(); // .Distinct()
        int _idCarTest = z0[0].CarID;
        bool _isCar = true;
        Func<LoggerOneInfo, bool> f00 = (z) =>
        {
          _isCar = (z.CarID == _idCarTest) && _isCar;
          return _isCar;
        };
        _carOneInfo.LSLoggerOneInfo = z0.Where(x => f00(x)).ToList<ILoggerOneInfo>();
      }

      InfoDan.CarOneInfos.AddOrUpdate(_carOneInfo.Name, _carOneInfo, (_, _)=> _carOneInfo);
    }

    List<Logger> _danLoggerSendDt = _contDb.QueryT<Logger>("SELECT * FROM loggers");

    GetLoggerSendDt.Add(_danLoggerSendDt);

  }
  #endregion

}
