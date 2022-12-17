using Microsoft.EntityFrameworkCore;
using System;

namespace EfCoreOne2Many
{
    /// <summary>
    /// 3.创建DbContext
    /// </summary>
    public class MyDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = "server=localhost;user=root;password=root;database=ef";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

            // 配置连接字符串
            optionsBuilder.UseMySql(connectionString, serverVersion);

            //// 简单日志，将生成的SQL输出到控制台
            //optionsBuilder.LogTo(msg =>
            //{
            //    // 过滤不需要的消息
            //    if (!msg.Contains("CommandExecuting"))
            //    {
            //        return;
            //    }
            //    Console.WriteLine(msg);
            //});
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 从当前程序集加载所有的IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}