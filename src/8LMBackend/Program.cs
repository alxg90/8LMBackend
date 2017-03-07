using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft​.Extensions​.DependencyInjection;
using Microsoft​.AspNetCore​.Mvc;
using Microsoft​.AspNetCore​.Mvc​.Cors​.Internal;

namespace _8LMBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                //.UseUrls("http://192.168.88.49:5000")
                .Build();

            host.Run();
        }
    }
}
