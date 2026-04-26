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
    public class AssetsMaster_Config : IEntityTypeConfiguration<AssetsMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<AssetsMaster_MDL> builder)
        {
            builder.ToTable("AssetsMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Path)
                .HasColumnName("Path")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DirectoryName)
                .HasColumnName("DirectoryName")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar(max)");
        }
    }

}
