
namespace WML.Models;

public interface IGetDanNew
{
  SourceList<OneLogger> Loggers { get; set; }
  SourceList<OneCar> Cars { get; set; }
}

public class GetDanNew : IGetDanNew
{
  private IEventAggregator _ea;
  public SourceList<OneLogger> Loggers { get; set; }
  public SourceList<OneCar> Cars { get; set; }
  public GetDanNew(IEventAggregator ea) // , IMyTimer myTimer
  {
    Loggers = new();
    Cars = new();

    _ea = ea;
    _ea.GetEvent<UpdetOutputEvent>().Subscribe(_loaddb);

    _loaddb(null);
  }
  private void _loaddb(IUpdetOutputEvent obj)
  {
    var _isD = DB.Static.InfoDan.GetInfo("loaddb");
    int k=40;
    var __is = true;
    while(k>0 && __is)
    {
      Thread.Sleep(500);
      var __isx = DB.Static.InfoDan.GetInfo("loaddb");
      if (__isx == null)
        continue;
      __is = (int)DB.Static.InfoDan.GetInfo("loaddb")!=2;
    }

    var _xid = (int)DB.Static.InfoDan.GetInfo("loaddb");
    if (  !__is)
    {
//      Task.Run(() =>
      {
       

        var _keyX = DB.Static.InfoDan.LoggerInfos.Keys.ToList<string>();
        _keyX.Sort();
        foreach (var it in _keyX)
        {
          
          var _key = it;
          var v = DB.Static.InfoDan.LoggerInfos[it];
          OneLogger _oneLogger;
          if (v.Length > 0)
          {
            var _elementDates = new List<Element>();
            var _dateTimeSizes = new List<DateTimeSize>();

            foreach (var it0 in v)
            {
//              _elementDates.Add(new Element(DB.Static.GetCarInd.GetName(it0.CarID), it0.DateTimeLast));
//              _dateTimeSizes.Add(new DateTimeSize() { DateTime = it0.DateTimeLast, Size = it0.PercentMem });
              _elementDates.Add(new Element(DB.Static.GetCarInd.GetName(it0.CarID), DateTime.Now));
              _dateTimeSizes.Add(new DateTimeSize() { DateTime = DateTime.Now, Size = it0.PercentMem });
            }


            _oneLogger = new OneLogger()
            {
              LoggerName = _key,
//              DateTimeStart = v[0].SDateTimeFirst,
//              DateTimeEnd = v[0].SDateTimeLast,
              Size = v[0].SPercentMem,
              Path = v[0].Path,
              ElementDates = new ObservableCollection<Element>(_elementDates),
              DateTimeSizes = new ObservableCollection<DateTimeSize>(_dateTimeSizes)
            };
          }
          else
          {
            _oneLogger = new OneLogger()
            {
              LoggerName = _key, DateTimeStart = "", DateTimeEnd = "",
              Size = "-", Path = "----",
              ElementDates = new ObservableCollection<Element>(),
              DateTimeSizes = new ObservableCollection<DateTimeSize>()
            };
          }

          Loggers.Add(_oneLogger);
        }

        _keyX = DB.Static.InfoDan.CarOneInfos.Keys.ToList<string>();
        _keyX.Sort();
        //----------------------------
        foreach (var it in _keyX)
        {
          var _key = it;

          if (_key == "ps218sedlogger1")
          {
            int yyy = 1;
          }

          var v = DB.Static.InfoDan.CarOneInfos[it];
          OneCar _oneCar;

          var _elementDates = new List<Element>();
          var _dateTimeSizes = new List<DateTimeSize>();

          foreach (var it0 in v.CarLogConnects)
          {
            _elementDates.Add(new Element(DB.Static.GetLogInd.GetName(it0.LoggerID), it0.DateTime));
          }

          foreach (var it0 in v.LSLoggerOneInfo)
          {
//            _dateTimeSizes.Add(new DateTimeSize() { DateTime = it0.DateTimeLast, Size = it0.PercentMem });
            _dateTimeSizes.Add(new DateTimeSize() { DateTime = DateTime.Now, Size = it0.PercentMem });
          }



          //          _elementDates.Reverse();
          if (v.CarLogConnects.Count > 0)
          {
            string __log = DB.Static.GetLogInd.GetName(v.CarLogConnects[0].LoggerID);
            var vLog = DB.Static.InfoDan.LoggerInfos[__log];

            string __dateTimeStart = "";
            string __dateTimeEnd = "";
            string __size = "";
            string __path = "";

            if (vLog.Count() > 0)
            {
//              __dateTimeStart = vLog[0].SDateTimeFirst;
//              __dateTimeEnd = vLog[0].SDateTimeLast;
              __dateTimeStart = "-- test 0 ";
              __dateTimeEnd = "-- test 1 ";
              __size = vLog[0].SPercentMem;
              __path = vLog[0].Path;
            }
            _oneCar = new OneCar()
            {
              CarName = _key,
              LoggerName = __log,
              DateTimeStart = __dateTimeStart,
              DateTimeEnd = __dateTimeEnd,
              Size = __size,
              Path = __path,
              ElementDates = new ObservableCollection<Element>(_elementDates),
              DateTimeSizes = new ObservableCollection<DateTimeSize>(_dateTimeSizes)
            };
          }
          else
          {
            _oneCar = new OneCar()
            {
              CarName = _key,
              LoggerName = "",
              DateTimeStart = "",
              DateTimeEnd = "",
              Size = "",
              Path = "--",
              ElementDates = new ObservableCollection<Element>(_elementDates),
              DateTimeSizes = new ObservableCollection<DateTimeSize>(_dateTimeSizes)
            };
          }

          Cars.Add(_oneCar);
        }

        DB.Static.InfoDan.SetInfo("loaddb", 3);
      }
//      );
    }
  }

/*
  private void CreatePeople()
  {
    var log = new SourceList<OneLogger>();

    var _elementDates = new List<Element>()
      {
        new Element("Car0", new DateTime(2022, 1, 1, 1, 0, 0)),
        new Element("Car1", new DateTime(2021, 2, 2, 2, 0, 0)),
        new Element("Car2", new DateTime(2020, 3, 3, 3, 0, 0)),
        new Element("Car0", new DateTime(2019, 11, 11, 1, 0, 0)),
        new Element("Car5", new DateTime(2018, 5, 5, 1, 0, 0))
      };

    var _dateTimeSizes = new List<DateTimeSize>()
      {
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 1, 1, 0, 0), Size=99.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 2, 2, 0, 0), Size=69.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 3, 1, 0, 0), Size=39.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 4, 1, 0, 0), Size=59.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 5, 1, 0, 0), Size=49.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 6, 1, 0, 0), Size=19.2}
      };

    log.Add(new OneLogger()
    {
      LoggerName = "Logger-0",
      DateTimeStart = new DateTime(2020, 01, 01, 1, 0, 0).ToString(),
      DateTimeEnd = new DateTime(2020, 02, 02, 2, 0, 0).ToString(),
      Size = "300.1",
      Path = @"q:\1111\22222\333",
      ElementDates = new ObservableCollection<Element>(_elementDates),
      DateTimeSizes = new ObservableCollection<DateTimeSize>(_dateTimeSizes)
    });


    _elementDates = new List<Element>()
      {
        new Element("Car10", new DateTime(2012, 1, 1, 1, 0, 0)),
        new Element("Car11", new DateTime(2011, 2, 2, 2, 0, 0)),
        new Element("Car12", new DateTime(2010, 3, 3, 3, 0, 0)),
        new Element("Car10", new DateTime(2009,11,11, 1, 0, 0)),
        new Element("Car15", new DateTime(2008, 5, 5, 1, 0, 0)),
      };

    _dateTimeSizes = new List<DateTimeSize>()
      {
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 11, 1, 0, 0), Size=29.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 12, 2, 0, 0), Size=39.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 13, 1, 0, 0), Size=49.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 14, 1, 0, 0), Size=59.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 15, 1, 0, 0), Size=69.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 16, 1, 0, 0), Size=29.2}
      };

    log.Add(new OneLogger()
    {
      LoggerName = "Logger-1",
      DateTimeStart = new DateTime(2010, 01, 01, 1, 0, 0).ToString(),
      DateTimeEnd = new DateTime(2010, 02, 02, 2, 0, 0).ToString(),
      Size = "100.1",
      Path = @"q:\1111\22222\333",
      ElementDates = new ObservableCollection<Element>(_elementDates),
      DateTimeSizes = new ObservableCollection<DateTimeSize>(_dateTimeSizes)
    });

    _elementDates = new List<Element>()
    {
        new Element("Car55", new DateTime(2013, 1, 1, 1, 0, 0)),
        new Element("Car66", new DateTime(2012, 2, 2, 2, 0, 0)),
        new Element("Car77", new DateTime(2009, 3, 3, 3, 0, 0)),
        new Element("Car88", new DateTime(2008,11,11, 1, 0, 0)),
        new Element("Car99", new DateTime(2008, 5, 5, 1, 0, 0)),
    };
    _dateTimeSizes = new List<DateTimeSize>()
      {
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 21, 1, 0, 0), Size=22.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 22, 2, 0, 0), Size=12.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 23, 1, 0, 0), Size=12.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 24, 1, 0, 0), Size=22.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 25, 1, 0, 0), Size=32.2},
        new DateTimeSize(){DateTime = new DateTime(2022, 1, 26, 1, 0, 0), Size=42.2}
      };

    log.Add(new OneLogger()
    {
      LoggerName = "Logger-2",
      DateTimeStart = new DateTime(2012, 01, 01, 1, 0, 0).ToString(),
      DateTimeEnd = new DateTime(2012, 02, 02, 2, 0, 0).ToString(),
      Size = "200.1",
      Path = @"q:\1111\22222\33322222",
      ElementDates = new ObservableCollection<Element>(_elementDates),
      DateTimeSizes = new ObservableCollection<DateTimeSize>(_dateTimeSizes)
    });

    Loggers = log;
  }

  */

}
