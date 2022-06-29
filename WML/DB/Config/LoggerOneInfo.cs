namespace DB.Config;
public interface ILoggerOneInfo : ITLogger
{
  string SDTSave { get; }
  string SPercentMem { get; }
}
public record LoggerOneInfo : ILoggerOneInfo 
{
  public LoggerOneInfo(ITLogger sourse, string name)
  {
    Name = name;
    CarID = sourse.CarID;
    DTSave = sourse.DTSave;
    PercentMem = sourse.PercentMem;
    Path = sourse.Path;
    NameConfig = sourse.NameConfig;
    TextFile = sourse.TextFile;
  }

  public string Name { get; set; }
  public int CarID { get; set; }
  public DateTime DTSave { get; set; } 
  public string SDTSave => DTSave.ToString(Format.DT);  //"yyyy-MM-dd HH:mm:ss"
  public double PercentMem { get; set; }
  public string SPercentMem => Math.Round(PercentMem, 2).ToString();
  public string Path { get; set; }
  public string NameConfig { get; set; }
  public string[] TextFile { get; set; }
}

