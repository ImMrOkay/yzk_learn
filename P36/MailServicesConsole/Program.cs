using ConfigServices;
using LogServices;
using MailServices;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MailServicesConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection service = new ServiceCollection();
            service.AddScoped<IConfigServices, EnvVarConfigServices>();
            service.AddScoped<ILogProvider, ConsoleLogProvider>();
            service.AddScoped<IMailServices,MailServices.MailServices>();
            var sp=service.BuildServiceProvider();

            using (var sc = sp.CreateScope())
            {
                var mailservice = sc.ServiceProvider.GetRequiredService<IMailServices>();
                mailservice.Send("Hello World!", "970226618@qq.com", "we");             
            }
            Console.ReadLine();
        } 
     }
}
