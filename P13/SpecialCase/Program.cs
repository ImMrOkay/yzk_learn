using System;
using System.IO;
using System.Text;
using System.Threading;

namespace SpecialCase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建文本文件
            string fileName = @"./a.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }

            // 构造一个大的字符串，以写入到文本文件
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                stringBuilder.Append("Hello!\n");
            }

            // 特殊情况（无返回值）
            // 这里必须加上Wait()，只有写完了才能够读取,
            // 否则，由于调用写入的方法是异步的，会马上执行到下一句读取
            // 而写入文件是独占的，在写的同时不允许读取
            // 但是，在同步方法中调用Wait()，有死锁的风险，不推荐
            File.WriteAllTextAsync(fileName, stringBuilder.ToString()).Wait();

            // 特殊情况（有返回值）：调用的方法必须是同步方法时(即不能写await的时候)，
            // 可以采用下面的写法：用Result获取值
            var t = File.ReadAllTextAsync(fileName);
            Console.WriteLine(t.Result);



            // 异步委托
            ThreadPool.QueueUserWorkItem(async (obj) =>
            {
                var s = await File.ReadAllTextAsync(fileName);
                Console.WriteLine($"{s}OK");
            });
            Console.ReadLine();
        }
    }
}
