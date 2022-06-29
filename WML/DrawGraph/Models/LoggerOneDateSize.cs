using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph.Models
{
  public interface ILoggerOneDateSize
  {
    string DateTime { get; set; }
    string Size { get; set; }
    double DSize { get; set; }

  }

  public class LoggerOneDateSize : ILoggerOneDateSize
  {
    public string DateTime { get; set; }
    public string  Size { get; set; }
    public double DSize { get; set; }

    public LoggerOneDateSize(DateTime dt, double size) 
    {
      DateTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
      DSize = Math.Round(size, 2);
      Size = DSize.ToString();
    }
  }
  public interface ILoggerDateSizes
  {
    List<LoggerOneDateSize> LoggerInfo { get; set; }
  }
  public class LoggerDateSizes: ILoggerDateSizes
  {
    public List<LoggerOneDateSize> LoggerInfo { get; set; }
    public LoggerDateSizes() { }

  }

}
