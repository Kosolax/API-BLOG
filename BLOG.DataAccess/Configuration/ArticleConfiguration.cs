namespace BLOG.DataAccess.Configuration
{
    using BLOG.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ArticleConfiguration : ConfigurationManagement<ArticleEntity>
    {
        public ArticleConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
            this.EntityConfiguration.Property(x => x.Title).IsRequired(true).HasColumnType("varchar(50)");
            this.EntityConfiguration.Property(x => x.Slug).IsRequired(true).HasColumnType("varchar(20)");
            this.EntityConfiguration.Property(x => x.Content).IsRequired(true).HasColumnType("longtext");
            this.EntityConfiguration.Property(x => x.Description).IsRequired(true).HasColumnType("varchar(255)");
            this.EntityConfiguration.Property(x => x.CreatedDate).IsRequired(true).HasColumnType("datetime(6)");
            this.EntityConfiguration.Property(x => x.UpdatedDate).IsRequired(false).HasColumnType("datetime(6)");
            this.EntityConfiguration.Property(x => x.Views).IsRequired(true).HasColumnType("integer");
            this.EntityConfiguration.Property(x => x.IsDeleted).IsRequired(true).HasColumnType("tinyint(1)");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Articles");
        }
    }
}