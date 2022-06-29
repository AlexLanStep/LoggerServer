
namespace WML.ViewModels;

public class GraphViewModel : BindableBase
{
  private string _nameLogger;
  private IEventAggregator _ea;

  private string _labelGraphPlot;
  public string LabelGraphPlot
  {
    get { return _labelGraphPlot; }
    set { SetProperty(ref _labelGraphPlot, value); }
  }

  public GraphViewModel(IEventAggregator ea)
  {
    _labelGraphPlot = "График - Cвободноe место на карте "; 
    _ea = ea;
    _ea.GetEvent<GraphSendVM>().Subscribe(_nNameLoggerInfo);
  }

  private void _nNameLoggerInfo(IOneLogger obj)
  {
    if (obj == null)
      return;
    _nameLogger = obj.LoggerName;
    LabelGraphPlot = $"График - Cвободноe место на карте - {_nameLogger} ";

  }
}
