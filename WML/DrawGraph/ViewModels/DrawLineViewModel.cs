using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawGraph.Models;
using System.Windows.Controls;
using System.Windows;
using DrawGraph.Views;


namespace DrawGraph.ViewModels;

public class DrawLineViewModel : BindableBase
{
  #region Dan
  private IEventAggregator _ea;

  private Chart chart = new LineChart();

  private Grid _grid;
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

  private List<LoggerOneDateSize> _loggerInfo;
  private DrawLine _drawLine;
  #endregion


  public DrawLineViewModel(ref Grid grid, IEventAggregator ea)
  {
    _grid = grid;
    _ea = ea;
    _ea.GetEvent<GraphLogInfo>().Subscribe(danInfoLog);
  }

  private void danInfoLog(List<LoggerOneDateSize> x)
  {
    InfoLoggers = new ObservableCollection<LoggerOneDateSize>(x);
    NewGrahp();
  }
  
  public void NewGrahp()
  {
    // Удаляем прежний график.

    // Добавляем новую диаграмму на поле контейнера для графиков.
    //    GridForChart.Children.Add(chart.ChartBackground);
    _grid.Children.Clear();
    _grid.Children.Add(chart.ChartBackground);

    // Принудительно обновляем размеры контейнера для графика.
    _grid.UpdateLayout();
    
    chart.Clear();
    if (_infoLogger != null)
    {
      foreach (LoggerOneDateSize x in _infoLogger.Reverse())
        chart.AddValue(x.DSize);
    }

//    GridForChart = _grid;
  }



}


/*
 
    // Удаляем прежний график.
    //    GridForChart.Children.OfType<Canvas>().ToList().ForEach(p => GridForChart.Children.Remove(p));
    //      _grid.Children.OfType<Canvas>().ToList().ForEach(p => GridForChart.Children.Remove(p));

    //    Chart chart = new LineChart();

    // Добавляем новую диаграмму на поле контейнера для графиков.
    //    GridForChart.Children.Add(chart.ChartBackground);
    _grid.Children.Clear();
    _grid.Children.Add(chart.ChartBackground);

    // Принудительно обновляем размеры контейнера для графика.
    //    GridForChart.UpdateLayout();
    _grid.UpdateLayout();

    // Создаём график.

    chart.Clear();

    chart.AddValue(99.0);
    chart.AddValue(90.0);
    chart.AddValue(75.2);
    chart.AddValue(60.0);
    chart.AddValue(31.0);
    chart.AddValue(12.0);
    chart.AddValue(21.0);

//    GridForChart = _grid;

  
internal class ViewAViewModel : BindableBase
{
public class StrInfoLogger
{
  public StrInfoLogger(InfoLogger sourse)
  {
    Id = sourse.Id.ToString();
    Path = sourse.Path;
    LoggerName = sourse.LoggerName;
    Size = sourse.Size.ToString();
    //        _dateTime = sourse.DateTime.ToString("yyyy-MM-dd HH:mm:ss tt"); // yyyy-MM-dd HH:mm:ss 
    Date = sourse.DateTime.ToString("yyyy-MM-dd"); // yyyy-MM-dd HH:mm:ss 
    Time = sourse.DateTime.ToString("HH:mm:ss tt"); // yyyy-MM-dd HH:mm:ss 
    DateTran = Date;
    TimeTran = Time;

  }

  public string Id { get; set; }
  public string Path { get; set; }
  public string LoggerName { get; set; }
  public string Size { get; set; }
  public string Date { get; set; }
  public string Time { get; set; }
  public string DateTran { get; set; }
  public string TimeTran { get; set; }


}

private string _headerTable = "Информация по логгерам для Сергея !!!";

public string HeaderTable
{
  get { return _headerTable; }
  set { SetProperty(ref _headerTable, value); }
}

public InfoLogger _selectInfoLogger;
public InfoLogger SelectInfoLogger
{
  get => _selectInfoLogger;
  set => SetProperty(ref _selectInfoLogger, value);
}

public ObservableCollection<StrInfoLogger> _infoLogger;
public ObservableCollection<StrInfoLogger> InfoLoggers
{
  get => _infoLogger;
  set => SetProperty(ref _infoLogger, value);
}

private List<StrInfoLogger> _getDanFunc()
{
  List<StrInfoLogger> result = GetDan.GetDanLogger().Values.Select(x => new StrInfoLogger(x)).ToList<StrInfoLogger>();

  return result;
}
public ViewAViewModel()
{
  GetDan _getDan = new GetDan();

  InfoLoggers = new ObservableCollection<StrInfoLogger>(_getDanFunc());


}



}


*/