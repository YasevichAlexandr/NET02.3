using Microsoft.Extensions.Configuration;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var logger = new Logger(configuration);

            logger.Log("Info", "This is an information message.");
            logger.Log("Warning", "This is a warning message.");
            logger.Log("Error", "This is an error message.");

            var obj = new { Name = "Alex", Age = 21 };
            logger.Track(obj);
        }
    }
}