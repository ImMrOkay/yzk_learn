using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P12
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
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

            // 异步写入,await直到写完
            await File.WriteAllTextAsync(fileName, stringBuilder.ToString());

            // 下面这段代码执行起来就好像在执行for循环一样
            string s = await File.ReadAllTextAsync(fileName);                
            Console.WriteLine(s);


            // 上面的代码执行的结果：await前和await后的线程可能是不一样的！

            // 表明：在await的等待期间.NET会把当前的线程返回给线程池，
            // 等异步方法调用（状态机）执行完毕后，框架会从线程池再取一个新的线程，
            // 继续执行当前方法的后续代码

            // 如果异步执行非常快，线程可能不会发生切换
        }
    }
}
