using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMySql
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await InitialTable(); //向数据库添加数据

            // myDbContext 逻辑上的数据库
            await using var myDbContext = new MyDbContext();
            //Dog dog = new Dog();
            //dog.Name = "Trump";
            //myDbContext.Dogs.Add(dog);           // 逻辑上的表中插入一条记录
            //await myDbContext.SaveChangesAsync();// 相当于 Update-DataBase


            // 筛选
            Console.WriteLine("价格大于80的书：");
            var books = myDbContext.Books.Where(x => x.Price > 80);
            Console.WriteLine("对应的SQL语句：");

            // 获取SQL语句的另一个方法是：简单日志加过滤器，在MyDbContext类里配置
            // books.ToQueryString()实际上不需要真的执行查询就能获取SQL语句
            // 但是只能用于查询语句，其他的不能
            Console.WriteLine(books.ToQueryString()); 
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title}");
            }





            // 查找
            var b = myDbContext.Books.Single(x => x.Title == "零基础趣学C语言");
            Console.WriteLine($"《零基础趣学C语言》的价格：{b.Price}");

            // 排序
            Console.WriteLine("价格升序排序：");
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
            
            await myDbContext.SaveChangesAsync();// 相当于 Update-DataBase
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
