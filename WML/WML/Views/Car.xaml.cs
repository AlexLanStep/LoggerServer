
namespace WML.Views;
public partial class Car : UserControl
{
  private IEventAggregator _ea;

  public Car(IRegionManager regionManager, IEventAggregator ea)
  {
    _ea = ea;
    _ea.GetEvent<UpdetOutputEvent>().Subscribe(updateOutput);

    InitializeComponent();
  }

  private void updateOutput(IUpdetOutputEvent obj)
  {
    throw new NotImplementedException();
  }
}
