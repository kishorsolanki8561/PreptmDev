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
    public class SubCategory_Config : IEntityTypeConfiguration<SubCategory_MDL>
    {
        public void Configure(EntityTypeBuilder<SubCategory_MDL> builder)
        {
            builder.ToTable("SubCategory");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.CategoryId)
                .HasColumnName("CategoryId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.Icon)
                .HasColumnName("Icon")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.SlugUrl)
                .HasColumnName("SlugUrl")
                .HasColumnType("nvarchar(max)");
        }
    }

}
