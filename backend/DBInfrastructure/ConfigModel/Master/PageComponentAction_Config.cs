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
    public class PageComponentAction_Config : IEntityTypeConfiguration<PageComponentAction_MDL>
    {
        public void Configure(EntityTypeBuilder<PageComponentAction_MDL> builder)
        {
            builder.ToTable("PageComponentAction");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.PageId)
                .HasColumnName("PageId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.ComponentId)
                .HasColumnName("ComponentId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.Action)
                .HasColumnName("Action")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
