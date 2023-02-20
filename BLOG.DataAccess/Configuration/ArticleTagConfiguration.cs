namespace BLOG.DataAccess.Configuration
{
    using BLOG.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ArticleTagConfiguration : ConfigurationManagement<ArticleTagEntity>
    {
        public ArticleTagConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(x => x.ArticleEntity).WithMany().IsRequired(true).HasForeignKey(x => x.ArticleEntityId).OnDelete(DeleteBehavior.Cascade);
            this.EntityConfiguration.HasOne(x => x.TagEntity).WithMany().IsRequired(true).HasForeignKey(x => x.TagEntityId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("ArticlesTags");
        }
    }
}