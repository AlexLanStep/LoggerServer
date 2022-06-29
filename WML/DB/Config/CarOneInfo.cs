namespace DB.Config;

/// <summary>
/// Информация по CAR
///   CarLogConnects - список подключений логгеров к CAR
///   LSLoggerOneInfo - информация о последнем подключение логгера
///   SCarLogConnect - производный сласс от CarLogConnects  для вывода 
/// </summary>
public interface ICarOneInfo
{
  string Name { get; set; }
  int Id { get; set; }
  List<SCarLogConnect> CarLogConnects { get; set; }
  List<ILoggerOneInfo> LSLoggerOneInfo { get; set; }
}
public class CarOneInfo: ICarOneInfo
{
  public List<SCarLogConnect> CarLogConnects { get; set; }
  public List<ILoggerOneInfo> LSLoggerOneInfo { get; set; }
  public string Name { get; set; }
  public int Id { get; set; }

  public CarOneInfo()
  {
    LSLoggerOneInfo = new ();
    CarLogConnects = new ();
    Name = "";
    Id = -1;
  }
}
