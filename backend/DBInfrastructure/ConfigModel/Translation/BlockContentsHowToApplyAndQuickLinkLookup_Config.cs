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
    public class BlockContentsHowToApplyAndQuickLinkLookup_Config : IEntityTypeConfiguration<BlockContentsHowToApplyAndQuickLinkLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<BlockContentsHowToApplyAndQuickLinkLookup_MDL> builder)
        {
            builder.ToTable("BlockContentsHowToApplyAndQuickLinkLookup");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Title)
              .HasColumnName("Title")
              .HasColumnType("nvarchar(max)");

            builder.Property(e => e.TitleHindi)
                .HasColumnName("TitleHindi")
                .HasColumnType("nvarchar(max)");
            
            builder.Property(e => e.BlockContentId)
                .HasColumnName("BlockContentId")
                .HasColumnType("int");

            builder.Property(e => e.LinkUrl)
                .HasColumnName("LinkUrl")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IsQuickLink)
                .HasColumnName("IsQuickLink")
                .HasColumnType("bit");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindi)
                .HasColumnName("DescriptionHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IconClass)
                .HasColumnName("IconClass")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionJson)
                .HasColumnName("DescriptionJson")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindiJson)
                .HasColumnName("DescriptionHindiJson")
                .HasColumnType("nvarchar(max)");
        }
    }

}
