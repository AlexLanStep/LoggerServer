
namespace DB.Static;

public interface ILogHistoryProc
{
  DateTime DtSize { get; set; }
  double Proc { get; set; }
  string Path { get; set; }
  string LoggerName { get; set; }
}

public class LogHistoryProc: ILogHistoryProc
{
  public LogHistoryProc(DateTime dtSize, double proc, string path, string loggerName)
  {
    DtSize = dtSize;
    Proc = proc;
    Path = path;
    LoggerName = loggerName;
  }

  public DateTime DtSize { get; set; }
  public double Proc{ get; set; }
  public string Path { get; set; }
  public string LoggerName { get; set; }
}

public interface ILogHistoryProcArray
{
  LogHistoryProc[] LogHistoryProcs { get; set; }
}

public class LogHistoryProcArray : ILogHistoryProcArray
{

  public LogHistoryProc[] LogHistoryProcs { get; set; }
}

public interface ILogHistoryProcText : ILogHistoryProc
{
  string[] TextFile { get; set; }
}

public class LogHistoryProcText : LogHistoryProc, ILogHistoryProcText
{
  public LogHistoryProcText(DateTime dtSize, double proc, string path, string loggerName, string[] textFile) 
          : base(dtSize, proc, path, loggerName) => TextFile = textFile;

  public string[] TextFile { get; set; }
}

public interface ILogHistoryProcTextArray
{
  LogHistoryProcText[] LogHistoryProcTexts { get; set; }
}

public class LogHistoryProcTextArray : ILogHistoryProcTextArray
{
  public LogHistoryProcText[] LogHistoryProcTexts { get; set; }
}


