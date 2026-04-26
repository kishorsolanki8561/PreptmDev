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
    public class PageComponentPermission_Config : IEntityTypeConfiguration<PageComponentPermission_MDL>
    {
        public void Configure(EntityTypeBuilder<PageComponentPermission_MDL> builder)
        {
            builder.ToTable("PageComponentPermission");

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

            builder.Property(e => e.PageId)
                .HasColumnName("PageId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.PageComponentId)
                .HasColumnName("PageComponentId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.PageCompActionId)
                .HasColumnName("PageCompActionId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.UserTypeCode)
                .HasColumnName("UserTypeCode")
                .HasColumnType("integer");

            builder.Property(e => e.IsAllowed)
                .HasColumnName("IsAllowed")
                .HasColumnType("bit");
        }
    }

}
