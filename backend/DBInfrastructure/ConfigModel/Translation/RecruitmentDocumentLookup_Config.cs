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
    public class RecruitmentDocumentLookup_Config : IEntityTypeConfiguration<RecruitmentDocumentLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<RecruitmentDocumentLookup_MDL> builder)
        {
            builder.ToTable("RecruitmentDocumentLookup");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Path)
                .HasColumnName("Path")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.RecruitmentId)
                .HasColumnName("RecruitmentId")
                .HasColumnType("bigint")
                .IsRequired();
        }
    }
}
