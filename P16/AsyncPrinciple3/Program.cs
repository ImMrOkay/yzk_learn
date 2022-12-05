using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncPrinciple3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"第1次调用耗时计算前的线程Id:{Thread.CurrentThread.ManagedThreadId}");
            await CalculateAsync(5000);
            Console.WriteLine($"第1次调用耗时计算后的线程Id:{Thread.CurrentThread.ManagedThreadId}");


            Console.WriteLine($"第2次调用耗时计算前的线程Id:{Thread.CurrentThread.ManagedThreadId}");
            await CalculateAsync2(5000);
            Console.WriteLine($"第2次调用耗时计算后的线程Id:{Thread.CurrentThread.ManagedThreadId}");

            // 执行结果：第1次调用线程Id不会改变，第2次调用线程Id可能改变

            // 异步方法的代码并不会自动在新线程中执行，除非主动把代码放到（Task.Run）新线程中执行
        }

        static async Task<double> CalculateAsync(int n)
        {
            double result = 0;
            Random random = new Random();
            for (int i = 0; i < n * n; i++)
            {
                result = (result + random.NextDouble()) / n;
            }
            return result;
        }

        static async Task<double> CalculateAsync2(int n)
        {
            return await Task.Run(() =>
            {
                double result = 0;
                Random random = new Random();
                for (int i = 0; i < n * n; i++)
                {
                    result = (result + random.NextDouble()) / n;
                }
                return result;
            });
        }
    }
}
