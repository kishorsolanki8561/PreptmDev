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
    public class Scheme_Config : IEntityTypeConfiguration<Scheme_MDL>
    {
        public void Configure(EntityTypeBuilder<Scheme_MDL> builder)
        {
            builder.ToTable("Scheme");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer") // For int data type
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar(255)") // Adjust size if needed
                .IsRequired();

            builder.Property(e => e.TitleHindi)
                .HasColumnName("TitleHindi")
                .HasColumnType("nvarchar(255)") // Adjust size if needed
                .IsRequired();

            builder.Property(e => e.DepartmentId)
                .HasColumnName("DepartmentId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.StateId)
                .HasColumnName("StateId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.MinAge)
                .HasColumnName("MinAge")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.MaxAge)
                .HasColumnName("MaxAge")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.StartDate)
                .HasColumnName("StartDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.EndDate)
                .HasColumnName("EndDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ExtendedDate)
                .HasColumnName("ExtendedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.CorrectionLastDate)
                .HasColumnName("CorrectionLastDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.PostponeDate)
                .HasColumnName("PostponeDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.LevelType)
                .HasColumnName("LevelType")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.Mode)
                .HasColumnName("Mode")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.OfficelLink)
                .HasColumnName("OfficelLink")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ApplyLink)
                .HasColumnName("ApplyLink")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ShortDescription)
                .HasColumnName("ShortDescription")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ShortDescriptionHindi)
                .HasColumnName("ShortDescriptionHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Keywords)
                .HasColumnName("Keywords")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindi)
                .HasColumnName("DescriptionHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.HowtoApply)
                .HasColumnName("HowtoApply")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.OtherLinks)
                .HasColumnName("OtherLinks")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Thumbnail)
                .HasColumnName("Thumbnail")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Slug)
                .HasColumnName("Slug")
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.ContactDetail)
                .HasColumnName("ContactDetail")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Fee)
                .HasColumnName("Fee")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit") // For boolean data type
                .IsRequired();

            builder.Property(e => e.IsDelete)
                .HasColumnName("IsDelete")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasColumnType("integer");

            builder.Property(e => e.Status)
                .HasColumnName("Status")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.VisitCount)
                .HasColumnName("VisitCount")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.SortLinks)
                .HasColumnName("SortLinks")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.PublisherId)
                .HasColumnName("PublisherId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.PublishedDate)
                .HasColumnName("PublishedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.BlockTypeCode)
                .HasColumnName("BlockTypeCode")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.SocialMediaUrl)
                .HasColumnName("SocialMediaUrl")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ThumbnailCredit)
                .HasColumnName("ThumbnailCredit")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IsCompleted)
                .HasColumnName("IsCompleted")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.IsExpired)
                .HasColumnName("IsExpired")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.ShouldReminder)
                .HasColumnName("ShouldReminder")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ReminderDescription)
                .HasColumnName("ReminderDescription")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.UpcomingCalendarCode)
                .HasColumnName("UpcomingCalendarCode")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.DescriptionJson)
                .HasColumnName("DescriptionJson")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindiJson)
                .HasColumnName("DescriptionHindiJson")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.KeywordsHindi)
                .HasColumnName("KeywordsHindi")
                .HasColumnType("nvarchar(max)");
        }
    }

}
