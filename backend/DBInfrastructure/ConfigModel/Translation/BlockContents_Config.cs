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
    public class BlockContents_Config : IEntityTypeConfiguration<BlockContents_MDL>
    {

        public void Configure(EntityTypeBuilder<BlockContents_MDL> builder)
        {
            builder.ToTable("BlockContents");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.BlockTypeId)
                .HasColumnName("BlockTypeId")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.RecruitmentId)
                .HasColumnName("RecruitmentId")
                .HasColumnType("integer");

            builder.Property(e => e.DepartmentId)
                .HasColumnName("DepartmentId")
                .HasColumnType("integer");

            builder.Property(e => e.CategoryId)
                .HasColumnName("CategoryId")
                .HasColumnType("integer");

            builder.Property(e => e.SlugUrl)
                .HasColumnName("SlugUrl")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Url)
                .HasColumnName("Url")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.HowTo)
                .HasColumnName("HowTo")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.PublishedDate)
                .HasColumnName("PublishedDate")
                .HasColumnType("datetime");

            builder.Property(e => e.PublisherId)
                .HasColumnName("PublisherId")
                .HasColumnType("integer");

            builder.Property(e => e.VisitCount)
                .HasColumnName("VisitCount")
                .HasColumnType("integer");

            builder.Property(e => e.Status)
                .HasColumnName("Status")
                .HasColumnType("integer");

            builder.Property(e => e.SortLinks)
                .HasColumnName("SortLinks")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Date)
                .HasColumnName("Date")
                .HasColumnType("datetime");

            builder.Property(e => e.StateId)
                .HasColumnName("StateId")
                .HasColumnType("integer");

            builder.Property(e => e.GroupId)
                .HasColumnName("GroupId")
                .HasColumnType("integer");

            builder.Property(e => e.SubCategoryId)
                .HasColumnName("SubCategoryId")
                .HasColumnType("integer");

            builder.Property(e => e.Keywords)
                .HasColumnName("Keywords")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.OtherLinks)
                .HasColumnName("OtherLinks")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.NotificationLink)
                .HasColumnName("NotificationLink")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.TitleHindi)
                .HasColumnName("TitleHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindi)
                .HasColumnName("DescriptionHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Summary)
                .HasColumnName("Summary")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.LastDate)
                .HasColumnName("LastDate")
                .HasColumnType("datetime");

            builder.Property(e => e.ExtendedDate)
                .HasColumnName("ExtendedDate")
                .HasColumnType("datetime");

            builder.Property(e => e.FeePaymentLastDate)
                .HasColumnName("FeePaymentLastDate")
                .HasColumnType("datetime");

            builder.Property(e => e.CorrectionLastDate)
                .HasColumnName("CorrectionLastDate")
                .HasColumnType("datetime");

            builder.Property(e => e.UrlLabelId)
                .HasColumnName("UrlLabelId")
                .HasColumnType("integer");

            builder.Property(e => e.ExamMode)
                .HasColumnName("ExamMode")
                .HasColumnType("integer");

            builder.Property(e => e.Thumbnail)
                .HasColumnName("Thumbnail")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.SocialMediaUrl)
                .HasColumnName("SocialMediaUrl")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ThumbnailCredit)
                .HasColumnName("ThumbnailCredit")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.IsCompleted)
                .HasColumnName("IsCompleted")
                .HasColumnType("bit");

            builder.Property(e => e.IsExpired)
                .HasColumnName("IsExpired")
                .HasColumnType("bit");

            builder.Property(e => e.ShouldReminder)
                .HasColumnName("ShouldReminder")
                .HasColumnType("datetime");

            builder.Property(e => e.ReminderDescription)
                .HasColumnName("ReminderDescription")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.UpcomingCalendarCode)
                .HasColumnName("UpcomingCalendarCode")
                .HasColumnType("int");

            builder.Property(e => e.DescriptionJson)
                .HasColumnName("DescriptionJson")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DescriptionHindiJson)
                .HasColumnName("DescriptionHindiJson")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.KeywordsHindi)
                .HasColumnName("KeywordsHindi")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.SummaryHindi)
                .HasColumnName("SummaryHindi")
                .HasColumnType("nvarchar(max)");
        }
    }

}
