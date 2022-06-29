using Prism.Mvvm;

namespace TestReactiveWPF.ViewModels
{
  public class ShellViewModel : BindableBase
  {
    private string _title = "Prism Application";
    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }

    public ShellViewModel()
    {

    }
  }
}
