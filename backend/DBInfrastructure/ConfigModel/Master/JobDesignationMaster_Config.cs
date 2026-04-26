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
    public class JobDesignationMaster_Config : IEntityTypeConfiguration<JobDesignationMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<JobDesignationMaster_MDL> builder)
        {
            builder.ToTable("JobDesignationMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.CreateBy)
                .HasColumnName("CreateBy")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.NameHindi)
                .HasColumnName("NameHindi")
                .HasColumnType("nvarchar(max)");
        }
    }

}
