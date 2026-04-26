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
    public class User_Config : IEntityTypeConfiguration<User_MDL>
    {
        public void Configure(EntityTypeBuilder<User_MDL> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.IsAutoLoggedOut)
                .HasColumnName("IsAutoLoggedOut")
                .HasColumnType("bit");

            builder.Property(e => e.Password)
                .HasColumnName("Password")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.UserTypeCode)
                .HasColumnName("UserTypeCode")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.IsDelete)
                .HasColumnName("IsDelete")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }

}
