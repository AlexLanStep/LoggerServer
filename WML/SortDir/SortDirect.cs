using ManagerFiles;
using Newtonsoft.Json;
using ManagerFiles.Static;
using DryIoc;
using DB.Static;
using DB.Core;

namespace SortDir;

public class SortDirect
{
  static ContainerManager _container;
  async static Task Main(string[] args)
  {
    Console.WriteLine(" Sort dir");

    ///////////////////////////////////////////////
    ///  Должны считать данные из Json файла
    string _pathSortFiles = @"E:\LOGs\CSM_UniCAN2";
    string _jsonConvertNameCar = @"\#COMMON\DLL\SortDirStreamConfig.json";
    _jsonConvertNameCar = _pathSortFiles + _jsonConvertNameCar;
    string _infoServer = @"E:\CSMUNICAN2";
    string _upload = _infoServer + @"\UPLOAD";

    ///////////////////////////////////////////////
    ///  Запускаем поток сортировки
    try
    {
      ConfJson.InicialConvertCar(JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_jsonConvertNameCar)));
    }
    catch (Exception)
    {    }
//    var _file0a = Task<int>.Run(() => { new TransferringFiles(_pathSortFiles).Run(); });


    _container = ContainerManager.GetInstance();
    var _xml = _container.DbContainer.Resolve<IParsingXml>();
//    _xml.Setup(_path);
    var _iconfig = _container.DbContainer.Resolve<IConfigDB>();
    var _context = _container.DbContainer.Resolve<IContext>();
    var _procDb = _container.DbContainer.Resolve<IProcessingDb>();
    var _dbInfo = _container.DbContainer.Resolve<IDbInfo>();

    _procDb.InicialProccessing();

//    var _file1a = Task<int>.Run(() => { new ConfigArhive(_container, _infoServer).Run(); });
//    var _file2a = Task<int>.Run(() => { new CalcSendDt(_container, _upload).Run(); });
//    var _file3a = Task<int>.Run(() => { new LoggerSize(_container, _upload).Run(); });
    new ConfigArhive(_container, _infoServer).Run(); 
    new CalcSendDt(_container, _upload).Run(); 
    new LoggerSize(_container, _upload).Run(); 

    // new LoggerSize(_container, _upload).Run();//
    //    Task.WaitAll(_file1a, _file2a, _file3a);

    _dbInfo.AddLoggerCar();



//    Task.WaitAll(_file0a);
    Thread.Sleep(1000);
    Console.WriteLine("=====  END ======");
    //.WaitAll(_file0a, _file1, _file2, _file3);

  }
}

