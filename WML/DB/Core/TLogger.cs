namespace DB.Core;

public interface ITLogger
{
  string Name { get; set; }
  int CarID { get; set; }
  public DateTime DTSave { get; set; }
  double PercentMem { get; set; }
  string Path { get; set; }
  string NameConfig { get; set; }
  string[] TextFile { get; set; }
}
public class TLogger : ITLogger
{
  public string Name { get; set; }
  public int CarID { get; set; }
  public DateTime DTSave { get; set; }
  public double PercentMem { get; set; }
  public string Path { get; set; }
  public string NameConfig { get; set; }
  public string[] TextFile { get; set; }

}
