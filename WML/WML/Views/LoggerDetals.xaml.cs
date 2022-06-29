
namespace WML.Views;
public partial class LoggerDetals : UserControl
{
  private IContainerExtension _container;
  private IRegionManager _regionManager;
  private IRegion _region;
  private NewGraph _Graph;
  private IEventAggregator _ea;
  public LoggerDetals(IContainerExtension container, IRegionManager regionManager, IEventAggregator ea)
  {
    _ea = ea;
    //_nameregion = namePlot;
    InitializeComponent();
    RegionContext.GetObservableContext(this).PropertyChanged += LoggerDetals_PropertyChanged;
    _container = container;
    _regionManager = regionManager;
    regionManager.RegisterViewWithRegion("NewGraph", typeof(NewGraph));

    _region = _regionManager.Regions["ContentRegion"];
    _Graph = _container.Resolve<NewGraph>();
    _region.Add(_Graph);
  }


  private void LoggerDetals_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
  {
    var context = (ObservableObject<object>)sender;

    var selectedPerson = (OneLoggerM)context.Value;
    (DataContext as LoggerDetalsViewModel).SelectedLogger = selectedPerson;
    IOneLogger _ix = (IOneLogger)context.Value;
   _ea.GetEvent<GraphSendVM>().Publish(_ix);

  }
}

/*
 ContentRegionNewGraph
 */