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
            //service.AddScoped<IConfigServices, EnvVarConfigServices>();
            //service.AddScoped(typeof(IConfigServices), s => new IniFileConfigService("mail.ini"));
           
            //增加扩展方法，直接Add不需要用户获取类名
            service.AddIniFileConfigService("mail.ini");
            service.AddScoped<ILogProvider, ConsoleLogProvider>();
            service.AddScoped<IMailServices,MailServices.MailServices>();
            var sp=service.BuildServiceProvider();

            using (var sc = sp.CreateScope())
            {
                var mailservice = sc.ServiceProvider.GetRequiredService<IMailServices>();
                mailservice.Send("970226618@qq.com","biden","we");             
            }


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        } 
     }
}
