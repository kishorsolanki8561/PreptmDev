using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Translation
{
    public class BlockContentTags_Config : IEntityTypeConfiguration<BlockContentsTags_MDL>
    {
        public void Configure(EntityTypeBuilder<BlockContentsTags_MDL> builder)
        {
            builder.ToTable("BlockContentTags");
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TagsId).HasColumnName("TagsId").HasColumnType("int");
            builder.Property(e => e.BlockContentId).HasColumnName("BlockContentId").HasColumnType("int");
        }
    }
}
