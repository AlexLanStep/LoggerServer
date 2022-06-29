
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace DB.Core;

public class FileSiseDate
{
  public string Path { get; set; }
  public long size { get; set; }
  public DateTime Datetime { get; set; }
}

public interface IParsingXml
{
  void Setup(string path);
  (string, string)[]? DanParsingXml { get; set; }
  FileSiseDate NewFileSizeDate { get; set; }
  Task Save();
}

public class ParsingXml: IParsingXml
{
  #region data
  private string _filejsonNameProcessCfg;
  private FileSiseDate _fileSiseDate = new FileSiseDate() {Path="",  Datetime = new DateTime(1, 1, 1), size = 0 };
  public (string, string)[]? DanParsingXml { get; set; } = null;
  public FileSiseDate NewFileSizeDate { get; set; }
  #endregion

  #region construct
  public ParsingXml() { }
  #endregion

  public async void Setup(string path)
  {
    List<(string, string)> ls = new();
    _filejsonNameProcessCfg = Environment.CurrentDirectory + "\\ProcessCfg.json";
    if (File.Exists(_filejsonNameProcessCfg))
      _fileSiseDate = JsonConvert.DeserializeObject<FileSiseDate>(File.ReadAllText(_filejsonNameProcessCfg));
    else
      File.WriteAllTextAsync(_filejsonNameProcessCfg, JsonConvert.SerializeObject(_fileSiseDate, Formatting.Indented));

    if (!File.Exists(path))
    {
      DanParsingXml = ls.ToArray();
      return;
    }

    System.IO.FileInfo file = new FileInfo(path);

    NewFileSizeDate = new FileSiseDate() { Path=path, Datetime = file.LastWriteTime, size = file.Length };
    if (_fileSiseDate.Datetime == NewFileSizeDate.Datetime 
        && _fileSiseDate.size== NewFileSizeDate.size)
    {
      DanParsingXml = ls.ToArray();
      return;
    }
      

    string[] readText = File.ReadAllLines(path);

    string[] readTexts = readText.Where(s => s.ToLower().Contains("deviceid", StringComparison.CurrentCulture)
                                        || s.ToLower().Contains("\"vin", StringComparison.CurrentCulture))
                                    .Select(x => x.Replace("<", "").Replace(">", "").Replace("\"", "").Replace("/", "").Trim())
                                    .ToArray();

    

    int i = 0;
    while (i < readTexts.Length)
    {
      ls.Add((readTexts[i].Split("=")[1], readTexts[i + 1].Split("=")[2]));
      i += 2;
    }
    DanParsingXml = ls.ToArray();
    await Save();
  }

  public async Task Save()=>
      await File.WriteAllTextAsync(_filejsonNameProcessCfg, JsonConvert.SerializeObject(NewFileSizeDate, Formatting.Indented));
}

