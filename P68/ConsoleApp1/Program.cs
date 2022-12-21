using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using MyDbContext myDbContext = new MyDbContext();

            //// 插入数据
            //InsertData(myDbContext);
            //myDbContext.SaveChanges();

            // 缩进打印
            var root = myDbContext.OrgUnits.Single(o => o.Parent == null);//寻找根节点
            Console.WriteLine(root.Name);
            PrintChildren(1, myDbContext, root);
        }

        private static void PrintChildren(int indentLevel, MyDbContext myDbContext, OrgUnit parent)
        {
            var children = myDbContext.OrgUnits.Where(o => o.Parent == parent);


            foreach (var child in children.ToList())
            //foreach (var child in children)  //只一句会出错：网上说不进行ToList得到的是IQueryable类型，这是延迟加载？？？
            {
                // 深度越深，添加的制表符越多
                var tabStrs = new string('\t', indentLevel);
                Console.WriteLine(tabStrs + child.Name);

                // 递归打印自己的子节点
                PrintChildren(indentLevel + 1, myDbContext, child);
            }
        }

        private static void InsertData(MyDbContext myDbContext)
        {
            // 建立节点
            OrgUnit ouRoot = new OrgUnit() { Name = "中科集团全球总部" };
            OrgUnit ouAsia = new OrgUnit() { Name = "中科集团亚太区总部" };
            OrgUnit ouAmerica = new OrgUnit() { Name = "中科集团美洲总部" };
            OrgUnit ouUsa = new OrgUnit() { Name = "中科美国" };
            OrgUnit ouCanada = new OrgUnit() { Name = "中科加拿大" };
            OrgUnit ouChina = new OrgUnit() { Name = "中科集团（中国）" };
            OrgUnit ouSingapore = new OrgUnit() { Name = "中科集团（新加坡）" };

            // 建立关系
            ouAsia.Parent = ouRoot;
            ouRoot.Children.Add(ouAsia);

            ouAmerica.Parent = ouRoot;
            ouRoot.Children.Add(ouAmerica);

            ouUsa.Parent = ouAmerica;
            ouAmerica.Children.Add(ouUsa);

            ouCanada.Parent = ouAmerica;
            ouAmerica.Children.Add(ouCanada);


            ouChina.Parent = ouAsia;
            ouAsia.Children.Add(ouChina);

            ouSingapore.Parent = ouAsia;
            ouAsia.Children.Add(ouSingapore);

            myDbContext.OrgUnits.Add(ouRoot);
            //myDbContext.OrgUnits.Add(ouAsia);
            //myDbContext.OrgUnits.Add(ouAmerica);
            //myDbContext.OrgUnits.Add(ouUsa);
            //myDbContext.OrgUnits.Add(ouCanada);
            //myDbContext.OrgUnits.Add(ouChina);
            //myDbContext.OrgUnits.Add(ouSingapore);
        }
    }
}
