using Microsoft.Extensions.Configuration;

namespace MyApp.Listeners
{
    public interface IListener
    {
        void WriteMessage(string message);
        void Configure(IConfiguration configuration);
    }
}