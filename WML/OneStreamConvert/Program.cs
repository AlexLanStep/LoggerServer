using ConvertAll.Core;
using ConvertAll.Static;
using DryIoc;
using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace MyApp // Note: actual namespace depends on the project name.
{
  internal class Program
  {
    static ContainerManager _container;
    static  void Main(string[] args)
    {
      Console.WriteLine("Hello World! CONVERT!!!    ");
      string _path = @"\\mlmsrv\MLServer\PS19SED\log";
      string _pathConvertExe = @"\\mlmsrv\MLServer\#COMMON\Dll\convert.exe";
//      _path = @"\\mlmsrv\MLServer\PS19SED\log\2021-10-17_11-26-51";

      _container = ContainerManager.GetInstance();
      var _dir = _container.DbContainer.Resolve<IHandlingDir>(); //preResolveParent

      _dir.Run(new DateTime(2021, 10, 1), new DateTime(2022, 12, 1), _path);
      Console.WriteLine($"----->>>>  {_dir.WorkDir.Count()}");

      var _startExe = _container.DbContainer.Resolve<IStartExe>();
      _startExe.ExeInfoStream(_pathConvertExe, 6);
      //      _startExe.ExeInfo(_pathConvertExe);
    }
  }
}

