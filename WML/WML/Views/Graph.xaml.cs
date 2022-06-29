using DrawGraph.Views;

namespace WML.Views;

public partial class Graph : UserControl
{
  private IContainerExtension _container;
  private IRegionManager _regionManager;
  private IRegion _region;
  private IEventAggregator _ea;
  private DrawLine _drawLine;
  public Graph(IContainerExtension container, IRegionManager regionManager, IEventAggregator ea)
  {
    _ea = ea;
    InitializeComponent();
    _container = container;
    _regionManager = regionManager;
    regionManager.RegisterViewWithRegion("DrawLine", typeof(DrawLine));

    _region = _regionManager.Regions["ContentRegion"];
    _drawLine = _container.Resolve<DrawLine>();
    _region.Add(_drawLine);

  }

  private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
  {

  }
}
