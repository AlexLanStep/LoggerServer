
namespace DB.Core;
public interface ILoggerDateTime
{
  int LoggerID { get; set; }
  DateTime DateTime { get; set; }
}
public class LoggerDateTime : ILoggerDateTime
{
  public int LoggerID { get; set; }
  public DateTime DateTime { get; set; }
}
