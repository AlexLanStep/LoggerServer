//  https://riptutorial.com/dapper/learn/100005/insert-data

namespace DB.Core;
public interface IContext
{
  void Add<T>(object dan);
  void AddLoggerInfo(TLogger[] danTLog);
  void UpDateLoggerDt(LoggerDt[] table);
  void DropAllTablesAsync();
  Task DropAllTablesAsync(string[] s);
  Task DropTablesAsync(string s);
  void DeleteDublicateCarLogConnect();
  List<T> QueryT<T>(string command);
  bool IsTable(string st);
  void CreateTLogger(string _tloggerName);
}

public class Context : IContext
{
  #region == Data ==
  #region ==- InicialDan -==
  protected IConfigDB _iconfigDB;
  protected IParsingXml _xml;
  private string _connectDBparams;

  protected Dictionary<string, Func<string>> AllSetTables { get; set; }
  protected ConcurrentDictionary<string, int> _carId = new();
  protected ConcurrentDictionary<string, int> _loggerId = new();
  protected ConcurrentDictionary<string, LoggerDateTime> _carlogIddatetime = new();
  protected ConcurrentDictionary<string, List<TLogger>> _loggerlsdatetime = new();
  #endregion

  #region  ==- Create Tables -==
  private string CreatCars() => @"CREATE TABLE Cars( CarId SERIAL PRIMARY KEY, Name  VARCHAR(20)  NOT NULL UNIQUE );";
  private string CreatLoggers() => @"CREATE TABLE Loggers( LoggerId SERIAL PRIMARY KEY, 
                                                            Name  VARCHAR(20)  NOT NULL UNIQUE,
                                                            SendDt TEXT);";
  private string CreatCarLoggersConnect() => @"CREATE TABLE CarLogConnects(  
                  CarID         INTEGER     REFERENCES Cars (CarID) ON DELETE NO ACTION ON UPDATE NO ACTION NOT NULL,
                  LoggerID      INTEGER     REFERENCES Loggers (LoggerID) ON DELETE NO ACTION ON UPDATE NO ACTION NOT NULL,
                  DateTime      TIMESTAMP   NOT NULL);";
  protected string CreatLoggerMem(string s) => $@"CREATE TABLE {s}( 
                  {s}ID SERIAL PRIMARY KEY, 
                  CarID       INTEGER   REFERENCES Cars (CarID) ON DELETE NO ACTION ON UPDATE NO ACTION NOT NULL,
                  DTSave      TIMESTAMP NOT NULL, 
                  NameConfig  TEXT, 
                  Path        TEXT,
                  PercentMem  NUMERIC(5, 2) NOT NULL,
                  TextFile    TEXT[]);";
  private string DropTable(string s) => $"DROP TABLE IF exists {s} cascade;";

  #endregion

  #endregion

  #region   Constructor
  public Context(IConfigDB iconfigDB, IParsingXml xml)
  { 
    _iconfigDB = iconfigDB;
    _xml = xml;
    _connectDBparams = _iconfigDB.ConnectDBparams;
    AllSetTables = new Dictionary<string, Func<string>>
    {
      {"Cars", CreatCars},
      {"Loggers", CreatLoggers},
      {"CarLogConnects", CreatCarLoggersConnect},
    };

  }
  #endregion

  #region ==- Create Table -==
  public void CreateTables()
  {
    var lsTabl = AllTable();

    foreach (var table in AllSetTables.Keys.Where(x => lsTabl.FirstOrDefault(z => z.ToLower() == x.ToLower()) == null))
      ComExecute(AllSetTables[table].Invoke());
  }
  #endregion

  #region ===- ADD -==
  public void Add<T>(object dan) => InsertMultiple<T>(dan);

  public void AddLoggerInfo(TLogger[] danTLog)
  {
    if (danTLog == null || danTLog.Count() < 0)
      return;

    InsertMultiple<TLogger>(danTLog, "t"+danTLog[0].Name.ToLower());
  }

  #endregion

  #region ===- Index -==
  public List<T> IndexFieldCarLog<T>()
  {
    string sqlQuery = typeof(T).Name.ToLower() switch
    {
      "car" => "SELECT Name, CarID from cars;",
      "logger" => "SELECT Name, LoggerID, SendDt from loggers;",
      _ => ""
    };
    return string.IsNullOrEmpty(sqlQuery) ? new List<T>() : QueryT<T>(sqlQuery);
  }
  protected void CarId()      
  {
    foreach (var item in IndexFieldCarLog<Car>())
      _carId.AddOrUpdate(item.Name, item.CarID, (k, v)=> item.CarID);
  }
  protected void LoggerId()    
  { 
    foreach (Logger item in IndexFieldCarLog<Logger>()) 
      _loggerId.AddOrUpdate(item.Name, item.LoggerID, (k, v)=> item.LoggerID); 
  }
  #endregion

  #region ===-- ReadDb --===
  public List<T> QueryT<T>(string command)
  {
    using (var db = new NpgsqlConnection(_connectDBparams))
    {
      return db.Query<T>(command).ToList();
    }
  }
  #endregion

  public async void CreateTLogger(string _tloggerName)
  {
    if (IsTable(_tloggerName))
      return;

     ComExecute(CreatLoggerMem(_tloggerName));
  }

  #region ===-- Insert  --==
  protected void InsertMultiple<T>(object table, string tname = "")
  {
    string _nameClass = typeof(T).Name.ToLower();
    string sqlQuery = _nameClass switch
    {
      "car" => $"INSERT INTO {_nameClass}s (Name) VALUES(@Name)",
      "logger" => $"INSERT INTO {_nameClass}s (Name, SendDt) VALUES(@Name, @SendDt)",
      "carlogconnect" => $"INSERT INTO carlogconnects (loggerid, carid, datetime) VALUES(@LoggerID, @CarID, @DateTime)",
      "tlogger" => $"INSERT INTO {tname} (carid, dtsave, percentmem, path, nameconfig, textfile) " +
                                $"VALUES(@CarId, @DTSave, @PercentMem, @Path, @NameConfig, @TextFile)",
      _ => ""
    };
    //  {Name , CarID, DTSave , PercentMem, NameConfig, Path});
    if (string.IsNullOrEmpty(sqlQuery))
      return;

    using (IDbConnection db = new NpgsqlConnection(_connectDBparams))
    {
      int rowsAffected = db.Execute(sqlQuery, table);
    }
  }
  #endregion

  #region  ===-- Drop  --===
  public async void DropAllTablesAsync() => await DropAllTablesAsync(AllTable().ToArray());
  public async Task DropAllTablesAsync(string[] s) => await DropTablesAsync(String.Join(", ", s));
  public async Task DropTablesAsync(string s) => await ComExecuteAsync(DropTable(s));

  #endregion

  public async void DeleteDublicateCarLogConnect()=>
      await ComExecuteAsync("DELETE FROM carlogconnects WHERE ctid NOT IN (SELECT max(ctid) FROM carlogconnects GROUP BY carId, loggerid, datetime);");
  
  public async void UpDateLoggerDt(LoggerDt[] table)
  {
    string sqlQuery = "UPDATE loggers SET SendDt = @SendDt WHERE LoggerID = @LoggerID;";

    using (IDbConnection db = new NpgsqlConnection(_connectDBparams))
    {
      int rowsAffected = db.Execute(sqlQuery, table);
    }
  }

  #region === Command NpgsqlConnection ===
  #region ==-  Execute  -==
  public async Task ComExecuteAsync(string command)
  {
    Action<string> _f0 = s => {
      using (var db = new NpgsqlConnection(_connectDBparams)) //IDbConnection
      {
        db.Execute(s);
      }
    };

    await Task.Run(() => _f0(command));
  }
  public void ComExecute(string command)
  {
    using (var db = new NpgsqlConnection(_connectDBparams)) //IDbConnection
    {
      db.Execute(command);
    }
  }
  #endregion

  #endregion

  #region -- AllTables Получение списка таблиц
  public List<string> AllTable() => QueryT<string>("SELECT table_name " +
      " FROM information_schema.tables " +
      " WHERE table_schema NOT IN('information_schema', 'pg_catalog');").ToList();
  public bool IsTable(string st) => AllTable().Find(z => z == st) == st;
  #endregion
}
