
namespace DB;

public class StartForVisual
{
  private static ContainerManager? _container;

  public StartForVisual()
  {
//      Task.Run(() => 
    {
      string _path = @"E:\MLserver\LoggerMemory\ProcessCfg.xml";

      _container = ContainerManager.GetInstance();
      var _xml = _container.DbContainer.Resolve<IParsingXml>();
      _xml.Setup(_path);
      _ = _container.DbContainer.Resolve<IConfigDB>();
      _ = _container.DbContainer.Resolve<IContext>();
      var _procDb = _container.DbContainer.Resolve<IProcessingDb>();
      var _dbInfo = _container.DbContainer.Resolve<IDbInfo>();
      _procDb.NewDanXml();
      _dbInfo.AddLoggerCar();

    }
    //);
  }

}
