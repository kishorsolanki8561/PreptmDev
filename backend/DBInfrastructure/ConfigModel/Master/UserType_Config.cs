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
    public class UserType_Config : IEntityTypeConfiguration<UserType_MDL>
    {
        public void Configure(EntityTypeBuilder<UserType_MDL> builder)
        {
            builder.ToTable("UserType");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TypeName)
                .HasColumnName("TypeName")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.IsDelete)
                .HasColumnName("IsDelete")
                .HasColumnType("bit")
                .IsRequired();
        }
    }

}
