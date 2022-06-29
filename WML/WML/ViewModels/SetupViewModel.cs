
namespace WML.ViewModels;

public class SetupViewModel : BindableBase
{
  private string _labelSetup = "Настройка программы";
  public string LabelSetup
  {
    get { return _labelSetup; }
    set { SetProperty(ref _labelSetup, value); }
  }

  public SetupViewModel()
  {

  }
}
