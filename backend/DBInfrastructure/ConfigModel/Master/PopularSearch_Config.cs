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
    public class PopularSearch_Config : IEntityTypeConfiguration<PopularSearch_MDL>
    {
        public void Configure(EntityTypeBuilder<PopularSearch_MDL> builder)
        {
            builder.ToTable("PopularSearch");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SearchText)
                .HasColumnName("SearchText")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.HitCount)
                .HasColumnName("HitCount")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.StateId)
                .HasColumnName("StateId")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
