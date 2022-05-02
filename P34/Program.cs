using Microsoft.Extensions.DependencyInjection;
using System;

namespace P34
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<ITestService,CnTestService>();
            services.AddScoped<ITestService, CnTestService2>();
            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                using (var sc = provider.CreateScope())
                {

                    var ss = sc.ServiceProvider.GetServices<ITestService>();
                    foreach (var s in ss)
                    {
                        Console.WriteLine(s.GetType());
                    }

                    //在scope中获取Scope相关的对象，要用sc.ServiceProvider而不是provider
                    var cnTestService = sc.ServiceProvider.GetService<ITestService>();
                    cnTestService.SayHi();

                }
            }
            Console.ReadLine();
        }
    }

    public interface ITestService
    {
        string Name { get; set; }

        void SayHi();
    }

    public class CnTestService : ITestService
    {
        public string Name { get { return "老胡"; } set { } }

        public void SayHi()
        {
            Console.WriteLine("你好！我叫：" + Name);
        }
    }
    public class CnTestService2 : ITestService
    {
        public string Name { get { return "LaoHu"; } set { } }

        public void SayHi()
        {
            Console.WriteLine("Hello！I'm：" + Name);
        }
    }
}
