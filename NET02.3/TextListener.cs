using Microsoft.Extensions.Configuration;

namespace MyApp.Listeners
{
    public class TextListener : IListener
    {
        private string filePath;

        public void Configure(IConfiguration configuration)
        {
            filePath = configuration["TextListener:FilePath"];
        }

        public void WriteMessage(string message)
        {
            File.AppendAllText(filePath, message + System.Environment.NewLine);
        }
    }
}