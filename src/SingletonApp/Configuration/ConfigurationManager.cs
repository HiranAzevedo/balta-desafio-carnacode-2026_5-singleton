using System.Collections.Concurrent;

namespace SingletonApp.Configuration;

public sealed class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> _instance =
        new(() => new ConfigurationManager());

    public static ConfigurationManager Instance => _instance.Value;

    private readonly ConcurrentDictionary<string, string> _settings;
    private bool _isLoaded;
    private readonly Lock _loadLock = new();

    private ConfigurationManager()
    {
        _settings = [];
        _isLoaded = false;
        Console.WriteLine("✅ ConfigurationManager Singleton criado (uma vez).");
    }

    public void LoadConfigurations()
    {
        if (_isLoaded)
        {
            return;
        }

        lock (_loadLock)
        {
            if (_isLoaded)
            {
                return;
            }

            Console.WriteLine("🔄 Carregando configurações...");

            Thread.Sleep(200);

            _settings["DatabaseConnection"] = "Server=localhost;Database=MyApp;";
            _settings["ApiKey"] = "abc123xyz789";
            _settings["CacheServer"] = "redis://localhost:6379";
            _settings["MaxRetries"] = "3";
            _settings["TimeoutSeconds"] = "30";
            _settings["EnableLogging"] = "true";
            _settings["LogLevel"] = "Information";

            _isLoaded = true;
            Console.WriteLine("✅ Configurações carregadas com sucesso!\n");
        }
    }

    public string? GetSetting(string key)
    {
        if (!_isLoaded)
        {
            LoadConfigurations();
        }

        return _settings.TryGetValue(key, out var value) ? value : null;
    }

    public void UpdateSetting(string key, string value)
    {
        // Se houver leitura em paralelo, considere trocar por ConcurrentDictionary
        _settings[key] = value;
        Console.WriteLine($"Configuração atualizada: {key} = {value}");
    }
}
