using Microsoft.Extensions.DependencyInjection;
using System;

namespace P32
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<CnTestService>();
            using(ServiceProvider provider=services.BuildServiceProvider())
            {
                var cnTestService = provider.GetService<CnTestService>();
                cnTestService.SayHi();
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
            Console.WriteLine("你好！我叫："+Name);
        }
    }
}
