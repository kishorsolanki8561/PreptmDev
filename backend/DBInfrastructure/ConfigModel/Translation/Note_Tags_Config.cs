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
    public class Note_Tags_Config : IEntityTypeConfiguration<NoteTags_MDL>
    {
        public void Configure(EntityTypeBuilder<NoteTags_MDL> builder)
        {
            builder.ToTable("NoteTags");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.TagId)
               .HasColumnName("TagId")
               .HasColumnType("integer");

            builder.Property(e => e.NoteId)
                .HasColumnName("NoteId")
                .HasColumnType("integer");

            builder.HasOne(e => e.Notes)
                     .WithMany(e => e.Note_Tags)
                       .HasForeignKey(e => e.NoteId);

        }
    }
}
