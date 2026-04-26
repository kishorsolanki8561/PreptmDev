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
    public class Syllabus_Subject_Config : IEntityTypeConfiguration<Syllabus_Subject_MDL>
    {
        public void Configure(EntityTypeBuilder<Syllabus_Subject_MDL> builder)
        {
            builder.ToTable("SyllabusSubjects");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
             .HasColumnName("Id")
             .HasColumnType("integer")
             .IsRequired()
             .ValueGeneratedOnAdd();

            builder.Property(e => e.SyllabusId)
                .HasColumnName("SyllabusId")
                .HasColumnType("integer");


            builder.HasOne(e => e.Syllabus)
      .WithMany(e => e.Syllabus_Subjects)
        .HasForeignKey(e => e.SyllabusId);

            builder.Property(e => e.SubjectName)
                .HasColumnName("SubjectName")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(e => e.YearId)
                .HasColumnName("YearId")
                .HasColumnType("integer");

            builder.Property(e => e.SubjectNameHindi)
                .HasColumnName("SubjectNameHindi")
                .HasColumnType("nvarchar(150)");

            builder.Property(e => e.Path)
                .HasColumnName("Path")
                .HasColumnType("nvarchar(Max)");
        }
    }
}
