
namespace ConvertAll.Static;

public class ContainerManager
{
  private static readonly Lazy<ContainerManager> Lazy = new Lazy<ContainerManager>(() => new ContainerManager());
  private ContainerManager()
  {
    DbContainer = new DryCont();              // new DryIoc.Container();
    DbContainer.Register<IHandlingDir, HandlingDir>(Reuse.Singleton);
    DbContainer.Register<IStartExe, StartExe>();
  }

  public static ContainerManager GetInstance() => Lazy.Value;

  public DryCont DbContainer;

}
