using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncPrinciple1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string html = await httpClient.GetStringAsync("Https://www.baidu.com");
                Console.WriteLine(html);
            }

            // 创建文本文件
            string fileName = @"./a.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }

            // 构造一个大的字符串，以写入到文本文件
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 1000000; i++)
            {
                stringBuilder.Append("Hello!\n");
            }

            // 异步写入，下面这段代码执行起来就好像在执行for循环一样
            await File.WriteAllTextAsync(fileName, stringBuilder.ToString());// 写一个，返回一次
            Console.WriteLine("写入完成！");

            string s = await File.ReadAllTextAsync(fileName);                // 读一个，返回一次
            Console.WriteLine(s);
            Console.WriteLine("读取完成！");


            // 1.上面这段代码执行的结果是先打印出“写入完成！”，然后不断地打印“Hello!”，最后打印"读取完成！"

            // 2. async的方法会被C# 编译器编译成一个类，
            // 会主要根据await调用切分成多个状态，
            // 对async方法的调用会被拆分成为对MoveNext（状态机）的调用
            // 用await看似是在“等待”，经过编译后，其实没有“wait”

            // 我的理解是：异步方法的编译器实现是一个状态机，执行异步方法实际上是在执行这个状态机，
            // 并没有真正等待，而是启用多线程执行状态机的代码
        }
    }
}
