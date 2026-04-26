using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelService.MDL.Translation;

namespace DBInfrastructure.ConfigModel.Translation
{
    public class Article_Config : IEntityTypeConfiguration<Article_MDL>
    {
        public void Configure(EntityTypeBuilder<Article_MDL> builder)
        {
            builder.ToTable("Article");

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).HasColumnName("Title").HasColumnType("NVARCHAR(MAX)");

            builder.Property(e => e.TitleHindi).HasColumnName("TitleHindi").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.ArticleType).HasColumnName("ArticleType").HasColumnType("int");
            builder.Property(e => e.Summary).HasColumnName("Summary").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.SummaryHindi).HasColumnName("SummaryHindi").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.DescriptionHindi).HasColumnName("DescriptionHindi").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.DescriptionJson).HasColumnName("DescriptionJson").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.DescriptionJsonHindi).HasColumnName("DescriptionJsonHindi").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.Keywords).HasColumnName("Keywords").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.KeywordHindi).HasColumnName("KeywordHindi").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.Thumbnail).HasColumnName("Thumbnail").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.ThumbnailCredit).HasColumnName("ThumbnailCredit").HasColumnType("NVARCHAR(MAX)");
            builder.Property(e => e.VisitCount).HasColumnName("VisitCount").HasColumnType("int");
            builder.Property(e => e.PublisherId).HasColumnName("PublisherId").HasColumnType("int");
            builder.Property(e => e.PublisherDate).HasColumnName("PublisherDate").HasColumnType("datetime");
            builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("int");

        }
    }
}
