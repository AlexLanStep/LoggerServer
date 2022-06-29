
using DB.Static;

namespace WML.Models;

public class OneCarM: OneLoggerM
{
  #region == CarName ==
  private string _carName;
  public string CarName
  {
    get { return _carName; }
    set
    {
      _carName = value;
      OnPropertyChanged();
    }
  }

  #endregion
  #region == HistoryLogger ==
  protected ObservableCollection<HistoryLogger> _historyLogger;
  public ObservableCollection<HistoryLogger> HistoryLogger
  {
    get { return _historyLogger; }
    set { SetProperty(ref _historyLogger, value); }
  }

  #endregion


}
