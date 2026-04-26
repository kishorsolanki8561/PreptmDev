using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class SchemeContactDetailsLookup_Config : IEntityTypeConfiguration<SchemeContactDetailsLookup_MDL>
    {
        public void Configure(EntityTypeBuilder<SchemeContactDetailsLookup_MDL> builder)
        {
            // Configure the table name
            builder.ToTable("SchemeContactDetailsLookup");

            // Configure the primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DepartmentId)
                .HasColumnName("DepartmentId")
                .HasColumnType("int");

            builder.Property(e => e.SchemeId)
                .HasColumnName("SchemeId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(e => e.NodalOfficerName)
                .HasColumnName("NodalOfficerName")
                .HasColumnType("nvarchar(Max)");

            builder.Property(e => e.NodalOfficerNameHindi)
                .HasColumnName("NodalOfficerNameHindi")
                .HasColumnType("nvarchar(Max)"); 

            builder.Property(e => e.PhoneNo)
                .HasColumnName("PhoneNo")
                .HasColumnType("nvarchar(Max)"); // Adjust size based on phone number format

            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(Max)"); // Adjust size if needed
        }
    }

}
