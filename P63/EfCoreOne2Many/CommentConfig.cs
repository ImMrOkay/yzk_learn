using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreOne2Many
{
    public class CommentConfig:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments)
                .IsRequired() //Comment的Article属性不能为空
                .HasForeignKey(c => c.ArticleId);
                ;
            builder.Property(c=>c.Message).IsRequired().IsUnicode();
        }
    }
}