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
    public class RecruitmentJobDesignationLookup_Config : IEntityTypeConfiguration<RecruitmentJobDesignationLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<RecruitmentJobDesignationLookup_MDL> builder)
        {
            builder.ToTable("RecruitmentJobDesignationLookup");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer") // For int data type
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.JobDesignationId)
                .HasColumnName("JobDesignationId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.RecruitmentId)
                .HasColumnName("RecruitmentId")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
