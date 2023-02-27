namespace BLOG.DataAccess.Configuration
{
    using BLOG.Entities;

    using Microsoft.EntityFrameworkCore;

    public class ImageConfiguration : ConfigurationManagement<ImageEntity>
    {
        public ImageConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
            this.EntityConfiguration.Property(x => x.Base64Image).IsRequired(true).HasColumnType("longtext");
            this.EntityConfiguration.Property(x => x.Placeholder).IsRequired(true).HasColumnType("varchar(20)");
            this.EntityConfiguration.Property(x => x.IsThumbnail).IsRequired(true).HasColumnType("tinyint(1)");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(x => x.ArticleEntity).WithMany().IsRequired(true).HasForeignKey(x => x.ArticleEntityId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Images");
        }
    }
}