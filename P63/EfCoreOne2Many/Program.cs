namespace EfCoreOne2Many
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                Article a1 = new Article();
                a1.Title = "A";
                a1.Content = "Bla bla ...";

                // 注意：Comment并没有指定Article属性，在执行a1.Comments.Add后Ef core 会自动建立关系
                Comment c1 = new Comment() { Message = "c1..." /*,Article = a1*/};
                Comment c2 = new Comment() { Message = "c2 ..." /*,Article = a1*/};
                a1.Comments.Add(c1);
                a1.Comments.Add(c2);

                // 将a1加入逻辑数据库
                myDbContext.Articles.Add(a1);

                // 注意：只要把父表加入，子表也会自动地加入到数据库，不需要写下面的这些 
                //myDbContext.Comments.Add(c1);
                //myDbContext.Comments.Add(c2);

                myDbContext.SaveChanges();

            }
        }
    }
}
