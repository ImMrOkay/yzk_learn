using Microsoft.Extensions.DependencyInjection;
using System;

namespace P33
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();

            //瞬态
            //services.AddTransient<CnTestService>();

            //单例
            //services.AddSingleton<CnTestService>();

            //scope
            services.AddScoped<CnTestService>();

            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                using (var sc=provider.CreateScope())
                {
                    //在scope中获取Scope相关的对象，要用sc.ServiceProvider而不是provider
                    var cnTestService=sc.ServiceProvider.GetService<CnTestService>();
                    cnTestService.SayHi();
                }
            }



            /*
            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                var cnTestService = provider.GetService<CnTestService>();
                cnTestService.SayHi();

                var cnTestService1 = provider.GetService<CnTestService>();
                cnTestService1.SayHi();

                //对象是否是同一个引用
                Console.WriteLine(object.ReferenceEquals(cnTestService,cnTestService1));
            }*/


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
}
