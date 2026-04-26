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
    public class FrontUser_Config : IEntityTypeConfiguration<FrontUser_MDL>
    {
        public void Configure(EntityTypeBuilder<FrontUser_MDL> builder)
        {
            builder.ToTable("FrontUser");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UId)
                .HasColumnName("UId")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.LastName)
                .HasColumnName("LastName")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.MobileNumber)
                .HasColumnName("MobileNumber")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .HasColumnType("datetime");

            builder.Property(e => e.Language)
                .HasColumnName("Language")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ProfileImg)
                .HasColumnName("ProfileImg")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.StateId)
                .HasColumnName("StateId")
                .HasColumnType("integer");

            builder.Property(e => e.AuthToken)
                .HasColumnName("AuthToken")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Provider)
                .HasColumnName("Provider")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.FCMToken)
                .HasColumnName("FCMToken")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Platform)
                .HasColumnName("Platform")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IsPushNotification)
                .HasColumnName("IsPushNotification")
                .HasColumnType("bit");

            builder.Property(e => e.IsVerified)
                .HasColumnName("IsVerified")
                .HasColumnType("bit");
        }
    }

}
