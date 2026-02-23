using SingletonApp.Configuration;

var config1 = ConfigurationManager.Instance;
config1.LoadConfigurations();
config1.UpdateSetting("LogLevel", "Debug");

var config2 = ConfigurationManager.Instance;
Console.WriteLine($"Config1 LogLevel: {config1.GetSetting("LogLevel")}");
Console.WriteLine($"Config2 LogLevel: {config2.GetSetting("LogLevel")}");
Console.WriteLine("✅ Consistência: mesma instância, mesmo valor.");