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
    public class PageComponent_Config : IEntityTypeConfiguration<PageComponent_MDL>
    {
        public void Configure(EntityTypeBuilder<PageComponent_MDL> builder)
        {
            builder.ToTable("PageComponent");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.PageId)
                .HasColumnName("PageId")
                .HasColumnType("integer");
        }
    }

}
