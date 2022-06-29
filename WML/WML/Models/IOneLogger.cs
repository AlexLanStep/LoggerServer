
using DB.Static;

namespace WML.Models;

public interface IOneLogger
{
  string LoggerName { get; set; }
  ObservableCollection<LogHistoryProc> DateTimeSizes { get; set; }
}

