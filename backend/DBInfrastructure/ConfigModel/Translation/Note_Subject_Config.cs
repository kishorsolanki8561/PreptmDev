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
    public class Note_Subject_Config : IEntityTypeConfiguration<Note_Subject_MDL>
    {
        public void Configure(EntityTypeBuilder<Note_Subject_MDL> builder)
        {
            builder.ToTable("NoteSubjects");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.NoteId)
                .HasColumnName("NoteId")
                .HasColumnType("integer");

            builder.HasOne(e => e.Notes)
                  .WithMany(e => e.Note_Subjects)
                    .HasForeignKey(e => e.NoteId);

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
