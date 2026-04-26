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
    public class RecruitmentQualificationLookup_Config : IEntityTypeConfiguration<RecruitmentQualificationLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<RecruitmentQualificationLookup_MDL> builder)
        {
            builder.ToTable("RecruitmentQualificationLookup");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer") // For int data type
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.QualificationId)
                .HasColumnName("QualificationId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.RecruitmentId)
                .HasColumnName("RecruitmentId")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
