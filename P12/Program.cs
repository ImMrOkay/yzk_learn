using System;
using System.IO;
using System.Text;
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
            }

            // 构造一个大的字符串，以写入到文本文件
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 5000000; i++)
            {
                stringBuilder.Append("Hello!\n");
            }

            // 异步写入，下面这段代码执行起来就好像在执行for循环一样
            await File.WriteAllTextAsync(fileName, stringBuilder.ToString());// 写一个，返回一次
            string s = await File.ReadAllTextAsync(fileName);                // 读一个，返回一次
            Console.WriteLine(s);                                            // 收到一个s，打印一个s
        }
    }
}
