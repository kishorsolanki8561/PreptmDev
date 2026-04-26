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
    public class SiteMap_Config : IEntityTypeConfiguration<SiteMap_MDL>
    {
        public void Configure(EntityTypeBuilder<SiteMap_MDL> builder)
        {
            // Configure the table name
            builder.ToTable("SiteMap");

            // Configure the primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SlugUrl)
                .HasColumnName("SlugUrl")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ModuleName)
                .HasColumnName("ModuleName")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(e => e.ModuleNameHindi)
                .HasColumnName("ModuleNameHindi")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
    }

}
