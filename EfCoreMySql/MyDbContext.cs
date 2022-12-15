using Microsoft.EntityFrameworkCore;
using System;

namespace EfCoreMySql
{
    /// <summary>
    /// 3.创建DbContext
    /// </summary>
    public class MyDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = "server=localhost;user=root;password=root;database=ef";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

            // 配置连接字符串
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 从当前程序集加载所有的IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}