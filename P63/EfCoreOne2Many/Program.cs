using System;
using System.Linq;

namespace EfCoreOne2Many
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                #region P63 关系配置
                // Init(myDbContext);

                #endregion

                #region P64 关联数据的获取

                //// 获取关联数据

                ////// 下面这种做法时获取不到的关联的数据
                //// var a = myDbContext.Articles.Single(a => a.Id == 4);
                //// Console.WriteLine(a.Title);


                //// Include 正向查询
                //var a = myDbContext.Articles.Include(a => a.Comments).Single(a => a.Id == 4);
                //Console.WriteLine(a.Title);
                //foreach (var comment in a.Comments)
                //{
                //    Console.WriteLine(comment.Message);
                //}

                //// Include 反向查询
                //var c = myDbContext.Comments.Include(c => c.Article).Single(c => c.Id == 8);
                //Console.WriteLine(c.Message);
                //Console.WriteLine($"文章ID:{c.Article.Id}，文章标题：{c.Article.Title}");
                #endregion

                #region P65 获取外键的值

                // 1. 通过Select获取只需要的列
                var article = myDbContext.Articles.Select(a => new { a.Id, a.Title }).Single(a => a.Id == 4);
                //// 下面这种方式也可以实现这个效果，
                //// 但是生成的SQL语句会将所有的字段都查询出来，效率不如上面的这种方式高
                //var article = myDbContext.Articles.Single(a => a.Id == 4);
                Console.WriteLine(article.Title);

                // 2.获取外键的值的方法
                // 第一步：在Comment中新增ArticleId字段（与不添加这个字段时数据库自动生成的外键字段名称一致）
                // 第二步：在CommentConfig中配置HasForeignKey(c=>c.ArticleId)
                var c = myDbContext.Comments.Single(c => c.Id == 8);
                Console.WriteLine($"外键的值为：{c.ArticleId}");

                #endregion

                #region P71 基于关系的复杂查询

                var articles = myDbContext.Comments.Where(c => c.Message.Contains("微软")).Select(c2 => c2.Article).Distinct();
                foreach (var article1 in articles)
                {
                    Console.WriteLine($"{article1.Id},{article1.Title}");
                }
                #endregion

            }
        }

        private static void Init(MyDbContext myDbContext)
        {
            Article a1 = new Article();
            a1.Title = "微软发布了.NET 7 大版本的首个预览";
            a1.Content = "微软昨日在官网博客中宣布了 .NET 6 首个预览版本的到来. Bla bla ...";

            // 注意：Comment并没有指定Article属性，在执行a1.Comments.Add后Ef core 会自动建立关系
            Comment c1 = new Comment() { Message = "太牛了..." /*,Article = a1*/ };
            Comment c2 = new Comment() { Message = "更新太快了吧 ..." /*,Article = a1*/ };
            Comment c3 = new Comment() { Message = "支持 ..." /*,Article = a1*/ };
            a1.Comments.Add(c1);
            a1.Comments.Add(c2);
            a1.Comments.Add(c3);

            // 将a1加入逻辑数据库
            myDbContext.Articles.Add(a1);

            // 注意：只要把父表加入，子表也会自动地加入到数据库，不需要写下面的这些 
            //myDbContext.Comments.Add(c1);
            //myDbContext.Comments.Add(c2);

            myDbContext.SaveChanges();
        }
    }
}
