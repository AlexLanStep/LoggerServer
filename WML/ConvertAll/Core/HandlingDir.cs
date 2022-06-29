
namespace ConvertAll.Core;

public interface IHandlingDir
{  
  ConcurrentDictionary<string, int> WorkDir { get; set; }
  int IsWork { get; set; }
  void Run(DateTime dtStart, DateTime dtEnd, string? path = null);
}

public class HandlingDir: IHandlingDir
{
  public ConcurrentDictionary<string, int> WorkDir { get; set; }
  public int IsWork { get; set; }

  public HandlingDir()
  {
    WorkDir = new ConcurrentDictionary<string, int>();
    IsWork = 0;
  }

  public void Run(DateTime dtStart, DateTime dtEnd, string path="")
  {
    if (path == null || (! Directory.Exists(path)))
      return;

    Regex r = new Regex(@"\d{4}-\d{2}-\d{2}");

    IsWork = 1;
    List<string> dirs = new List<string>();
    foreach (string dir in Directory.GetDirectories(path))
    {
      string _z = r.Match(dir).Value;
      var _z0 = _z==""? new DateTime(1,1,1): DateTime.Parse(_z);
      if (_z0 >= dtStart && _z0 <= dtEnd)
      {
        if (Directory.GetDirectories(dir).Where(x => x.ToLower().Contains("clf")).Count() == 0)
          continue;

        if ( Directory.GetFiles(dir + "\\clf").ToList().Count() == 0)
          continue;

        var _ls = Directory.GetFiles(dir).ToList();
        var __isfiles = Directory.GetFiles(dir).Where(y => y.ToLower().Contains("ml_rt.ini")
                                                || y.ToLower().Contains("ml_rt2.ini")
                                                || y.ToLower().Contains("textlog.txt")).ToArray().Count()==3;
          if (__isfiles)
            dirs.Add(dir);
        }
      }
    dirs.Sort();
    WorkDir = new ConcurrentDictionary<string, int>(dirs.ToDictionary(x => x, x => 0));
    IsWork = 2;
  }
}
