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
    public class UserFeedback_Config : IEntityTypeConfiguration<UserFeedback_MDL>
    {
        public void Configure(EntityTypeBuilder<UserFeedback_MDL> builder)
        {
            builder.ToTable("UserFeedback");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasColumnName("UserId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("Status")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(e => e.Type)
                .HasColumnName("Type")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(e => e.Message)
                .HasColumnName("Message")
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

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
