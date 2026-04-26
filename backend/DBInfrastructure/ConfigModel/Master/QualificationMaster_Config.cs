using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Master
{
    public class QualificationMaster_Config : IEntityTypeConfiguration<QualificationMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<QualificationMaster_MDL> builder)
        {
            builder.ToTable("QualificationMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.Isdelete)
                .HasColumnName("Isdelete")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.TitleHindi)
                .HasColumnName("TitleHindi")
                .HasColumnType("nvarchar(max)");
        }
    }

}
