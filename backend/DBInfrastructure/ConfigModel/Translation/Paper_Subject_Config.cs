using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelService.MDL.Translation;

namespace DBInfrastructure.ConfigModel.Translation
{
    public class Paper_Subject_Config : IEntityTypeConfiguration<PaperSubject_MDL>
    {
        public void Configure(EntityTypeBuilder<PaperSubject_MDL> builder)
        {
            builder.ToTable("PaperSubjects");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
             .HasColumnName("Id")
             .HasColumnType("integer")
             .IsRequired()
             .ValueGeneratedOnAdd();

            builder.Property(e => e.PaperId)
                .HasColumnName("PaperId")
                .HasColumnType("integer");

            builder.HasOne(e => e.Papers)
              .WithMany(e => e.Paper_Subjects)
              .HasForeignKey(e => e.PaperId);

            builder.Property(e => e.SubjectName)
                .HasColumnName("SubjectName")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(e => e.YearId)
                .HasColumnName("YearId")
                .HasColumnType("integer");

            builder.Property(e => e.SubjectNameHindi)
                .HasColumnName("SubjectNameHindi")
                .HasColumnType("nvarchar(150)");

            builder.Property(e => e.Path)
                .HasColumnName("Path")
                .HasColumnType("nvarchar(Max)");
        }
    }
}
