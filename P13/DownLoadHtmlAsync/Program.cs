using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DownLoadHtmlAsync
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

            int length = await DownLoadHtmlAsync("https://www.youzack.com", fileName);
            Console.WriteLine($"OK:{length}");
        }

        static async Task<int> DownLoadHtmlAsync(string url, string fileName)
        {
            using HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(url);
            await File.WriteAllTextAsync(fileName, html);
            return html.Length;
        }
    }
}
