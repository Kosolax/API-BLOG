namespace BLOG.DataAccess.Configuration
{
    using BLOG.Entities;

    using Microsoft.EntityFrameworkCore;

    public class TagConfiguration : ConfigurationManagement<TagEntity>
    {
        public TagConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
            this.EntityConfiguration.Property(x => x.Name).IsRequired(false).HasColumnType("varchar(255)");
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
            this.EntityConfiguration.ToTable("Tags");
        }
    }
}