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
    public class State_Config : IEntityTypeConfiguration<State_MDL>
    {
        public void Configure(EntityTypeBuilder<State_MDL> builder)
        {
            builder.ToTable("State");

            builder.HasKey(e => e.StateId);

            builder.Property(e => e.StateId)
                .HasColumnName("StateId")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.StateCode)
                .HasColumnName("StateCode")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            builder.Property(e => e.StateName)
                .HasColumnName("StateName")
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }

}
