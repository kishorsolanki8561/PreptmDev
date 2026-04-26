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
    public class MenuMaster_Config : IEntityTypeConfiguration<MenuMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<MenuMaster_MDL> builder)
        {
            builder.ToTable("MenuMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.MenuName)
                .HasColumnName("MenuName")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DisplayName)
                .HasColumnName("DisplayName")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.HasChild)
                .HasColumnName("HasChild")
                .HasColumnType("bit");

            builder.Property(e => e.ParentId)
                .HasColumnName("ParentId")
                .HasColumnType("bigint");

            builder.Property(e => e.Position)
                .HasColumnName("Position")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(e => e.IconClass)
                .HasColumnName("IconClass")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit");

            builder.Property(e => e.IsDelete)
                .HasColumnName("IsDelete")
                .HasColumnType("bit");

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("datetime");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasColumnType("bigint");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasColumnType("bigint")
                .IsRequired();
        }
    }

}
