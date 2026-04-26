using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Translation
{
    public class SchemeHowToApplyAndQuickLinkLookup_Config : IEntityTypeConfiguration<SchemeHowToApplyAndQuickLinkLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<SchemeHowToApplyAndQuickLinkLookup_MDL> builder)
        {
            // Configure the table name
            builder.ToTable("SchemeHowToApplyAndQuickLinkLookup");

            // Configure the primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SchemeId)
                .HasColumnName("SchemeId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.TitleHindi)
                .HasColumnName("TitleHindi")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.LinkUrl)
                .HasColumnName("LinkUrl")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.IsQuickLink)
                .HasColumnName("IsQuickLink")
                .HasColumnType("bit")
                .IsRequired(true);

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.DescriptionHindi)
                .HasColumnName("DescriptionHindi")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.IconClass)
                .HasColumnName("IconClass")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.DescriptionJson)
                .HasColumnName("DescriptionJson")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.DescriptionHindiJson)
                .HasColumnName("DescriptionHindiJson")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
    }

}
