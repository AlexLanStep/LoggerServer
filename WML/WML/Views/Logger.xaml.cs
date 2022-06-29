

namespace WML.Views;

public partial class Logger : UserControl
{
  private IEventAggregator _ea;
  public Logger(IRegionManager regionManager, IEventAggregator ea)
  {
    _ea = ea;
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
    _listOfLogger.ItemsSource =( DataContext as LoggerViewModel).Loggers.Items;
    _listOfLogger.Items.Refresh();
    }));

  }

}
