
namespace WML.Models;

public class GraphSendVM : PubSubEvent<IOneLogger> { } //string, ObservableCollection<DateTimeSize>
public class GraphSendVMCar : PubSubEvent<IOneCar> { } //string, ObservableCollection<DateTimeSize>

public class LoadDanDb : PubSubEvent { } // start загрузки

public interface IUpdetOutputEvent {}

public class UpdetOutputEvent : PubSubEvent<IUpdetOutputEvent>{}

public interface IRefreshGraph { }

public class RefreshGraph : PubSubEvent<IRefreshGraph>
{
  public void Publish()
  {
    throw new NotImplementedException();
  }
}

public class RefreshGraphCar : PubSubEvent<IRefreshGraph> { }
