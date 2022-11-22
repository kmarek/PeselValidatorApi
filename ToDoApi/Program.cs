using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TodoApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Program
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static void Main(string[] args)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            CreateHostBuilder(args).Build().Run();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static IHostBuilder CreateHostBuilder(string[] args) =>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
