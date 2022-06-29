
using DB.Core;
using DB.Static;
using DryIoc;
using System.Text.RegularExpressions;

namespace ManagerFiles;

public class CalcSendDt
{
  public class SendDtElement
  {
    public int Id { get; set; }
    public string Logger { set; get; }
    public string Path { set; get; }
    public string SendDt { set; get; }
  }


  private readonly ContainerManager _container;
  private readonly string _upload;
  public CalcSendDt(ContainerManager container, string path)
  {
    _container = container;
    _upload = path;
  }

  public void Run()
  {
    var _dirs = Directory.GetDirectories(_upload).ToList();
    if (_dirs.Count == 0)
      return;
    int _count = _dirs.ElementAt(0).Split("\\").Count() - 1;
    var _context = _container.DbContainer.Resolve<IContext>();
    var __logger = _context.QueryT<Logger>("select * from loggers;");
    GetLogInd.Add(__logger);
    List<SendDtElement> _elements = new List<SendDtElement>();
    
    for (int i = 0; i < _dirs.Count; i++)
    {
      var _z = _dirs[i];
      var _logger =_z.Split("\\")[_count].ToLower();
      _elements.Add(new SendDtElement() { Path=_z, Logger = _logger, SendDt="" });
    }

    Func<string, bool> f00 = (s) =>
    {
      try
      {
        return __logger.FirstOrDefault(y => y.Name.ToLower() == s)?.Name == s;
      }
      catch (Exception)
      {
        return false;
      }
    };

    var _newDir = _elements.Where(x => f00(x.Logger.ToLower())).ToArray();
    List<SendDtElement> _newEelements = new ();
    List<LoggerDt> _logDt = new();
    foreach (var _x in _elements)
    {
      string _pathArc = _x.Path + @"\ARCHIVE";
      if (!Directory.Exists(_pathArc))
        continue;

      var _files = Directory.GetFiles(_pathArc);
      if (_files.Length == 0)
        continue;

      var selectedList = from u in _files
                         orderby u descending
                         select u;
      string _filex = selectedList.ToArray()[0];
      _filex = Path.GetFileNameWithoutExtension(_filex);
      Regex r = new Regex(@"\d{8}_\d{6}");
      var _x00 = Regex.Matches(_filex, @"\d{8}_\d{6}_", RegexOptions.None)[1].Value.ToString();
      string _senddt = _x00.Substring(0, 4)+ "-" + _x00.Substring(4, 2)+ "-" + _x00.Substring(6, 2)
        + "_" + _x00.Substring(9, 2)+ ":" + _x00.Substring(11, 2)+ ":" + _x00.Substring(13, 2);
      int _idLogger = GetLogInd.GetId(_x.Logger);
      _newEelements.Add(new SendDtElement() { Logger =_x.Logger.ToLower(), SendDt= _senddt, Id = _idLogger });
      _logDt.Add(new LoggerDt() { LoggerID = _idLogger, SendDt = _senddt });
    }
    _context.UpDateLoggerDt(_logDt.ToArray());
  }
}

/*
 string sql = "UPDATE Categories SET Description = @Description WHERE CategoryID = @CategoryID;";

using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSqlServerW3Schools()))
{    
    var affectedRows = connection.Execute(sql,
    new[]
    {
    new {CategoryID = 1, Description = "Soft drinks, coffees, teas, beers, mixed drinks, and ales"},
    new {CategoryID = 4, Description = "Cheeses and butters etc."}
    }
);
 
 */