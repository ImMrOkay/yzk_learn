using System;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreMySql
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await InitialTable(); //向数据库添加数据

            await using var myDbContext = new MyDbContext();

            // 筛选
            var books = myDbContext.Books.Where(x => x.Price > 80);
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }

            // 查找
            var b = myDbContext.Books.Single(x => x.Title == "零基础趣学C语言");
            Console.WriteLine(b.Price);

            // 排序
            books = myDbContext.Books.OrderBy(x => x.Price);
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title},{book.Price}");
            }

            // 分组
            var groups = myDbContext.Books
                .GroupBy(x => x.AuthorName)
                .Select(g => new // 创建投影
                {
                    AuthorName = g.Key,
                    BookCount = g.Count(),
                    MaxPrice = g.Max(y => y.Price)
                });
            foreach (var group in groups)
            {
                Console.WriteLine($"作者：{group.AuthorName},图书数量：{group.BookCount},最高价格：{group.MaxPrice}");
            }

            // 修改
            var sxzm = myDbContext.Books.Single(x => x.Title == "数学之美");
            sxzm.AuthorName = "Jun Wu";
            await myDbContext.SaveChangesAsync();
        }


        static async Task InitialTable()
        {
            await using var myDbContext = new MyDbContext();

            var b1 = new Book()
            {
                AuthorName = "杨中科",
                Title = "零基础趣学C语言",
                Price = 59.8,
                PubTime = new DateTime(2019, 3, 1)
            };
            var b2 = new Book()
            {
                AuthorName = "Robert Sedgewick",
                Title = "算法（第4版）",
                Price = 99,
                PubTime = new DateTime(2012, 10, 1)
            };

            var b3 = new Book()
            {
                AuthorName = "吴军",
                Title = "数学之美",
                Price = 69,
                PubTime = new DateTime(2020, 5, 1)
            };

            var b4 = new Book()
            {
                AuthorName = "杨中科",
                Title = "程序员的SQL金典",
                Price = 52,
                PubTime = new DateTime(2008, 9, 1),
            };

            var b5 = new Book()
            {
                AuthorName = "吴军",
                Title = "文明之光",
                Price = 246,
                PubTime = new DateTime(2017, 3, 1)
            };

            myDbContext.Add(b1);
            myDbContext.Add(b2);
            myDbContext.Add(b3);
            myDbContext.Add(b4);
            myDbContext.Add(b5);

            // 类似于Update-Database
            await myDbContext.SaveChangesAsync();
        }
    }
}
