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
    public class DepartmentMaster_Config : IEntityTypeConfiguration<DepartmentMaster_MDL>
    {
        public void Configure(EntityTypeBuilder<DepartmentMaster_MDL> builder)
        {
            builder.ToTable("DepartmentMaster");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(max)").IsRequired();

            builder.Property(e => e.NameHindi)
                .HasColumnName("NameHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ShortName)
           .HasColumnName("ShortName")
           .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Address)
        .HasColumnName("Address")
        .HasColumnType("nvarchar(max)");

            builder.Property(e => e.MapUrl)
      .HasColumnName("MapUrl")
      .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Email)
 .HasColumnName("Email")
 .HasColumnType("nvarchar(max)");

            builder.Property(e => e.PhoneNumber)
.HasColumnName("PhoneNumber")
.HasColumnType("nvarchar(max)");


            builder.Property(e => e.Logo)
.HasColumnName("Logo")
.HasColumnType("nvarchar(max)");


            builder.Property(e => e.Url)
.HasColumnName("Url")
.HasColumnType("nvarchar(max)");


            builder.Property(e => e.Description)
.HasColumnName("Description")
.HasColumnType("nvarchar(max)");


            builder.Property(e => e.DescriptionJson)
.HasColumnName("DescriptionJson")
.HasColumnType("nvarchar(max)");


            builder.Property(e => e.FaceBookLink)
.HasColumnName("FaceBookLink")
.HasColumnType("nvarchar(max)");

            builder.Property(e => e.TwitterLink)
.HasColumnName("TwitterLink")
.HasColumnType("nvarchar(max)");

            builder.Property(e => e.StateId)
.HasColumnName("StateId")
.HasColumnType("integer");


            builder.Property(e => e.AddressHindi)
.HasColumnName("AddressHindi")
.HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindi)
.HasColumnName("DescriptionHindi")
.HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindiJson)
.HasColumnName("DescriptionHindiJson")
.HasColumnType("nvarchar(max)");

            builder.Property(e => e.WikipediaEnglishUrl)
.HasColumnName("WikipediaEnglishUrl")
.HasColumnType("nvarchar(max)");

            builder.Property(e => e.WikipediaHindiUrl)
.HasColumnName("WikipediaHindiUrl")
.HasColumnType("nvarchar(max)");

            builder.Property(e => e.SlugUrl)
           .HasColumnName("SlugUrl")
           .HasColumnType("nvarchar(max)");
        }
    }
}
