//using Unity;

namespace WML.Views
{
  public partial class Shell : Window
  {
    private IContainerExtension _container;
    private IRegionManager _regionManager;
    private IRegion _region;
    private IEventAggregator _ea;

    private Dictionary<int, object> _mapView = new Dictionary<int, object>();
    private int _indexMap = 0;

    public Shell(IContainerExtension container, IRegionManager regionManager, IEventAggregator ea)
    {
      //   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      //foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
      //{
      //  //FontFamily.Source contains the font family name.
      //  var xxx0 = fontFamily;
      //  var xxx = fontFamily.Source;
      //}

      _ = container.Resolve<IMyTimer>();
      _ = container.Resolve<IGetDanNew>();
      _container = container;
      _regionManager = regionManager;
      regionManager.RegisterViewWithRegion("ContentRegion", typeof(Logger));
      regionManager.RegisterViewWithRegion("LorrerDetalsRegion", typeof(LoggerDetals));
      regionManager.RegisterViewWithRegion("CarDetalsRegion", typeof(CarDetals));

      _ea = ea;
      _ea.GetEvent<LoadDanDb>().Publish();

      InitializeComponent();

      this.Loaded += MainWindow_Loaded;
    }

    #region === --- Load --- ====
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      _mapView.TryAdd(0, _container.Resolve<Logger>());
      _mapView.TryAdd(1, _container.Resolve<Car>());
      _mapView.TryAdd(2, _container.Resolve<Setup>());

      _region = _regionManager.Regions["ContentRegion"];

      _region.Add(_mapView[0]);
      _region.Add(_mapView[1]);
      _region.Add(_mapView[2]);

      _region.Activate(_mapView[_indexMap]);
    }
    #endregion

    #region === --- Logger ---====
    private void btnLogger_MouseEnter(object sender, MouseEventArgs e)
    {
      if (Tg_Btn.IsChecked == true)
        return;

      Popup.PlacementTarget = btnLogger;
      Popup.Placement = PlacementMode.Right;
      Popup.IsOpen = true;
      Header.PopupText.Text = "Logger";
    }
    private void btnLogger_MouseLeave(object sender, MouseEventArgs e)
    {
      Popup.Visibility = Visibility.Collapsed;
      Popup.IsOpen = false;
    }
    private void btnLogger_Click(object sender, RoutedEventArgs e)
    {
      _region.Deactivate(_mapView[_indexMap]);
      _indexMap = 0;
      _region.Activate(_mapView[_indexMap]);
    }
    private void Button_Click_Logger(object sender, RoutedEventArgs e)
    {
      _region.Deactivate(_mapView[_indexMap]);
      _indexMap = 0;
      _region.Activate(_mapView[_indexMap]);
    }
    #endregion

    #region === --- CAR --- ===
    private void btnCar_MouseEnter(object sender, MouseEventArgs e)
    {
      if (Tg_Btn.IsChecked == true)
        return;

      Popup.PlacementTarget = btnCar;
      Popup.Placement = PlacementMode.Right;
      Popup.IsOpen = true;
      Header.PopupText.Text = "Car";
    }
    private void btnCar_MouseLeave(object sender, MouseEventArgs e)
    {
      Popup.Visibility = Visibility.Collapsed;
      Popup.IsOpen = false;
    }
    private void btnCar_Click(object sender, RoutedEventArgs e)
    {
      _region.Deactivate(_mapView[_indexMap]);
      _indexMap = 1;
      _region.Activate(_mapView[_indexMap]);
    }
    private void Button_Click_Car(object sender, RoutedEventArgs e)
    {
      _region.Deactivate(_mapView[_indexMap]);
      _indexMap = 1;
      _region.Activate(_mapView[_indexMap]);
    }
    #endregion

    #region === --- Settup  --- ====
    private void btnSetting_MouseEnter(object sender, MouseEventArgs e)
    {
      if (Tg_Btn.IsChecked == true)
        return;

      Popup.PlacementTarget = btnSetting;
      Popup.Placement = PlacementMode.Right;
      Popup.IsOpen = true;
      Header.PopupText.Text = "Setting";
    }
    private void btnSetting_MouseLeave(object sender, MouseEventArgs e)
    {
      Popup.Visibility = Visibility.Collapsed;
      Popup.IsOpen = false;
    }
    private void btnSetting_Click(object sender, RoutedEventArgs e)
    {
      _region.Deactivate(_mapView[_indexMap]);
      _indexMap = 2;
      _region.Activate(_mapView[_indexMap]);
    }
    private void Button_Click_Setup(object sender, RoutedEventArgs e)
    {
      _region.Deactivate(_mapView[_indexMap]);
      _indexMap = 2;
      _region.Activate(_mapView[_indexMap]);
    }
    #endregion

    #region === --- Button Close | Restore | Minimize  --- ===
    // Start: Button Close | Restore | Minimize 
    private void btnClose_Click(object sender, RoutedEventArgs e) => Close();
    private void btnRestore_Click(object sender, RoutedEventArgs e)
                        => WindowState = WindowState == WindowState.Normal 
                                  ?  WindowState.Maximized
                                  : WindowState.Normal;
    private void btnMinimize_Click(object sender, RoutedEventArgs e) 
                        => WindowState = WindowState.Minimized;
    // End: Button Close | Restore | Minimize
    #endregion
  }
}

