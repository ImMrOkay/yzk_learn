using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskImportantMethods
{
    /* Task类的重要方法
     * 1. Task<Task> WhenAny(params Task[] tasks) 任何一个Task完成，Task就完成
     * 2. Task WhenAll(params Task[] tasks) 所有Task完成，Task才完成
     * 3. Task<TResult> FromResult<TResult>(TResult result) 创建指定结果的、成功完成的 Task<TResult>
     */

    internal class Program
    {
        static async Task Main(string[] args)
        {
            // 创建文本文件
            string fileNameA = @"./Temp/a.txt";
            string fileNameB = @"./Temp/b.txt";
            string fileNameC = @"./Temp/c.txt";

            if (!File.Exists(fileNameA))
            {
                File.Create(fileNameA);
            }

            if (!File.Exists(fileNameB))
            {
                File.Create(fileNameB);
            }

            if (!File.Exists(fileNameC))
            {
                File.Create(fileNameC);
            }
            Thread.Sleep(1000);

            // 构造一个大的字符串，以写入到文本文件
            Random random = new Random();
            StringBuilder stringBuilderA = new StringBuilder();
            int n1 = random.Next(100, 10000);
            for (int i = 0; i < n1; i++)
            {
                stringBuilderA.Append("Hello!\n");
            }

            StringBuilder stringBuilderB = new StringBuilder();
            int n2 = random.Next(100, 10000);
            for (int i = 0; i < n2; i++)
            {
                stringBuilderB.Append("Hello!\n");
            }

            StringBuilder stringBuilderC = new StringBuilder();
            int n3 = random.Next(100, 10000);
            for (int i = 0; i < n3; i++)
            {
                stringBuilderC.Append("Hello!\n");
            }

            var t1 = File.WriteAllTextAsync(fileNameA, stringBuilderA.ToString());
            var t2 = File.WriteAllTextAsync(fileNameB, stringBuilderB.ToString());
            var t3 = File.WriteAllTextAsync(fileNameC, stringBuilderC.ToString());
            await Task.WhenAll(t1, t2, t3);


            var fileNames = Directory.GetFiles(@"./temp/");
            var countTask = new Task<int>[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                var t = FileCharsAsync(fileNames[i]);
                countTask[i] = t;
            }

            var result = await Task.WhenAll(countTask);
            Console.WriteLine(result.Sum());
        }

        static Task<int> FileCharsAsync(string fileName)
        {
            var s = File.ReadAllTextAsync(fileName);
            return Task.FromResult(s.Result.Length);
        }
    }
}
