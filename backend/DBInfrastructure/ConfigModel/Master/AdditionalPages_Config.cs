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
    public class AdditionalPages_Config : IEntityTypeConfiguration<AdditionalPages_MDL>
    {
        public void Configure(EntityTypeBuilder<AdditionalPages_MDL> builder)
        {
            builder.ToTable("AdditionalPages");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.PageType)
                .HasColumnName("PageType")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(e => e.Content)
                .HasColumnName("Content")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.ContentHindi)
                .HasColumnName("ContentHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ContentJson)
                .HasColumnName("ContentJson")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ContentHindiJson)
                .HasColumnName("ContentHindiJson")
                .HasColumnType("nvarchar(max)");
        }
    }
}
