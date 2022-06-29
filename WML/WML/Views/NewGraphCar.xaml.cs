using System.Windows.Controls;
using DB.Static;
using OxyPlot.Series;
using OxyPlot;
using System.Windows.Controls;

namespace WML.Views
{
  /// <summary>
  /// Interaction logic for NewGraphCar
  /// </summary>
  public partial class NewGraphCar : UserControl
  {
    private IContainerExtension _container;
    private IRegionManager _regionManager;
    private IRegion _region;
    private IEventAggregator _ea;
    private ILogHistoryProcArray _logHisProc;
    private NewGraphCarViewModel _newGraphCarViewModel;

    public NewGraphCar(IContainerExtension container,
                    IRegionManager regionManager,
                    IEventAggregator ea,
                    ILogHistoryProcArray logHisProc)
    {
      _logHisProc = logHisProc;
      _ea = ea;

      InitializeComponent();
      _newGraphCarViewModel = new NewGraphCarViewModel(_ea, _logHisProc);
      _container = container;
      _regionManager = regionManager;
      _ea.GetEvent<RefreshGraphCar>().Subscribe(updateOutput);

    }
    private void updateOutput(IRefreshGraph obj)
    {
      (DataContext as NewGraphCarViewModel).LogDtSizes.Clear();
      (DataContext as NewGraphCarViewModel).LogDtSizes.AddRange(_logHisProc.LogHistoryProcs);
      Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {
        _dgDTSize.ItemsSource = (DataContext as NewGraphCarViewModel).LogDtSizes.Items;
        _dgDTSize.Items.Refresh();
        (DataContext as NewGraphCarViewModel).LoadDanGraph(null);
        //Plot1.Model[0]        Model.
      }));

    }



  }
}
