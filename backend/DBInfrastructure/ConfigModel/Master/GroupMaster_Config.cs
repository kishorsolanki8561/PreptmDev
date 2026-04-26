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
    public class GroupMaster_Config : IEntityTypeConfiguration<GroupMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<GroupMaster_MDL> builder)
        {
            builder.ToTable("GroupMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.NameHindi)
                .HasColumnName("NameHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.SlugUrl)
            .HasColumnName("SlugUrl")
            .HasColumnType("nvarchar(max)");
        }
    }

}
