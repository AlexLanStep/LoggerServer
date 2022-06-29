
namespace WML.Views;

public partial class CarDetals : UserControl
{
  private IContainerExtension _container;
  private IRegionManager _regionManager;
  private IRegion _region;
  private NewGraphCar _GraphCar;
  private IEventAggregator _ea;

  public CarDetals(IContainerExtension container, IRegionManager regionManager, IEventAggregator ea)
  {
    _ea = ea;
    InitializeComponent();
    RegionContext.GetObservableContext(this).PropertyChanged += LoggerDetals_PropertyChanged;
    _container = container;
    _regionManager = regionManager;
    regionManager.RegisterViewWithRegion("NewGraphCar", typeof(NewGraphCar));

    _region = _regionManager.Regions["ContentRegion"];
    _GraphCar = _container.Resolve<NewGraphCar>();
    _region.Add(_GraphCar);


    //    DataContext = new CarDetalsViewModel(_ea);
    //    this.Loaded += CarDetals_Loaded;
  }

  private void CarDetals_Loaded(object sender, RoutedEventArgs e)
  {
//    (DataContext as CarDetalsViewModel).UpdateTablDTS();
  }

  private void LoggerDetals_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
  {
    var context = (ObservableObject<object>)sender;
    
    var selectedPerson = (OneCarM)context.Value;
    (DataContext as CarDetalsViewModel).SelectedCar = selectedPerson;
    IOneLogger _ix = (IOneLogger)context.Value;
    _ea.GetEvent<GraphSendVM>().Publish(_ix);

  }

  private void GridForChartCar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
  {

  }
}
