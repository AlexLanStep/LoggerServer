using Prism.Mvvm;
using OxyPlot;
using OxyPlot.Series;
using System;
using DB.Static;

namespace WML.ViewModels;

public class NewGraphViewModel : BindableBase
{
  private IEventAggregator _ea;
  private ILogHistoryProcArray _logHisProc;

  private SourceList<LogHistoryProc> _logDtSizes;
  public SourceList<LogHistoryProc> LogDtSizes
  {
    get { return _logDtSizes; }
    set { SetProperty(ref _logDtSizes, value); }
  }
  public PlotModel MyModel { get; private set; }

  
  public void LoadDanGraph(IRefreshGraph obj)
  {
    
    LineSeries lineSeries  = new LineSeries();

    if(_logHisProc.LogHistoryProcs.Count() > 0)
    {
      this.MyModel.Series.Clear();

      int _id = _logHisProc.LogHistoryProcs.Length-1;
      for (int i = 0; i < _logHisProc.LogHistoryProcs.Length; i++ )
      {
        lineSeries.Points.Add(new DataPoint(i, _logHisProc.LogHistoryProcs[_id-i].Proc));
      }

      this.MyModel.Series.Add(lineSeries);
      this.MyModel.InvalidatePlot(true);

    }

  }

  public NewGraphViewModel(IEventAggregator ea, ILogHistoryProcArray logHisProc)
  {
    _ea = ea;
    _logHisProc = logHisProc;
    this.MyModel = new PlotModel { };
    LogDtSizes = new();
    LogDtSizes.AddRange(_logHisProc.LogHistoryProcs);
    _ea.GetEvent<RefreshGraph>().Subscribe(LoadDanGraph);
    LoadDanGraph(null);

/*
    this.MyModel = new PlotModel {  }; //Title = "Example 1"
    Func<double, double> f0 = (d) =>
    {
      double x = 0.0;
      try
      {
        x = Math.Sin(d) / Math.Cos(d) + 2;
      }
      catch (Exception)
      {
        double _z = Math.Cos(d);
        x = _z == 0 ? 9.0 : 0;
      }
      return Math.Abs(x) > 10 ? 10 : x;
    };
    //    var x = new FunctionSeries(f0, 0, 100, 0.1, "2*cos(x)");
    ////    this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
    //    this.MyModel.Series.Add(x);

    double dx = 0.01;
    LineSeries lineSeries = new LineSeries();
    for (double num = 0; num <= 100 + dx * 0.5; num += dx)
    {
      lineSeries.Points.Add(new DataPoint(num, f0(num)));
    }

    this.MyModel.Series.Add(lineSeries);
*/

  }
}


/*

    private IContainerExtension _container;
    private IRegionManager _regionManager;
    private IRegion _region;
    private IEventAggregator _ea;
    private ILogHistoryProcArray _logHisProc;
    public NewGraph(IEventAggregator ea, ILogHistoryProcArray logHisProc)



  private string _title = "Prism Application";
  public string Title
  {
    get { return _title; }
    set { SetProperty(ref _title, value); }
  }
  public PlotModel MyModel { get; private set; }

  public MainWindowViewModel()
  {
    this.MyModel = new PlotModel { Title = "Example 1" };
    Func<double, double> f0 = (d) =>
    {
      double x = 0.0;
      try
      {
        x = Math.Sin(d)/Math.Cos(d) + 2;
      }
      catch (Exception)
      {
        double _z = Math.Cos(d);
        x = _z==0?9.0:0;
      }
      return Math.Abs(x)>10?10:x;
    };
//    var x = new FunctionSeries(f0, 0, 100, 0.1, "2*cos(x)");
////    this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
//    this.MyModel.Series.Add(x);

    double dx = 0.01;
    LineSeries lineSeries = new LineSeries();
    for (double num = 0; num <= 100 + dx * 0.5; num += dx)
    {
      lineSeries.Points.Add(new DataPoint(num, f0(num)));
    }

    this.MyModel.Series.Add(lineSeries);



  }


 
 */