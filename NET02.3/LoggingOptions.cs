namespace MyApp
{
    public class LoggingOptions
    {
        public string MinimumLogLevel { get; set; }
        public TextListenerOptions TextListener { get; set; }
        public WordListenerOptions WordListener { get; set; }
        public EventLogListenerOptions EventLogListener { get; set; }
    }

    public class TextListenerOptions
    {
        public string FilePath { get; set; }
    }

    public class WordListenerOptions
    {
        public string FilePath { get; set; }
    }

    public class EventLogListenerOptions
    {
        public string SourceName { get; set; }
    }
}