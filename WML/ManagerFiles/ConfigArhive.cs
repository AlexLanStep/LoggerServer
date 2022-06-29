
using DB.Core;
using DB.Static;
using DryIoc;
namespace ManagerFiles;

public class ConfigArhive
{
  private readonly string _infoServer;
  private readonly string _serverConfig;
  private readonly string _serverConfigArhive;
  private readonly string _nameConfig;
  private readonly ContainerManager _container;

  public ConfigArhive(ContainerManager container, string path)
  {
    _container = container;
    _infoServer = path;
    _serverConfig = _infoServer + @"\SERVER\CONFIG";
    _serverConfigArhive = _serverConfig + @"\ARHIVE";
    _nameConfig = "ProcessCfg.xml";
  }
  public void Run()
  {
/////////////////////////////////////////////
///  Исправить !  не писать дубликаты
    var _xml = _container.DbContainer.Resolve<IParsingXml>();
    var _iconfig = _container.DbContainer.Resolve<IConfigDB>();
    var _context = _container.DbContainer.Resolve<IContext>();
    var _procDb = _container.DbContainer.Resolve<IProcessingDb>();

    var __car = _context.QueryT<Car>("select * from cars;");
    //var _count = GetCarInd.Get();
    if (__car.Count == 0)
    {//  Первый запуск
      var _dirs = Directory.GetDirectories(_serverConfigArhive).ToList();
      _dirs.Sort();

      foreach (var dir in _dirs)
      {
        string _path = dir+"\\"+ _nameConfig;
        if (File.Exists(_path))
        {
          Console.WriteLine(_path);
          _xml.Setup(_path);
          _procDb.NewDanXml();
        }
      }
    }
    // считываем последнюю конфигурацию
    string _pathConfig = _serverConfig + "\\" + _nameConfig;
    if (File.Exists(_pathConfig))
    {
      Console.WriteLine(_pathConfig);
      _xml.Setup(_pathConfig);
      _procDb.NewDanXml();
    }
    _context.DeleteDublicateCarLogConnect();
  }

}
