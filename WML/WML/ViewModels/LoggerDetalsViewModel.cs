using DB.Static;
using DrawGraph.Models;

namespace WML.ViewModels;

public class LoggerDetalsViewModel : BindableBase, IOneLogger
{
  public IEventAggregator _ea;

  private OneLoggerM _selectedLogger;

  public OneLoggerM SelectedLogger
  {
    get { return _selectedLogger; }
    set 
    { 
      SetProperty(ref _selectedLogger, value);
      if (value == null)
        return;
      LoggerName = value.LoggerName;

      DateTimeSizes = value.DateTimeSizes;

      List<LoggerOneDateSize> _ls = new();
      _ls.AddRange(DateTimeSizes.Select(item => new LoggerOneDateSize(item.DtSize, item.Proc)));

      var _isx = DateTimeSizes.ToArray();
      if (_isx.Count() > 0)
      {
        IsInfoSize = "Visible";
        IsInfoSizeHeight = "Auto";
      }
      else
      {

        IsInfoSize = "Hidden";
        IsInfoSizeHeight = "0";
      }
      _ea.GetEvent<GraphLogInfo>().Publish(_ls);
    }
  }

  private string _isInfoSize = "Visible";  // Hidden   Visible
  public string IsInfoSize
  {
    get { return _isInfoSize; }
    set { SetProperty(ref _isInfoSize, value); }
  }

  private string _isInfoSizeHeight = "0";  
  public string IsInfoSizeHeight
  {
    get { return _isInfoSizeHeight; }
    set { SetProperty(ref _isInfoSizeHeight, value); }
  }

  public string LoggerName { get; set; }
  public ObservableCollection<LogHistoryProc> DateTimeSizes { get; set; }

  public LoggerDetalsViewModel(IEventAggregator ea)
  {
    _ea = ea;

  }

}
