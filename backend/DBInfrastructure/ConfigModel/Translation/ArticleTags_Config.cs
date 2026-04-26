using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Translation
{
    public class ArticleTags_Config
    {
        public void Configure(EntityTypeBuilder<ArticleTags_MDL> builder)
        {
            builder.ToTable("ArticleTags");
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TagsId).HasColumnName("TagsId").HasColumnType("int");
            builder.Property(e => e.ArticleId).HasColumnName("ArticleId").HasColumnType("int");
        }
    }
}
