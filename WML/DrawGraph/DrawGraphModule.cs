using DrawGraph.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DrawGraph
{
  public class DrawGraphModule : IModule
  {
    public void OnInitialized(IContainerProvider containerProvider)
    {
      var regionManager = containerProvider.Resolve<IRegionManager>();
      regionManager.RegisterViewWithRegion("ContentRegion", typeof(DrawLine) );
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {

    }
  }
}