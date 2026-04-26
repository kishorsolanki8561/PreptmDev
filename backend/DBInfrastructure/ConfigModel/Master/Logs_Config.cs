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
    public class Logs_Config : IEntityTypeConfiguration<Logs_MDL>
    {
        public void Configure(EntityTypeBuilder<Logs_MDL> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(e => e.LogId);

            builder.Property(e => e.LogId)
                .HasColumnName("LogId")
                .HasColumnType("bigint")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.LogLevel)
                .HasColumnName("LogLevel")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(e => e.NewLine)
                .HasColumnName("NewLine")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.SourceContext)
                .HasColumnName("SourceContext")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.CorrelationId)
                .HasColumnName("CorrelationId")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ProcessName)
                .HasColumnName("ProcessName")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ProcessId)
                .HasColumnName("ProcessId")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(e => e.ThreadId)
                .HasColumnName("ThreadId")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(e => e.MachineName)
                .HasColumnName("MachineName")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ClientIp)
                .HasColumnName("ClientIp")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.EnvironmentName)
                .HasColumnName("EnvironmentName")
                .HasColumnType("nvarchar(max)");
        }
    }

}
