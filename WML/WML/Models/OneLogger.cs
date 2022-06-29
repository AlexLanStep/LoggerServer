
namespace WML.Models;
public class OneLogger : BindableBase, INotifyPropertyChanged//, IOneLogger
{
  private string _loggerName;
  public string LoggerName 
  {
    get { return _loggerName; } 
    set 
    { 
      _loggerName = value;
      OnPropertyChanged();
    } 
  }

  private string _dateTimeStart;
  public string DateTimeStart
  {
    get { return _dateTimeStart; }
    set
    {
      _dateTimeStart = value;
      OnPropertyChanged();
    }
  }

  private string _dateTimeEnd;

  public string DateTimeEnd
  {
    get { return _dateTimeEnd; }
    set
    {
      _dateTimeEnd = value;
      OnPropertyChanged();
    }
  }

  private string _size;
  public string Size
  {
    get { return _size; }
    set
    {
      _size = value;
      OnPropertyChanged();
    }
  }

  private string _path;
  public string Path
  {
    get { return _path; }
    set
    {
      _path = value;
      OnPropertyChanged();
    }
  }

  private ObservableCollection<Element> _elementDates;
  public ObservableCollection<Element> ElementDates
  {
    get { return _elementDates; }
    set { SetProperty(ref _elementDates, value); }
  }

  private ObservableCollection<DateTimeSize> _dateTimeSizes;
  public ObservableCollection<DateTimeSize> DateTimeSizes
  {
    get { return _dateTimeSizes; }
    set { SetProperty(ref _dateTimeSizes, value); }
  }

  
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

