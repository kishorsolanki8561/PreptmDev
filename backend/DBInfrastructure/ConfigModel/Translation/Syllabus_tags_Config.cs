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
    public class Syllabus_tags_Config :IEntityTypeConfiguration<SyllabusTags_MDL>
    {
        public void Configure(EntityTypeBuilder<SyllabusTags_MDL> builder)
        {
            builder.ToTable("SyllabusTags");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TagId)
               .HasColumnName("TagId")
               .HasColumnType("integer");

            builder.Property(e => e.SyllabusId)
                .HasColumnName("SyllabusId")
                .HasColumnType("integer");


            builder.HasOne(e => e.Syllabus)
                     .WithMany(e => e.SyllabusTags)
                       .HasForeignKey(e => e.SyllabusId);

        }
    }
}
