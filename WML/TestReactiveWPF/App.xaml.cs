using Prism.Ioc;
using System.Windows;
using TestReactiveWPF.Views;

namespace TestReactiveWPF
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

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {

    }
  }
}
