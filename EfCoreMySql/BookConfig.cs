using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreMySql
{
    /// <summary>
    /// 2.建立配置
    ///
    /// 原则：约定大于配置
    /// 即可以完全不用对实体Book做任何的配置，框架可以根据约定的规则自动地将数据库建好
    /// 也就是说：这个类可以不需要
    /// </summary>
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("T_Books");

            // 设置字段最大长度
            builder.Property(e => e.Title).HasMaxLength(50).IsRequired();       
            builder.Property(e => e.AuthorName).HasMaxLength(20).IsRequired();  
        }
    }
}
