
namespace WML.Views;

public partial class Setup : UserControl
{
  public Setup()
  {
    InitializeComponent();
    myGif.Source = new Uri(Environment.CurrentDirectory + @"\gif1work.gif");
  }

  private void myGif_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
  {
    myGif.Position = new TimeSpan(0, 0, 1);
    myGif.Play();
  }
}
