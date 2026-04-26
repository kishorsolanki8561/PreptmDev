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
    public class SchemeAttchamentLookup_Config : IEntityTypeConfiguration<SchemeAttchamentLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<SchemeAttchamentLookup_MDL> builder)
        {
            builder.ToTable("SchemeAttachmentLookup");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer") // For int data type
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SchemeId)
                .HasColumnName("SchemeId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("varchar(255)");

            builder.Property(e => e.TitleHindi)
                .HasColumnName("TitleHindi")
                .HasColumnType("varchar(255)");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(max)");

            builder.Property(e => e.DescriptionHindi)
                .HasColumnName("DescriptionHindi")
                .HasColumnType("varchar(max)");

            builder.Property(e => e.Type)
                .HasColumnName("Type")
                .HasColumnType("integer");

            builder.Property(e => e.Path)
                .HasColumnName("Path")
                .HasColumnType("varchar(max)");
        }
    }

}
