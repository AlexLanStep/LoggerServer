using Prism.Ioc;
using System.Windows;
using WML.Views;
using WML.ViewModels;
using Prism.DryIoc;
using DrawGraph.ViewModels;
using DrawGraph.Views;
using Prism.Regions;
using DB;
using DB.Static;

namespace WML
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {

    protected override Window CreateShell()
    {
      return Container.Resolve<Shell>();
    }

    protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
    {
      base.RegisterRequiredTypes(containerRegistry);
    }
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterSingleton<IMyTimer, MyTimer>();
      //      containerRegistry.Resolve<IGetDanNew>();
      //containerRegistry.RegisterViewWithRegion("NewGraph", typeof(NewGraph));

      


      containerRegistry.RegisterSingleton<ILogHistoryProcArray, LogHistoryProcArray>();
      containerRegistry.RegisterSingleton<ILogHistoryProcTextArray, LogHistoryProcTextArray>();

      containerRegistry.RegisterSingleton<IGetDanNew, GetDanNew>();


      //      containerRegistry.RegisterForNavigation<DrawLine, DrawLineViewModel>();

      //_ = container.Resolve<IGetDanNew>();

      //      containerRegistry.RegisterInstance<AppConfig>(appConfig);

      //      containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
      //      containerRegistry.RegisterForNavigation<Logger, LoggerViewModel>();
      //      containerRegistry.RegisterForNavigation<LoggerDetals, LoggerDetalsViewModel>();


      //      regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonList));
      //      regionManager.RegisterViewWithRegion("PersonDetailsRegion", typeof(PersonDetail));

    }


    protected override void OnStartup(StartupEventArgs e)
    {
      _ = new  StartForVisual();
      //IEventAggregator ea
      //_ea.GetEvent<LoadDanDb>().Publish();
      base.OnStartup(e);

    }

  }
}
