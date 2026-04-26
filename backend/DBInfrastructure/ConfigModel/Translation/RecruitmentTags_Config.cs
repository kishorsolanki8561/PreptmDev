using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Translation
{
   
    public class RecruitmentTags_Config : IEntityTypeConfiguration<RecruitmentTags_MDL>
    {
        public void Configure(EntityTypeBuilder<RecruitmentTags_MDL> builder)
        {
            builder.ToTable("RecruitmentTags");

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TagsId).HasColumnName("TagsId").HasColumnType("int");
            builder.Property(e => e.RecruitmentId).HasColumnName("RecruitmentId").HasColumnType("int");
        }
    }
}
