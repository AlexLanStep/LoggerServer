using ConvertAll.Static;
using System.Diagnostics;

namespace ConvertAll.Core;

public interface IStartExe
{
  void ExeInfoOne(string pathConvExe);
  void ExeInfoStream(string pathConvExe, int count);
}
public class StartExe: IStartExe
{
  private IHandlingDir _handlingDir;
  private string _pathConvExe;
  private ContainerManager _container;

  public StartExe(IHandlingDir handlingDir)
  {
    _handlingDir = handlingDir;
    _container = ContainerManager.GetInstance();
  }
  public virtual void ExeInfoOne(string pathConvExe)
  {
    _pathConvExe = pathConvExe;
    int error = 0;
    foreach (var path in _handlingDir.WorkDir.Where(z=>z.Value==0))
    {
      var _xcount = _handlingDir.WorkDir.Count(x=>x.Value==0);
      Console.WriteLine("/////////////////////////////////////////////////////////////");
      Console.WriteLine($"///////////// ---- осталоcь {_xcount}      ---- /////////////////");    
      Console.WriteLine("/////////////////////////////////////////////////////////////");
      _handlingDir.WorkDir.AddOrUpdate(path.Key, 1, (key,val)=>1);
      using (var runProcess = new Process())
      {
        runProcess.StartInfo.FileName = _pathConvExe;
        runProcess.StartInfo.Arguments = path.Key;
        runProcess.StartInfo.UseShellExecute = false;
        runProcess.StartInfo.RedirectStandardOutput = true;
        runProcess.Start();
        string line;
        while ((line = runProcess.StandardOutput.ReadLine()) != null)
          Console.WriteLine(line);

        runProcess.WaitForExit();
        error = runProcess.ExitCode;
      }
      _handlingDir.WorkDir.AddOrUpdate(path.Key, 2, (key, val) => 2);
    }
    
  }

  public virtual void ExeInfoStream(string pathConvExe, int count)
  {
    int _countDic = _handlingDir.WorkDir.Count;
    if (_countDic == 0)
      return;

    count = _countDic  > count ? count : _countDic;  
    List<Task> tasks = new();
    for (int i = 0; i < count; i++)
    {
      var _task = Task.Run(() =>
      {
        var _startExe = _container.DbContainer.Resolve<IStartExe>();
        _startExe.ExeInfoOne(pathConvExe);
      });
      tasks.Add(Task.Run(() => { var _startExe = _container.DbContainer.Resolve<IStartExe>();
                                  _startExe.ExeInfoOne(pathConvExe);}));
                                }
    while (tasks.Count() > 0)
    {
      Console.WriteLine($"xxxxxxxxxx     {tasks.Count()}   осталось Dir ==> {_handlingDir.WorkDir.Count(x => x.Value == 0)}                 xxxxxxxxxxxxxx");
      Thread.Sleep(1000);
      int _countTask = 0; 
      while(_countTask < tasks.Count())
      {
        switch (tasks.ElementAt(_countTask).Status)
        {
          case TaskStatus.Created:
          case TaskStatus.WaitingForActivation:
          case TaskStatus.WaitingToRun:
          case TaskStatus.Running:
          case TaskStatus.Canceled:
            _countTask++;
            break;

          case TaskStatus.WaitingForChildrenToComplete:
          case TaskStatus.RanToCompletion:
          case TaskStatus.Faulted:
            tasks.RemoveAt(_countTask);
            break;
          default:
            _countTask++;
            break;
        }
      }
    }
  }
}
