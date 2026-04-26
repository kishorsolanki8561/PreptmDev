using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Translation
{
    public class Paper_Config : IEntityTypeConfiguration<Paper_MDL>
    {
        public void Configure(EntityTypeBuilder<Paper_MDL> builder)
        {
            builder.ToTable("Papers");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

           builder.HasMany(e => e.Paper_Subjects)
            .WithOne(e => e.Papers)
            .HasForeignKey(e => e.PaperId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.PaperTags)
            .WithOne(e => e.Papers)
            .HasForeignKey(e => e.PaperId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.CategoryId)
                .HasColumnName("CategoryId")
                .HasColumnType("integer");

            builder.Property(e => e.DepartmentId)
                .HasColumnName("DepartmentId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.QualificationId)
                .HasColumnName("QualificationId")
                .HasColumnType("integer");

            builder.Property(e => e.StateId)
                .HasColumnName("StateId")
                .HasColumnType("integer");

            builder.Property(e => e.Title)
            .HasColumnName("Title")
              .HasColumnType("nvarchar(250)");

            builder.Property(e => e.TitleHindi)
                   .HasColumnName("TitleHindi")
                   .HasColumnType("nvarchar(250)");

            builder.Property(e => e.SlugUrl)
                .HasColumnName("SlugUrl")
                 .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.DescriptionHindi)
                .HasColumnName("DescriptionHindi")
                .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.Keywords)
                .HasColumnName("Keywords")
                .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.KeywordsHindi)
                .HasColumnName("KeywordsHindi")
                .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.DescriptionJson)
                .HasColumnName("DescriptionJson")
                .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.DescriptionJsonHindi)
                .HasColumnName("DescriptionJsonHindi")
                .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.VisitCount)
                .HasColumnName("VisitCount")
                .HasColumnType("integer");

            builder.Property(e => e.PublisherId)
              .HasColumnName("PublisherId")
              .HasColumnType("integer");

            builder.Property(e => e.PublisherDate)
              .HasColumnName("PublisherDate")
              .HasColumnType("DateTime");

            builder.Property(e => e.Status)
           .HasColumnName("Status")
           .HasColumnType("integer");

            builder.Property(e => e.ShortDescription)
.HasColumnName("ShortDescription")
.HasColumnType("nvarchar(Max)");

            builder.Property(e => e.ShortDescriptionHindi)
            .HasColumnName("ShortDescriptionHindi")
            .HasColumnType("nvarchar(Max)");
        }
    }
}
