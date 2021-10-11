using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using IroojBackend.socket;

namespace IroojBackend
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            SocketMain.InitializeSocket();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}