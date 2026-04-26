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
    public class BlockContentAttachmentLookup_Config : IEntityTypeConfiguration<BlockContentAttachmentLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<BlockContentAttachmentLookup_MDL> builder)
        {
            builder.ToTable("BlockContentAttachmentLookup");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Path)
                .HasColumnName("Path")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.BlockContentId)
                .HasColumnName("BlockContentId")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
