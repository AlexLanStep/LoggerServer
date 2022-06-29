
namespace WML.Models;
public class Element
{
  public Element(string name, DateTime dateTime)
  {
    Name = name;
    DateTime = dateTime.ToString();
  }

  public override string ToString()=>$" car- {Name} datetime - {DateTime} ";
  
  public string Name { get; set; }
  public string DateTime { get; set; }
}


