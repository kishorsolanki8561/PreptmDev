using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Master
{
    public class BannerMaster_Config : IEntityTypeConfiguration<BannerMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<BannerMaster_MDL> builder)
        {
            builder.ToTable("BannerMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();
                

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar(max)")
                .IsRequired(); ;

            builder.Property(e => e.TitleHindi)
                .HasColumnName("TitleHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.URL)
                .HasColumnName("URL")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.AttachmentUrl)
                .HasColumnName("AttachmentUrl")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IsAdvt)
                .HasColumnName("IsAdvt")
                .HasColumnType("bit");

            builder.Property(e => e.DisplayOrder)
                .HasColumnName("DisplayOrder")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
