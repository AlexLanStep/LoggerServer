
using System.Timers;

namespace WML.Models;

public interface IMyTimer:IDisposable
{
  System.Timers.Timer ATimer { get; set; }
  void Dispose();
}

public class MyTimer : IMyTimer
{
  public  System.Timers.Timer ATimer { get; set; }
  private IEventAggregator _ea;
  public MyTimer(IEventAggregator ea)
  {
    _ea = ea;
    ATimer = new System.Timers.Timer(1000);
//    ATimer.Elapsed += OnTimedEvent;
//    ATimer.AutoReset = true;
//    ATimer.Enabled = true;
  }

  private void OnTimedEvent(object sender, ElapsedEventArgs e)
  {
    _ea.GetEvent<UpdetOutputEvent>().Publish(null);
  }

  public void Dispose()
  {
    ATimer.Stop();
    ATimer.Dispose();
  }
}
