
namespace WML.Models;

public interface ILoggerSendDt
{
  public int LoggerId { get; set; }
  public string Name { get; set; }
  public string DTSave { get; set; }
}
public class LoggerSendDt : ILoggerSendDt
{
  public int LoggerId { get; set; }
  public string Name { get; set; }
  public string DTSave { get; set; }
}
