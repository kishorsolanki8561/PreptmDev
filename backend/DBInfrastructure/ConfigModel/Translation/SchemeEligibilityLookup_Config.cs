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
    public class SchemeEligibilityLookup_Config : IEntityTypeConfiguration<SchemeEligibilityLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<SchemeEligibilityLookup_MDL> builder)
        {
            // Configure the table name
            builder.ToTable("SchemeEligibilityLookup");

            // Configure the primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EligibilityId)
                .HasColumnName("EligibilityId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(e => e.SchemeId)
                .HasColumnName("SchemeId")
                .HasColumnType("int")
                .IsRequired();
        }
    }

}
