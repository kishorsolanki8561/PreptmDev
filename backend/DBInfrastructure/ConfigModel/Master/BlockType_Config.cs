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
    public class BlockType_Config : IEntityTypeConfiguration<BlockType_MDL>
    {
        public void Configure(EntityTypeBuilder<BlockType_MDL> builder)
        {
            builder.ToTable("BlockType");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)").IsRequired();

            builder.Property(e => e.NameHindi)
                .HasColumnName("NameHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ForRecruitment)
                .HasColumnName("ForRecruitment")
                .HasColumnType("bit");

            builder.Property(e => e.ModifiedSideMapDate)
                .HasColumnName("ModifiedSideMapDate")
                .HasColumnType("datetime");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindi)
                .HasColumnName("DescriptionHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.SlugUrl)
       .HasColumnName("SlugUrl")
       .HasColumnType("nvarchar(max)");
        }
    }

}
