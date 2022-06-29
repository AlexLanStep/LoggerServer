namespace DB.Core;

public interface ICarLogConnect
{
  int LoggerID { get; set; }
  int CarID { get; set; }
  DateTime DateTime { get; set; }
}
public class CarLogConnect : ICarLogConnect
{
  public int LoggerID { get; set; }
  public int CarID { get; set; }
  public DateTime DateTime { get; set; }
}

