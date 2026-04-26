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
    public class Paper_Tags_Config : IEntityTypeConfiguration<PaperTags_MDL>
    {
        public void Configure(EntityTypeBuilder<PaperTags_MDL> builder)
        {
            builder.ToTable("PaperTags");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TagId)
               .HasColumnName("TagId")
               .HasColumnType("integer");

            builder.Property(e => e.PaperId)
                .HasColumnName("PaperId")
                .HasColumnType("integer");

            builder.HasOne(e => e.Papers)
            .WithMany(e => e.PaperTags)
            .HasForeignKey(e => e.PaperId);

        }
    }
}
