//using DrawGraph.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using DrawGraph.ViewModels;
using DrawGraph.Models;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Threading;

namespace DrawGraph.Views;

public interface IDrawLine
{ 
}

public partial class DrawLine : UserControl, IDrawLine
{
  #region Data
  private IEventAggregator _ea;
  private List<LoggerOneDateSize> _elementDates = new List<LoggerOneDateSize>();
  #endregion
  public DrawLine(IEventAggregator ea)
  {
    _ea = ea;
//    CreatePeople();
    InitializeComponent();
    Loaded += MainWindow_Loaded;
    DataContext = new DrawLineViewModel(ref GridForChart, _ea);
//    _ea.GetEvent<GraphLogInfo>().Publish(_elementDates);
  }
  private void MainWindow_Loaded(object sender, RoutedEventArgs e)
      =>(DataContext as DrawLineViewModel).NewGrahp();

  private void GridForChart_SizeChanged(object sender, SizeChangedEventArgs e)
      =>(DataContext as DrawLineViewModel).NewGrahp();

  private void CreatePeople()
  {
    _elementDates = new List<LoggerOneDateSize>()
      {
        new LoggerOneDateSize(new DateTime(2022, 1, 1, 1, 9, 20), 99.0),
        new LoggerOneDateSize(new DateTime(2022, 1, 2, 2, 8, 30), 90.0),
        new LoggerOneDateSize(new DateTime(2022, 1, 3, 3, 7, 40), 75.2),
        new LoggerOneDateSize(new DateTime(2022, 1, 4, 4, 6, 50), 60.0),
        new LoggerOneDateSize(new DateTime(2022, 1, 5, 5, 5, 40), 31.0),
        new LoggerOneDateSize(new DateTime(2022, 1, 6, 6, 4, 30), 12.0),
        new LoggerOneDateSize(new DateTime(2022, 1, 7, 7, 3, 20), 21.0),
      };
    _elementDates.Reverse();
  }

}

/*
 //    DispatcherTimer timer = new DispatcherTimer();
//    timer.Interval = TimeSpan.FromSeconds(1);
//    timer.Tick += timer_Tick;
//    timer.Start();

//using System.Windows.Shapes;
//using System.Windows.Threading;


  
  void timer_Tick(object sender, EventArgs e)
  {
    _ea.GetEvent<GraphLogInfo>().Publish(_elementDates);
  }
//    var _elementDates = new List<LoggerOneDateSize>()
//      {
//        new LoggerOneDateSize(new DateTime(2022, 1, 1, 1, 9, 20), 99.0),
//        new LoggerOneDateSize(new DateTime(2022, 1, 2, 2, 8, 30), 90.0),
//        new LoggerOneDateSize(new DateTime(2022, 1, 3, 3, 7, 40), 75.2),
//        new LoggerOneDateSize(new DateTime(2022, 1, 4, 4, 6, 50), 60.0),
//        new LoggerOneDateSize(new DateTime(2022, 1, 5, 5, 5, 40), 31.0),
//        new LoggerOneDateSize(new DateTime(2022, 1, 6, 6, 4, 30), 12.0),
//        new LoggerOneDateSize(new DateTime(2022, 1, 7, 7, 3, 20), 21.0),
//      };
//    _elementDates.Reverse();
////    InfoLoggers = new ObservableCollection<LoggerOneDateSize>(_elementDates);

//    _ea.GetEvent<GraphLogInfo>().Publish(_elementDates);
  


 */