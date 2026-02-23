using SingletonApp.Configuration;

namespace SingletonApp;

public class DatabaseService
{
    private readonly ConfigurationManager _config = ConfigurationManager.Instance;

    public void Connect()
    {
        var connectionString = _config.GetSetting("DatabaseConnection");
        Console.WriteLine($"[DatabaseService] Conectando ao banco: {connectionString}");
    }
}

public class ApiService
{
    private readonly ConfigurationManager _config = ConfigurationManager.Instance;

    public void MakeRequest()
    {
        var apiKey = _config.GetSetting("ApiKey");
        Console.WriteLine($"[ApiService] Fazendo requisição com API Key: {apiKey}");
    }
}

public class CacheService
{
    private readonly ConfigurationManager _config = ConfigurationManager.Instance;

    public void Connect()
    {
        var cacheServer = _config.GetSetting("CacheServer");
        Console.WriteLine($"[CacheService] Conectando ao cache: {cacheServer}");
    }
}

public class LoggingService
{
    private readonly ConfigurationManager _config = ConfigurationManager.Instance;

    public void Log(string message)
    {
        var logLevel = _config.GetSetting("LogLevel");
        Console.WriteLine($"[LoggingService] [{logLevel}] {message}");
    }
}
