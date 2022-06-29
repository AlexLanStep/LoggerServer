using DB.Static;
using OxyPlot.Series;
using OxyPlot;
using System.Windows.Controls;

namespace WML.Views
{
  public partial class NewGraph : UserControl
  {
    private IContainerExtension _container;
    private IRegionManager _regionManager;
    private IRegion _region;
    private IEventAggregator _ea;
    private ILogHistoryProcArray _logHisProc;
    private NewGraphViewModel _NewGraphViewModel;
    public NewGraph(IContainerExtension container, 
                    IRegionManager regionManager, 
                    IEventAggregator ea, 
                    ILogHistoryProcArray logHisProc)
    {

      _logHisProc = logHisProc;
      _ea = ea;

//    public interface IRefreshGraph { }

//    public class RefreshGraph : PubSubEvent<IRefreshGraph> { }

    InitializeComponent();
      _NewGraphViewModel = new NewGraphViewModel(_ea, _logHisProc);
      _container = container;
      _regionManager = regionManager;
      _ea.GetEvent<RefreshGraph>().Subscribe(updateOutput);

    }

    private void updateOutput(IRefreshGraph obj)
    {
      (DataContext as NewGraphViewModel).LogDtSizes.Clear();
      (DataContext as NewGraphViewModel).LogDtSizes.AddRange(_logHisProc.LogHistoryProcs);
      Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {
                _dgDTSize.ItemsSource = (DataContext as NewGraphViewModel).LogDtSizes.Items;
                _dgDTSize.Items.Refresh();
               (DataContext as NewGraphViewModel).LoadDanGraph(null);
        //Plot1.Model[0]        Model.
      }));

    }
  }
}

/*
 
 LineSeries lineSeries  = new LineSeries();

    if(_logHisProc.LogHistoryProcs.Count() > 0)
    {

      for (int i = 0; i < _logHisProc.LogHistoryProcs.Length; i++ )
      {
        lineSeries.Points.Add(new DataPoint(i, _logHisProc.LogHistoryProcs[i].Proc));
      }

      this.MyModel.Series.Add(lineSeries);

    }
  
_ea.GetEvent<UpdetOutputEvent>().Subscribe(updateOutput);

InitializeComponent();
Loaded += Logger_Loaded;
  }

  private void Logger_Loaded(object sender, System.Windows.RoutedEventArgs e)
{
  (DataContext as LoggerViewModel).UpdateTablDTS();
}

private void updateOutput(IUpdetOutputEvent obj)
{
  Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {
    _listOfLogger.ItemsSource = (DataContext as LoggerViewModel).Loggers.Items;
    _listOfLogger.Items.Refresh();
  }));

}
*/