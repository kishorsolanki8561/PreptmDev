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
    public class CategoryMaster_Config : IEntityTypeConfiguration<CategoryMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<CategoryMaster_MDL> builder)
        {
            builder.ToTable("CategoryMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)").IsRequired();

            builder.Property(e => e.Icon)
                .HasColumnName("Icon")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.NameHindi)
                .HasColumnName("NameHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.SlugUrl)
           .HasColumnName("SlugUrl")
           .HasColumnType("nvarchar(max)");
        }
    }

}
