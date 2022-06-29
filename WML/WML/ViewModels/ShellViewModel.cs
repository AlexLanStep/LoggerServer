
namespace WML.ViewModels;

public class ShellViewModel : BindableBase
{
  private string _title = "";
  public string Title
  {
    get { return _title; }
    set { SetProperty(ref _title, value); }
  }

  private string _labelShapka = "";
  public string LabelShapka
  {
    get { return _labelShapka; }
    set { SetProperty(ref _labelShapka, value); }
  }

  public ShellViewModel()
  {
    Title = "---=== НАМИ ===---";
    LabelShapka = "Сервесная программа для Сергея!";
  }
}
