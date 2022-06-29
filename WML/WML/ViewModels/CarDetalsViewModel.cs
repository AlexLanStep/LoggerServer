using DrawGraph.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using DrawGraph.Models;
using DB.Static;

namespace WML.ViewModels;

public class CarDetalsViewModel : BindableBase
{
  #region ==== - - Dan - - ====
  private string _labelCar = "Ниформация о логерах в Car";
  public string LabelCar
  {
    get { return _labelCar; }
    set { SetProperty(ref _labelCar, value); }
  }

  private string _isInfoSize = "Visible";  // Hidden   Visible
  public string IsInfoSize
  {
    get { return _isInfoSize; }
    set { SetProperty(ref _isInfoSize, value); }
  }

  private SourceList<OneLogger> _loggers;
  public SourceList<OneLogger> Loggers
  {
    get { return _loggers; }
    set { SetProperty(ref _loggers, value); }
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

      if (value == null)
        return;
      CarName = value.CarName;
      
      DateTimeSizes = value.DateTimeSizes;

      //      DB.Static.InfoDan.SetInfo("car", value.CarName);
      //      DB.Static.InfoDan.SetInfo("log", value.LoggerName);


      var _isx = value.DateTimeSizes.Reverse().ToArray();
      IsInfoSize = _isx.Count() > 0 ? "Visible" : "Hidden";
      List<LoggerOneDateSize> _ls = new();
      _ls.AddRange(DateTimeSizes.Select(item => new LoggerOneDateSize(item.DtSize, item.Proc)));

      if (_isx.Count() > 0)
      {
        IsInfoSize = "Visible";
        IsInfoSizeHeight = "Auto";
      }
      else
      {

        IsInfoSize = "Hidden";
        IsInfoSizeHeight = "0";
      }

      _logHisProc.LogHistoryProcs = GetLogHistoryProc.Get(GetLogInd.GetId(value.LoggerName),30);
      _ea.GetEvent<RefreshGraphCar>().Publish(null);
//      _ea.GetEvent<RefreshGraph>().Publish(_ls);
      
    }
  }

  private string _isInfoSizeHeight = "0";
  public string IsInfoSizeHeight
  {
    get { return _isInfoSizeHeight; }
    set { SetProperty(ref _isInfoSizeHeight, value); }
  }

  public string CarName { get; set; }
  #endregion
  private IEventAggregator _ea;
  //  private Chart chart = new LineChart();
  private ILogHistoryProcArray _logHisProc;

  public ObservableCollection<LogHistoryProc> DateTimeSizes { get; set; }

  //private Grid _grid;
  public ObservableCollection<LoggerOneDateSize> _infoLogger;
  public ObservableCollection<LoggerOneDateSize> InfoLoggers
  {
    get => _infoLogger;
    set => SetProperty(ref _infoLogger, value);
  }

  private bool _isWinActiv;
  public bool IsWinActiv
  {
    get => _isWinActiv;
    set => SetProperty(ref _isWinActiv, value);
  }

  public CarDetalsViewModel(IEventAggregator ea, ILogHistoryProcArray logHisProc)
  {
    //_grid = grid;
    _logHisProc = logHisProc;
    _ea = ea;
//    _ea.GetEvent<RefreshGraph>().Subscribe(danInfoLog);

/*    
    SelectedCar = Cars.Items.Count() > 0
      ? Cars.Items.ElementAt(0)
      : SelectedCar = new OneCarM();
*/
//    _ea.GetEvent<GraphLogInfo>().Subscribe(danInfoLog);
  }
/*
  private void danInfoLog(List<LoggerOneDateSize> x)
  {
    InfoLoggers = new ObservableCollection<LoggerOneDateSize>(x);
    NewGrahp(null);
  }
*/
  public void UpdateTablDTS()
  {
    var _carnull =  DB.Static.InfoDan.GetInfo("car");  //, (_xcar, value.LoggerName));
    if(_carnull == null)
    {
      SelectedCar = Cars.Items.Count() > 0
        ? Cars.Items.ElementAt(0)
        : SelectedCar = new OneCarM();
      return;
    }
    string _car = (string) _carnull;

    if (Cars != null)
      SelectedCar = Cars.Items.FirstOrDefault(x => x.CarName == _car);
  }

/*
  public void NewGrahp(DateTimeSize[] dateTimeSizes)
  {
    if (dateTimeSizes == null)
      return;

    InfoLoggers = new ObservableCollection<LoggerOneDateSize>(dateTimeSizes.Reverse().Select(x => new LoggerOneDateSize(x.DateTime, x.Size)).ToList());

    // Удаляем прежний график.

    // Добавляем новую диаграмму на поле контейнера для графиков.
    //    GridForChart.Children.Add(chart.ChartBackground);

    _grid.Children.Clear();
    _grid.Children.Add(chart.ChartBackground);

    // Принудительно обновляем размеры контейнера для графика.
    _grid.UpdateLayout();

    chart.Clear();
    foreach (DateTimeSize x in dateTimeSizes)
      chart.AddValue(x.Size); 
  }
*/
}
