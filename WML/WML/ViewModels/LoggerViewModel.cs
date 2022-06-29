
using DB.Static;
using System.Windows.Media.TextFormatting;

namespace WML.ViewModels;

public class LoggerViewModel : BindableBase
{
  #region ==== - - Dan - - ====
  private string _labelLogger = "Ниформация о логерах";
  public string LabelLogger
  {
    get { return _labelLogger; }
    set { SetProperty(ref _labelLogger, value); }
  }

  private SourceList<OneLoggerM> _loggers;
  public SourceList<OneLoggerM> Loggers
  {
    get { return _loggers; }
    set { SetProperty(ref _loggers, value); }
  }

  private OneLoggerM _selectedLogger;
  public OneLoggerM SelectedLogger
  {
    get { return _selectedLogger; }
    set 
    { 
      SetProperty(ref _selectedLogger, value);
      if (value == null)
        return;

      var _xcarXX = Loggers.Items.FirstOrDefault(x => x.LoggerName == value.LoggerName).HistoryCar; //.ElementDates;
      if (_xcarXX.Count == 0)
        return;
      DB.Static.InfoDan.SetInfo("car", _xcarXX[0].CarName);
      DB.Static.InfoDan.SetInfo("log", value.LoggerName);

      _logHisProc.LogHistoryProcs = GetLogHistoryProc.Get(GetLogInd.GetId(value.LoggerName), 30); //, 14
      _ea.GetEvent<RefreshGraph>().Publish(null);
      
    }
  }

  private ILogHistoryProcArray _logHisProc;
  private IEventAggregator _ea;
  #endregion

  public LoggerViewModel(IEventAggregator ea, ILogHistoryProcArray logHisProc)
  {
    _ea = ea;
    _logHisProc = logHisProc;
    Loggers = new SourceList<OneLoggerM>();

    var _logs = GetLogInd.GetAllName();

    foreach (var it in _logs)
    {
      var _d = InfoDan.GetLoggerInfoName(it.Item1);
      if (_d.Count() == 0)
        continue;

      Loggers.Add(new OneLoggerM()
          {
            LoggerName = it.Item1,
            DTSend = GetLoggerSendDt.GetLoggerDt(it.Item2),
            DTSave = _d[0].SDTSave,
            Size = _d[0].SPercentMem,
            Path = _d[0].Path,
            NameConfig = _d[0].NameConfig,
            HistoryCar = new ObservableCollection<HistoryCar>(GetHistoryLoggerCar.HistoryLogger(it.Item2)),
            DateTimeSizes = new ObservableCollection<LogHistoryProc>(GetLogHistoryProc.Get(it.Item2, 30)) //, 14
      });
      SelectedLogger = Loggers.Items.Count() > 0
        ? Loggers.Items.ElementAt(0)
        : SelectedLogger = new OneLoggerM();

    }

    /*
            TextFile = _d[0].TextFile,
            DateTimeSizes = new ObservableCollection<DateTimeSize>(),
            HistoryCar = new ObservableCollection<HistoryCar>()

     */

    //Loggers = _igetDan.Loggers;

    //SelectedLogger = Loggers.Items.Count() > 0 
    //  ? Loggers.Items.ElementAt(0)
    //  : SelectedLogger = new OneLogger();
  }

  public void UpdateTablDTS()
  {
    //var _lognull = DB.Static.InfoDan.GetInfo("log");
    //if (_lognull == null)
    //{
    //  SelectedLogger = Loggers.Items.Count() > 0
    //  ? Loggers.Items.ElementAt(0)
    //  : SelectedLogger = new OneLogger();
    //  return;
    //}
    //string _log = (string)_lognull;

    //SelectedLogger = Loggers.Items.FirstOrDefault(x => x.LoggerName == _log);
  }
}

