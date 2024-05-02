using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace MyApp.Listeners
{
    public class EventLogListener : IListener
    {
        private string sourceName;

        public void Configure(IConfiguration configuration)
        {
            sourceName = configuration["EventLogListener:SourceName"];

            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }
        }

        public void WriteMessage(string message)
        {
            EventLog.WriteEntry(sourceName, message, EventLogEntryType.Information);
        }
    }
}