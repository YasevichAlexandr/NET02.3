using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MyApp
{
    public class Logger
    {
        private readonly List<Listeners.IListener> listeners;
        private readonly string minimumLogLevel;

        public Logger(IConfiguration configuration)
        {
            listeners = new List<Listeners.IListener>();

            var loggingOptions = new LoggingOptions();
            configuration.GetSection("Logging").Bind(loggingOptions);

            minimumLogLevel = loggingOptions.MinimumLogLevel;

            // Загрузка и настройка слушателей 
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (typeof(Listeners.IListener).IsAssignableFrom(type) && type != typeof(Listeners.IListener))
                {
                    var listener = (Listeners.IListener)Activator.CreateInstance(type);
                    listener.Configure(configuration);
                    listeners.Add(listener);
                }
            }
        }

        public void Log(string level, string message)
        {
            if (IsLogLevelEnabled(level))
            {
                foreach (var listener in listeners)
                {
                    listener.WriteMessage($"[{level}] {message}");
                }
            }
        }

        public bool IsLogLevelEnabled(string level)
        {
            // Логика проверки уровня логирования 
            var logLevels = new Dictionary<string, int>()
            {
                { "Trace", 0 },
                { "Debug", 1 },
                { "Info", 2 },
                { "Warning", 3 },
                { "Error", 4 }
            };

            if (logLevels.ContainsKey(level) && logLevels.ContainsKey(minimumLogLevel))
            {
                return logLevels[level] >= logLevels[minimumLogLevel];
            }

            return false;
        }

        public void Track(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var trackingAttribute = property.GetCustomAttribute<TrackingPropertyAttribute>();
                if (trackingAttribute != null)
                {
                    var propertyName = trackingAttribute.PropertyName ?? property.Name;
                    var value = property.GetValue(obj)?.ToString();
                    var message = $"{propertyName}={value}";
                    Log("Trace", message);
                }
            }

            var fields = type.GetFields();

            foreach (var field in fields)
            {
                var trackingAttribute = field.GetCustomAttribute<TrackingPropertyAttribute>();
                if (trackingAttribute != null)
                {
                    var propertyName = trackingAttribute.PropertyName ?? field.Name;
                    var value = field.GetValue(obj)?.ToString();
                    var message = $"{propertyName}={value}";
                    Log("Trace", message);
                }
            }
        }
    }
}