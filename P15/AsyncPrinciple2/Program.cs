using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncPrinciple2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine($"程序启动时的线程Id：{Thread.CurrentThread.ManagedThreadId}");
            // 创建文本文件
            string fileName = @"./a.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                Thread.Sleep(1000);
            }

            // 构造一个大的字符串，以写入到文本文件
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 5000000; i++)
            {
                stringBuilder.Append("Hello!\n");
            }

            // 异步写入,await直到写完;
            Console.WriteLine($"await写入前的线程Id：{Thread.CurrentThread.ManagedThreadId}");
            await File.WriteAllTextAsync(fileName, stringBuilder.ToString());
            Console.WriteLine($"await写入后的线程Id：{Thread.CurrentThread.ManagedThreadId}");

            // 下面这段代码执行起来就好像在执行for循环一样
            Console.WriteLine($"await读取前的线程Id：{Thread.CurrentThread.ManagedThreadId}");
            string s = await File.ReadAllTextAsync(fileName);
            Console.WriteLine($"await读取后的线程Id：{Thread.CurrentThread.ManagedThreadId}");

            //Console.WriteLine(s);
            Console.WriteLine($"程序结束时的线程Id：{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
