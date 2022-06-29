
using DB.Static;

namespace WML.ViewModels;
public class CarViewModel : BindableBase
{
  private string _labelCar = "Ниформация о Car -> (logger)";
  public string LabelCar
  {
    get { return _labelCar; }
    set { SetProperty(ref _labelCar, value); }
  }

  private SourceList<OneCarM> _cars;
  public SourceList<OneCarM> Cars
  {
    get { return _cars; }
    set { SetProperty(ref _cars, value); }
  }

  private OneCarM _selectedCar;
  public OneCarM SelectedCar
  {
    get { return _selectedCar; }
    set 
    { 
      SetProperty(ref _selectedCar, value);
      DB.Static.InfoDan.SetInfo("car", value.CarName);
      DB.Static.InfoDan.SetInfo("log", value.LoggerName);
    }
  }

  private ILogHistoryProcArray _logHisProc;
  private IEventAggregator _ea;

  public CarViewModel(IEventAggregator ea, ILogHistoryProcArray logHisProc)
  {
    _ea = ea;
    _logHisProc = logHisProc;
    Cars = new();

    var __cars = GetCarInd.GetAllName();

    foreach (var it in __cars)
    {
      var __car = InfoDan.GetCarOneInfosName(it.Item1);
      if (__car == null)
        continue;

      if (__car.LSLoggerOneInfo == null || __car.LSLoggerOneInfo?.Count() == 0)
        continue;

      var __log = __car.LSLoggerOneInfo[0];

      //var _d = InfoDan.GetLoggerInfoName(it.Item1);
      //if (_d.Count() == 0)
      //  continue;


      Cars.Add(new OneCarM()
      {
        CarName = it.Item1,
        LoggerName = __log.Name,
        DTSend = GetLoggerSendDt.GetLoggerDt(GetLogInd.GetId(__log.Name)),
        DTSave = __log.SDTSave,
        Size = __log.SPercentMem,
        Path = __log.Path,
        NameConfig = __log.NameConfig,
        HistoryLogger = new ObservableCollection<HistoryLogger>(GetHistoryLoggerCar.HistoryCar(it.Item2)),
        DateTimeSizes = new ObservableCollection<LogHistoryProc>(GetLogHistoryProc.Get(GetLogInd.GetId(__log.Name), 30)) //, 14
      });
      SelectedCar = Cars.Items.Count() > 0
        ? Cars.Items.ElementAt(0)
        : SelectedCar = new OneCarM();

    }

  }
}


/*    SelectedCar = Cars.Items.Count() > 0
      ? Cars.Items.ElementAt(0)
      : SelectedCar = new OneCar();
*/

