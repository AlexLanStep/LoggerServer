
namespace ManagerFiles;

public class TransferringFiles
{

  private string _path;
  public TransferringFiles(string path)
  {
    _path = path;
  }

  private (string, string, string, string, string, string[]) fBasaCsv(string path)
  {

    var _name_ext = Path.GetFileName(path);
    var __names = Path.GetFileNameWithoutExtension(path).Split("_(");
    var __nameCar = __names[0];
    var _nameCar = __nameCar;
    if (ConfJson.ConvertCar.ContainsKey(__nameCar))
    {
      _nameCar = ConfJson.ConvertCar[__nameCar];
      _name_ext = _name_ext.Replace(__nameCar, _nameCar);
    }

    var _nameLogger = __names[1].Split("-")[0];
    var _date1 = __names[2].TrimEnd(')').Split("_")[0];
    return (path, _name_ext, _nameCar, _nameLogger, _date1, __names);
  }

  private (string, string, string, string, string) fCsv(string path)
  {
    var x = fBasaCsv(path);
    return (x.Item1, x.Item2, x.Item3, x.Item4, x.Item5);
  }

  private (string, string, string, string, string, string) fdat(string path)
  {
    var x = fBasaCsv(path);
    return (x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6[3].Split(")_")[1]);

  }

  private void sortDatAsc((string, string, string, string, string, string)[] dat, string name)
  {
    int k0 = dat.Length;
    foreach (var f in dat)
    {
      string _path0 = _path + "\\" + f.Item3 + $"\\{name}\\" + f.Item6 + "\\" + f.Item5;
      if (!Directory.Exists(_path0))
        Directory.CreateDirectory(_path0);
      File.Move(f.Item1, _path0 + "\\" + f.Item2, true);
      Console.WriteLine($" Осталось DAT :{k0} -> {f.Item2}");
      k0--;
    }

  }

  public int Run()
  {
    var _lsfiles = Directory.GetFiles(_path);
    var l0 = Path.GetExtension(_lsfiles.ElementAt(0));
    var _lsDat = _lsfiles.Where(x => Path.GetExtension(x).ToLower() == ".dat").Select(x=>fdat(x)). ToArray();
    var _lsCsv = _lsfiles.Where(x => Path.GetExtension(x).ToLower() == ".csv").Select(x => fCsv(x)).ToArray();
    var _lsAsc = _lsfiles.Where(x => Path.GetExtension(x).ToLower() == ".asc").Select(x => fdat(x)).ToArray();

    var twait0 = Task.Run(() => { sortDatAsc(_lsDat, "DAT"); });
    var twait1 = Task.Run(() => { sortDatAsc(_lsAsc, "ASC"); });

    var twait2 = Task.Run(() =>
    {
      int k0 = _lsCsv.Length;
      foreach (var f in _lsCsv)
      {
        string _path0 = _path + "\\" + f.Item3 + "\\CSV\\" + f.Item5;
        if (!Directory.Exists(_path0))
          Directory.CreateDirectory(_path0);
        File.Move(f.Item1, _path0 + "\\" + f.Item2, true);
        Console.WriteLine($" Осталось CSV :{k0} -> {f.Item2}");
        k0--;
      }
    });
    Task.WaitAll(twait0, twait1, twait2);

    return 2;
  }
}
