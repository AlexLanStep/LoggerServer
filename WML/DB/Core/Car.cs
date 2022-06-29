namespace DB.Core;

public interface ICar
{
  int CarID { get; set; }
  string Name { get; set; }
}
public class Car : ICar
{
  public int CarID { get; set; }
  public string Name { get; set; }
}


