namespace DB.Core;

public interface ISCarLogConnect: ICarLogConnect
{
  string SLoggerID { get; set; }
  string SCarID { get; set; }
  string SDateTime { get; set; }
}
public class SCarLogConnect : ISCarLogConnect
{
  public SCarLogConnect(string sloggerID, string scarID, string sdateTime)
  {
    SLoggerID = sloggerID;
    SCarID = scarID;
    SDateTime = sdateTime;
  }
  public SCarLogConnect(int loggerID, int carID, DateTime dateTime)
  {
    LoggerID = loggerID;
    CarID = carID;
    DateTime = dateTime;
    calc();
  }
  public SCarLogConnect(ICarLogConnect sours)
  {
    LoggerID = sours.LoggerID;
    CarID = sours.CarID;
    DateTime = sours.DateTime;
    calc();
  }

  private void calc()
  {
    SLoggerID = LoggerID.ToString();
    SCarID = CarID.ToString();
    SDateTime = DateTime.ToString(Format.DT);
  }

  public string SLoggerID { get; set; }
  public string SCarID { get; set; }
  public string SDateTime { get; set; }
  public int LoggerID { get; set; } = -1;
  public int CarID { get; set; } = -1;
  public DateTime DateTime { get; set; }= new DateTime(1, 1, 1);
}
