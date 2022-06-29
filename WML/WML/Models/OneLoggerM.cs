
using DB.Static;
using System.Windows.Media.TextFormatting;

namespace WML.Models;

public class OneLoggerM : BindableBase, INotifyPropertyChanged, IOneLogger
{
  #region = Data =
  #region == LoggerName ==
  protected string _loggerName;
  public string LoggerName
  {
    get { return _loggerName; }
    set
    {
      _loggerName = value;
      OnPropertyChanged();
    }
  }

  #endregion

  #region == DateTimeSizes ==
  protected ObservableCollection<LogHistoryProc> _dateTimeSizes;
  public ObservableCollection<LogHistoryProc> DateTimeSizes
  {
    get { return _dateTimeSizes; }
    set { SetProperty(ref _dateTimeSizes, value); }
  }
  #endregion

  #region == DTSend ==
  protected string _dtSend;
  public string DTSend
  {
    get { return _dtSend; }
    set
    {
      _dtSend = value;
      OnPropertyChanged();
    }
  }

  #endregion
  #region == DTSava ==
  protected string _dtSave;
  public string DTSave
  {
    get { return _dtSave; }
    set
    {
      _dtSave = value;
      OnPropertyChanged();
    }
  }

  #endregion
  #region == Size ==
  protected string _size;
  public string Size
  {
    get { return _size; }
    set
    {
      _size = value;
      OnPropertyChanged();
    }
  }

  #endregion
  #region == Path ==
  protected string _path;
  public string Path
  {
    get { return _path; }
    set
    {
      _path = value;
      OnPropertyChanged();
    }
  }

  #endregion
  #region == NameConfig ==
  protected string _nameConfig;
  public string NameConfig
  {
    get { return _nameConfig; }
    set
    {
      _nameConfig = value;
      OnPropertyChanged();
    }
  }

  #endregion

  #region == HistoryCarLogger ==
  protected ObservableCollection<HistoryCar> _historyCar;
  public ObservableCollection<HistoryCar> HistoryCar
  {
    get { return _historyCar; }
    set { SetProperty(ref _historyCar, value); }
  }
  #endregion


  #region == TextFile ==
  protected string[] _textFile;
  public string[] TextFile
  {
    get { return _textFile; }
    set
    {
      _textFile = value;
      OnPropertyChanged();
    }
  }

  #endregion


  #endregion

  #region INotifyPropertyChanged
  public event PropertyChangedEventHandler PropertyChanged;

  protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
  }

  #endregion //INotifyPropertyChanged

  public override string ToString()
  {
    return String.Format("{0}, {1}", LoggerName, Size);
  }








}
