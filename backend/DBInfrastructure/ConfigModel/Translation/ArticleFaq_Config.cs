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
    public class ArticleFaq_Config : IEntityTypeConfiguration<ArticleFaq_MDL>
    {
        public void Configure(EntityTypeBuilder<ArticleFaq_MDL> builder)
        {
            builder.ToTable("ArticleFaq");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ArticleId)
                .HasColumnName("ArticleId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.Que)
                .HasColumnName("Que")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false); // Nullable

            builder.Property(e => e.Ans)
                .HasColumnName("Ans")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false); // Nullable

            builder.Property(e => e.QueHindi)
                .HasColumnName("QueHindi")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false); // Nullable

            builder.Property(e => e.AnsHindi)
                .HasColumnName("AnsHindi")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false); // Nullable
        }
    }

}
