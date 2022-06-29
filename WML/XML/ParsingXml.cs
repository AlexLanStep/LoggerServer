using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace XML;
public class FileSiseDate
{
  public long size { get; set; }
  public DateTime Datetime { get; set; }
/*  
  public static bool operator ==(FileSiseDate p0, FileSiseDate p1)
    => (p0.size == p1.size) && (p0.Datetime == p1.Datetime);
  public static bool operator !=(FileSiseDate p0, FileSiseDate p1)
    => !((p0.size == p1.size) && (p0.Datetime == p1.Datetime));
*/
}

public class ParsingXml
{
  #region data
  private readonly string filename;
  private readonly string _filejsonNameProcessCfg;
  private readonly bool _isfilejson;
  private FileSiseDate _fileSiseDate;
  #endregion

  #region construct
  public ParsingXml(string path)
  {
    string _dir = Environment.CurrentDirectory;
    _filejsonNameProcessCfg = _dir + "\\ProcessCfg.json";
    _isfilejson = File.Exists(_filejsonNameProcessCfg);
    if (!_isfilejson)
    {
      _fileSiseDate = new FileSiseDate() { Datetime = new DateTime(1, 1, 1), size=0};
      string json = JsonConvert.SerializeObject(_fileSiseDate, Formatting.Indented);
      File.WriteAllTextAsync(_filejsonNameProcessCfg, json);
    }
    else
    {
      var json = File.ReadAllText(_filejsonNameProcessCfg);
      _fileSiseDate = JsonConvert.DeserializeObject<FileSiseDate>(json);
    }

    filename = path;
  }
  #endregion

  public (string, string)[]? TestConfig()
  {
    if (!File.Exists(filename))
    {
      return null;
    }
    FileSiseDate fileSiseDate = new FileSiseDate();
    System.IO.FileInfo file = new System.IO.FileInfo(filename);

    fileSiseDate.Datetime = file.LastWriteTime; // File.GetLastWriteTime(filename);
    fileSiseDate.size = file.Length;
    bool b = _fileSiseDate.Equals(fileSiseDate);
    return null;
  }
  public (string, string)[]? ReadDanConfig()
  {

    string[] readText = File.ReadAllLines(filename);

    string[] readTexts = readText.Where(s => s.ToLower().Contains("deviceid", StringComparison.CurrentCulture)
                                        || s.ToLower().Contains("\"vin", StringComparison.CurrentCulture))
                                    .Select(x=>x.Replace("<", "").Replace(">", "").Replace("\"", "").Replace("/", "").Trim())
                                    .ToArray();

    List<(string, string)> ls = new List<(string, string)>();

    int i = 0;
    while (i < readTexts.Length)
    {
      ls.Add((readTexts[i].Split("=")[1], readTexts[i + 1].Split("=")[2]));
      i = i + 2;
    }

    return ls.ToArray();
  }
}

