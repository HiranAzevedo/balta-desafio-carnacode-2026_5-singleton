//// DESAFIO: Gerenciador de Configurações da Aplicação
//// PROBLEMA: Uma aplicação precisa carregar configurações de banco de dados, APIs e cache
//// uma única vez e compartilhar entre todos os componentes. O código atual permite múltiplas
//// instâncias, causando inconsistências e desperdício de recursos

//using System;
//using System.Collections.Generic;

//namespace DesignPatternChallenge
//{
//    // Contexto: Sistema que precisa de configurações centralizadas e consistentes
//    // As configurações são carregadas de arquivos, variáveis de ambiente e banco de dados

//    public class ConfigurationManager
//    {
//        private Dictionary<string, string> _settings;
//        private bool _isLoaded;

//        public ConfigurationManager()
//        {
//            _settings = new Dictionary<string, string>();
//            _isLoaded = false;
//            Console.WriteLine("⚠️ Nova instância de ConfigurationManager criada!");
//        }

//        public void LoadConfigurations()
//        {
//            if (_isLoaded)
//            {
//                Console.WriteLine("Configurações já carregadas.");
//                return;
//            }

//            Console.WriteLine("🔄 Carregando configurações...");

//            // Simulando operação custosa de carregamento
//            System.Threading.Thread.Sleep(200);

//            // Carregando configurações de diferentes fontes
//            _settings["DatabaseConnection"] = "Server=localhost;Database=MyApp;";
//            _settings["ApiKey"] = "abc123xyz789";
//            _settings["CacheServer"] = "redis://localhost:6379";
//            _settings["MaxRetries"] = "3";
//            _settings["TimeoutSeconds"] = "30";
//            _settings["EnableLogging"] = "true";
//            _settings["LogLevel"] = "Information";

//            _isLoaded = true;
//            Console.WriteLine("✅ Configurações carregadas com sucesso!\n");
//        }

//        public string GetSetting(string key)
//        {
//            if (!_isLoaded)
//                LoadConfigurations();

//            if (_settings.ContainsKey(key))
//                return _settings[key];

//            return null;
//        }

//        public void UpdateSetting(string key, string value)
//        {
//            _settings[key] = value;
//            Console.WriteLine($"Configuração atualizada: {key} = {value}");
//        }
//    }

//    // Serviços da aplicação que precisam das configurações
//    public class DatabaseService
//    {
//        private readonly ConfigurationManager _config;

//        public DatabaseService()
//        {
//            // Problema: Cada serviço cria sua própria instância
//            _config = new ConfigurationManager();
//        }

//        public void Connect()
//        {
//            var connectionString = _config.GetSetting("DatabaseConnection");
//            Console.WriteLine($"[DatabaseService] Conectando ao banco: {connectionString}");
//        }
//    }

//    public class ApiService
//    {
//        private readonly ConfigurationManager _config;

//        public ApiService()
//        {
//            // Problema: Nova instância = novos carregamentos desnecessários
//            _config = new ConfigurationManager();
//        }

//        public void MakeRequest()
//        {
//            var apiKey = _config.GetSetting("ApiKey");
//            Console.WriteLine($"[ApiService] Fazendo requisição com API Key: {apiKey}");
//        }
//    }

//    public class CacheService
//    {
//        private readonly ConfigurationManager _config;

//        public CacheService()
//        {
//            // Problema: Mais uma instância duplicada
//            _config = new ConfigurationManager();
//        }

//        public void Connect()
//        {
//            var cacheServer = _config.GetSetting("CacheServer");
//            Console.WriteLine($"[CacheService] Conectando ao cache: {cacheServer}");
//        }
//    }

//    public class LoggingService
//    {
//        private readonly ConfigurationManager _config;

//        public LoggingService()
//        {
//            _config = new ConfigurationManager();
//        }

//        public void Log(string message)
//        {
//            var logLevel = _config.GetSetting("LogLevel");
//            Console.WriteLine($"[LoggingService] [{logLevel}] {message}");
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("=== Sistema de Configurações ===\n");

//            // Problema 1: Múltiplas instâncias são criadas
//            Console.WriteLine("Inicializando serviços...\n");

//            var dbService = new DatabaseService();
//            var apiService = new ApiService();
//            var cacheService = new CacheService();
//            var logService = new LoggingService();

//            Console.WriteLine("\nUsando os serviços...\n");

//            dbService.Connect();
//            apiService.MakeRequest();
//            cacheService.Connect();
//            logService.Log("Sistema iniciado");

//            // Problema 2: Configurações podem ficar inconsistentes
//            Console.WriteLine("\n--- Tentativa de atualização ---\n");

//            var config1 = new ConfigurationManager();
//            config1.LoadConfigurations();
//            config1.UpdateSetting("LogLevel", "Debug");

//            var config2 = new ConfigurationManager();
//            config2.LoadConfigurations();
//            Console.WriteLine($"Config1 LogLevel: {config1.GetSetting("LogLevel")}");
//            Console.WriteLine($"Config2 LogLevel: {config2.GetSetting("LogLevel")}");
//            Console.WriteLine("⚠️ Inconsistência: Instâncias diferentes têm valores diferentes!");

//            // Problema 3: Desperdício de memória e processamento
//            Console.WriteLine("\n--- Impacto de Performance ---");
//            Console.WriteLine("Cada serviço carregou as configurações separadamente");
//            Console.WriteLine("Isso multiplica o uso de memória e tempo de inicialização");

//            // Perguntas para reflexão:
//            // - Como garantir que apenas uma instância de ConfigurationManager exista?
//            // - Como fazer todos os serviços compartilharem a mesma instância?
//            // - Como controlar o ponto de criação e acesso à instância única?
//            // - Como lidar com thread-safety em cenários multi-thread?
//        }
//    }
//}
