using Prism.Mvvm;
using OxyPlot;
using OxyPlot.Series;
using System;
using DB.Static;
using DrawGraph.Models;

namespace WML.ViewModels;

public class NewGraphCarViewModel : BindableBase
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

//  public void LoadDanGraph(IRefreshGraph obj)
  public void LoadDanGraph(List<LoggerOneDateSize> obj)
  {

    LineSeries lineSeries = new LineSeries();

    if (_logHisProc.LogHistoryProcs.Count() > 0)
    {
      this.MyModel.Series.Clear();

      int _id = _logHisProc.LogHistoryProcs.Length - 1;
      for (int i = 0; i < _logHisProc.LogHistoryProcs.Length; i++)
      {
        lineSeries.Points.Add(new DataPoint(i, _logHisProc.LogHistoryProcs[_id - i].Proc));
      }

      this.MyModel.Series.Add(lineSeries);
      this.MyModel.InvalidatePlot(true);

    }

  }

  public NewGraphCarViewModel(IEventAggregator ea, ILogHistoryProcArray logHisProc)
  {
    _ea = ea;
    _logHisProc = logHisProc;
    this.MyModel = new PlotModel { };
    LogDtSizes = new();
    LogDtSizes.AddRange(_logHisProc.LogHistoryProcs);
//    _ea.GetEvent<RefreshGraph>().Subscribe(LoadDanGraph);
    _ea.GetEvent<GraphLogInfoCar>().Subscribe(LoadDanGraph);
    
    LoadDanGraph(null);

  }
}
