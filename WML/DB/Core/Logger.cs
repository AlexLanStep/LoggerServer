
namespace DB.Core;

public interface ILoggerDt
{
  int LoggerID { get; set; }
  string SendDt { get; set; }
}
public class LoggerDt : ILoggerDt
{
  public int LoggerID { get; set; }
  public string SendDt { get; set; }
}

public interface ILogger : ILoggerDt
{
  string Name { get; set; }
}
public class Logger : ILogger
{
  public int LoggerID { get; set; }
  public string Name { get; set; }
  public string SendDt { get; set; }
}
