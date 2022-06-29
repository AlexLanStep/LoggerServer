
namespace DB.Static;

public interface IConfigDB
{
  string DbName { get; set; }
  int Port { get; set; }
  string Username { get; set; }
  string Password { get; set; }
  string ConnectDBparams { get; set; }
  void LoadJson(string path);
  string FormConnectStr(string dataBaseName, int port, string username, string password);
  string FormConnectStr();
  static string StConnectDBparams { get; set; }
}
public class ConfigDB : IConfigDB
{
  #region dan
  public string DbName { get; set; }
  public int Port { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public string ConnectDBparams { get; set; }
  static public string StConnectDBparams { get; set; }
  #endregion

  public ConfigDB()
  {
    LoadJson();
  }

  public void LoadJson(string path = "")
  {
    DbName = "LoggerInfo";
    Port = 10000;
    Username = "postgres";
    Password = "123";

    ConnectDBparams = $"Host=localhost; Port={Port}; Database={DbName}; Username={Username}; Password={Password}";
    StConnectDBparams = ConnectDBparams;
  }

  public string FormConnectStr(string dataBaseName, int port, string username, string password)
  {
    ConnectDBparams = $"Host=localhost; Port={port}; Database={dataBaseName}; Username={username}; Password={password}";
    StConnectDBparams = ConnectDBparams;
    return ConnectDBparams;
  }

  public string FormConnectStr() => FormConnectStr(DbName, Port, Username, Password);

}

