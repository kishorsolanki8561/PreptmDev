using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBInfrastructure.Migrations
{
    public partial class CreateNewMigration_20240929_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    TitleHindi = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ArticleType = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    SummaryHindi = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    DescriptionHindi = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    DescriptionJson = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    DescriptionJsonHindi = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Keywords = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    KeywordHindi = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Thumbnail = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    ThumbnailCredit = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    TagId = table.Column<int>(type: "int", nullable: true),
                    VisitCount = table.Column<int>(type: "int", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    PublisherDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    SlugUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleFaq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    Que = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnsHindi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleFaq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockContentAttachmentLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockContentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockContentAttachmentLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockTypeId = table.Column<int>(type: "integer", nullable: false),
                    RecruitmentId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlugUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PublisherId = table.Column<int>(type: "integer", nullable: true),
                    VisitCount = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    SortLinks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    GroupId = table.Column<int>(type: "integer", nullable: true),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherLinks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExtendedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FeePaymentLastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CorrectionLastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UrlLabelId = table.Column<int>(type: "integer", nullable: true),
                    ExamMode = table.Column<int>(type: "integer", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialMediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailCredit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: true),
                    ShouldReminder = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReminderDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpcomingCalendarCode = table.Column<int>(type: "int", nullable: true),
                    DescriptionJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindiJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeywordsHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummaryHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockContentsHowToApplyAndQuickLinkLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockContentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQuickLink = table.Column<bool>(type: "bit", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindiJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockContentsHowToApplyAndQuickLinkLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockContentTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    BlockContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockContentTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    BlockTypeId = table.Column<int>(type: "integer", nullable: false),
                    Que = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnsHindi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recruitment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinAge = table.Column<int>(type: "integer", nullable: true),
                    MaxAge = table.Column<int>(type: "integer", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExtendedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FeePaymentLastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CorrectionLastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AdmitCardDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExamMode = table.Column<int>(type: "integer", nullable: false),
                    ApplyLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherId = table.Column<int>(type: "integer", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    TotalPost = table.Column<long>(type: "bigint", nullable: true),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: true),
                    HowTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDesription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    SlugUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitCount = table.Column<int>(type: "integer", nullable: true),
                    SortLinks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    OtherLinks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDesriptionHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockTypeCode = table.Column<int>(type: "integer", nullable: true),
                    ThumbnailCaption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialMediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: true),
                    ShouldReminder = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReminderDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpcomingCalendarCode = table.Column<int>(type: "integer", nullable: true),
                    DescriptionJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindiJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeywordsHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentDocumentLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecruitmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentDocumentLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentHowToApplyAndQuickLinkLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecruitmentId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQuickLink = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHindiJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentHowToApplyAndQuickLinkLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentJobDesignationLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobDesignationId = table.Column<int>(type: "integer", nullable: false),
                    RecruitmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentJobDesignationLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentQualificationLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationId = table.Column<int>(type: "integer", nullable: false),
                    RecruitmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentQualificationLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    RecruitmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentTags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "ArticleFaq");

            migrationBuilder.DropTable(
                name: "ArticleTags");

            migrationBuilder.DropTable(
                name: "BlockContentAttachmentLookup");

            migrationBuilder.DropTable(
                name: "BlockContents");

            migrationBuilder.DropTable(
                name: "BlockContentsHowToApplyAndQuickLinkLookup");

            migrationBuilder.DropTable(
                name: "BlockContentTags");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "Recruitment");

            migrationBuilder.DropTable(
                name: "RecruitmentDocumentLookup");

            migrationBuilder.DropTable(
                name: "RecruitmentHowToApplyAndQuickLinkLookup");

            migrationBuilder.DropTable(
                name: "RecruitmentJobDesignationLookup");

            migrationBuilder.DropTable(
                name: "RecruitmentQualificationLookup");

            migrationBuilder.DropTable(
                name: "RecruitmentTags");
        }
    }
}
