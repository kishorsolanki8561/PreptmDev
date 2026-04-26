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
    public class MenuMasterMapping_Config : IEntityTypeConfiguration<MenuMasterMapping_MDL>
    {
        public void Configure(EntityTypeBuilder<MenuMasterMapping_MDL> builder)
        {
            builder.ToTable("MenuMasterMapping");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.MenuId)
                .HasColumnName("MenuId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.UserTypeCode)
                .HasColumnName("UserTypeCode")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
