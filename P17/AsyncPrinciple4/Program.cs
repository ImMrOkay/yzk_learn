using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncPrinciple4
{
    internal class Program
    {
        static Task Main(string[] args)
        {
            var t = CalculateAsync(500);
            Console.WriteLine($"{t.Result}");

            return Task.CompletedTask;
            // async 方法的缺点：
            // 1. 异步方法会生成一个类，运行效率没有普通方法高；
            // 2. 可能会占用额外的线程资源

            // 当异步方法只是简单地转发Task，
            // 而不需要await结果之后进行其他的额外操作，则可以不用async 和 await
            // 这样做的好处是不会生成新的类（可通过反编译验证），运行效率更高，也不会占用额外的线程资源浪费
        }

        static Task<double> CalculateAsync(int n)
        {
            double result = 0;
            Random random = new Random();
            for (int i = 0; i < n * n; i++)
            {
                result = (result + random.NextDouble()) / n;
            }
            return Task.FromResult(result);
        }
    }
}
