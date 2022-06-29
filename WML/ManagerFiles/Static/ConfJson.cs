
namespace ManagerFiles.Static;

public class ConfJson
{
  public static ConcurrentDictionary<string, string> ConvertCar = new();
  public static void AddConvertCar((string, string)[] d)
  {
    foreach (var item in d)
      ConvertCar.AddOrUpdate(item.Item1, item.Item2, (k, v) => item.Item2); 
  }
  public static void InicialConvertCar(Dictionary<string, string> d) 
    => ConvertCar = new ConcurrentDictionary<string, string>(d);
  

}
