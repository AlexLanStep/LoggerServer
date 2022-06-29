using DBPostgesSQL.TestDan;
using DryIoc;

namespace DBPostgesSQL;

internal class Program
{
  static ContainerManager _container;
  static void DropTable(IContext container) // !!!!!!!!!!!  Удаление таблиц
  {
    Console.WriteLine("  ____ Очиска db  ______");
    container.DropAllTablesAsync();

    //container.DropTablesAsync("cars");
    Thread.Sleep(1000);
    System.Environment.Exit(-1);
  }
  static void TestInsertDan(IProcessingDb iprocDb )
  {
    Dictionary<string, object> _d0 = new Dictionary<string, object>(); //TLogger
    _d0 = new Dictionary<string, object>() {
      {"name", "u02995" }, {"car", "ps81sed" },{"proc",  43.1},{"path", @"E:\111\22\t0.txt"},
      {"datetimefirst", new DateTime(2022, 1, 11, 0, 0, 0)}, {"datetimelast", new DateTime(2022, 1, 11, 0, 0, 10)}};
    iprocDb.AddLoggerInfo(_d0);

    _d0 = new Dictionary<string, object>() {
      {"name", "u02995" }, {"car", "ps81sed" },{"proc",  26.3},{"path", @"E:\111\22\t1.txt"},
      {"datetimefirst", new DateTime(2022, 1, 12, 0, 0, 10)}, {"datetimelast", new DateTime(2022, 1, 12, 0, 0, 12)}};
    iprocDb.AddLoggerInfo(_d0);

    _d0 = new Dictionary<string, object>() {
      {"name", "u02995" }, {"car", "pta02pmot" },{"proc",  1.21},{"path", @"E:\111\22\t2.txt"},
      {"datetimefirst", new DateTime(2022, 1, 13, 0, 0, 10)}, {"datetimelast", new DateTime(2022, 1, 13, 0, 0, 12)}};
    iprocDb.AddLoggerInfo(_d0);

    _d0 = new Dictionary<string, object>() {
      {"name", "u02995" }, {"car", "ps156sed" },{"proc",  29.1},{"path", @"E:\111\22\t3.txt"},
      {"datetimefirst", new DateTime(2022, 1, 14, 0, 0, 10)}, {"datetimelast", new DateTime(2022, 1, 14, 0, 0, 12)}};
    iprocDb.AddLoggerInfo(_d0);

    _d0 = new Dictionary<string, object>() {
      {"name", "u02995" }, {"car", "ps81sed" },{"proc",  32.5},{"path", @"E:\111\22\t4.txt"},
      {"datetimefirst", new DateTime(2022, 1, 15, 0, 0, 10)}, {"datetimelast", new DateTime(2022, 1, 15, 0, 0, 12)}};
    iprocDb.AddLoggerInfo(_d0);



  }

  static void Main(string[] args)
  {
    Console.WriteLine("Hello, World!");



    int iiii = 1;
    //      string _path = @"E:\MLserver\data\#COMMON\Configuration\UMPv0101\UMPv0101_2020-09-10_11-48-53.analysis\Analysis.gla";
    string _path = @"E:\MLserver\LoggerMemory\ProcessCfg.xml";

    _container = ContainerManager.GetInstance();
    var _xml = _container.DbContainer.Resolve<IParsingXml>();
    _xml.Setup(_path);
    var _iconfig = _container.DbContainer.Resolve<IConfigDB>();
    var _context = _container.DbContainer.Resolve<IContext>();
    var _procDb = _container.DbContainer.Resolve<IProcessingDb>();
    var _dbInfo = _container.DbContainer.Resolve<IDbInfo>();
    DropTable(_context);
//    InicialDan _inicialDan = new InicialDan(_container);

    // _procDb.NewDanXml();
    //TestInsertDan(_procDb);
    _dbInfo.AddLoggerCar();

    int ddd = 1;



    // DropTable(_context);  // !!!!!!!!!!!  Удаление таблиц

    //    _context.AddLoggerCarDateTimeAsync();

    //    _context.AddLoggerCarAsync();


    //    List<Car> carss = new List<Car>() { new Car() { Name = "Car11" }, new Car() { Name = "Car2" }, new Car() { Name = "Car3" } };
    //    _context.Add<Car>(carss.ToArray());
    //(string, string)[] loggers = new (string, string)[] { ("logger11", "tlogger11"),
    //                                                      ("logger12", "tlogger12"),
    //                                                      ("logger13", "tlogger13"),
    //                                                      ("logger14", "tlogger14")};
    //_context.AddLogger(loggers);
    int kk = 1;


    //    var _cCar = _container.DbContainer.Resolve<IContext<Car>>();
    //    var _cLog = _container.DbContainer.Resolve<IContext<Logger>>();

    //    List<Car> carss = new List<Car>() { new Car() { Name = "Car11" }, new Car() { Name = "Car2" }, new Car() { Name = "Car3" } };
    //    _cCar.Add(carss.ToArray());
    //    List<Logger> loggers = new List<Logger>() { new Logger() { Name = "Logger11" }, 
    //                                                new Logger() { Name = "Logger2" }, 
    //                                                new Logger() { Name = "Logger3" } };
    //    _cLog.Add(loggers.ToArray());

    //    var _create = _container.DbContainer.Resolve<IDbContext>();
    //    _create.TestMulti();
    //    _create.InsertSingleAuthor<Car>(new Car() { Name = "Car2" });



    //IDbContext _idbContext = _container.DbContainer.Resolve<IDbContext>();
    //IDbRepository _idbRepository = _container.DbContainer.Resolve<IDbRepository>();
    //_idbContext.SetParams("DbNamiLog");
    //_idbContext.CreateTables();
    //_idbRepository.Get("ss").ForEach(t => Console.WriteLine(t));
    //_idbRepository.InicialDb();

    //Console.WriteLine("Hello World!");
  }
}

// See https://aka.ms/new-console-template for more information
