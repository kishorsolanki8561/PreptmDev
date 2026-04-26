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
    public class Bookmark_Config : IEntityTypeConfiguration<Bookmark_MDL>
    {
        public void Configure(EntityTypeBuilder<Bookmark_MDL> builder)
        {
            builder.ToTable("Bookmark");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.PostId)
                .HasColumnName("PostId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.UserId)
                .HasColumnName("UserId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.ModuleEnum)
                .HasColumnName("ModuleEnum")
                .HasColumnType("integer")
                .IsRequired();
        }
    }

}
