namespace DB.Static;

public class ContainerManager
{
  private static readonly Lazy<ContainerManager> Lazy = new Lazy<ContainerManager>(() => new ContainerManager());
  private ContainerManager()
  {
    DbContainer = new DryCont();              // new DryIoc.Container();
    
    DbContainer.Register<IParsingXml, ParsingXml>(Reuse.Singleton);
    DbContainer.Register<IConfigDB, ConfigDB>(Reuse.Singleton);
    DbContainer.Register<IContext, Context>(Reuse.Singleton);
    DbContainer.Register<IProcessingDb, ProcessingDb>();
    DbContainer.Register<IDbInfo, DbInfo>(Reuse.Singleton);
  }

  public static ContainerManager GetInstance() => Lazy.Value;

  public DryCont DbContainer;

}
