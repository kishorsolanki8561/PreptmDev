using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure.ConfigModel.Translation.DBInterface
{
    public interface DBInterfaceDTO
    {
        // Simple or small models
        //public DbSet<UserFeedback_MDL> UserFeedbacks { get; set; }
        public DbSet<ArticleFaq_MDL> ArticleFaqs { get; set; }
        //public DbSet<FrontUser_MDL> FrontUsers { get; set; }
        //public DbSet<GroupMaster_MDL> GroupMasters { get; set; }
        //public DbSet<CategoryMaster_MDL> CategoryMasters { get; set; }
        //public DbSet<Bookmark_MDL> Bookmarks { get; set; }
        //public DbSet<BlockType_MDL> BlockTypes { get; set; }
        //public DbSet<BannerMaster_MDL> BannerMaster { get; set; }
        //public DbSet<AssetsMaster_MDL> AssetsMaster { get; set; }
        //public DbSet<AdditionalPages_MDL> AdditionalPages { get; set; }
        //public DbSet<State_MDL> State { get; set; }
        //public DbSet<User_MDL> User { get; set; }
        //public DbSet<UserType_MDL> UserType { get; set; }
        public DbSet<FAQ_MDL> FAQ { get; set; }
        public DbSet<Article_MDL> Article { get; set; }
        public DbSet<ArticleTags_MDL> ArticleTags { get; set; }

        // Intermediate models
        //public DbSet<Lookup_MDL> Lookup { get; set; }
        //public DbSet<LookupType_MDL> LookupType { get; set; }
        //public DbSet<PopularSearch_MDL> PopularSearch { get; set; }
        //public DbSet<QualificationMaster_MDL> QualificationMaster { get; set; }
        //public DbSet<SubCategory_MDL> SubCategory { get; set; }
        //public DbSet<Logs_MDL> Logs { get; set; }
        //public DbSet<JobDesignationMaster_MDL> JobDesignationMaster { get; set; }
        //public DbSet<PageMaster_MDL> PageMaster { get; set; }
        //public DbSet<MenuMaster_MDL> MenuMaster { get; set; }
        //public DbSet<MenuMasterMapping_MDL> MenuMasterMapping { get; set; }

        // More complex or larger models
        public DbSet<BlockContents_MDL> BlockContents { get; set; }
        public DbSet<BlockContentAttachmentLookup_MDL> BlockContentAttachmentLookup { get; set; }
        public DbSet<BlockContentsHowToApplyAndQuickLinkLookup_MDL> BlockContentsHowToApplyAndQuickLinkLookup { get; set; }
        public DbSet<BlockContentsTags_MDL> blockContentsTags { get; set; }
        //public DbSet<PageComponent_MDL> PageComponent { get; set; }
        //public DbSet<PageComponentAction_MDL> PageComponentAction { get; set; }
        //public DbSet<PageComponentPermission_MDL> PageComponentPermission { get; set; }
        //public DbSet<Scheme_MDL> Scheme { get; set; }
        //public DbSet<SchemeAttchamentLookup_MDL> SchemeAttchamentLookup { get; set; }
        //public DbSet<SchemeContactDetailsLookup_MDL> SchemeContactDetailsLookup { get; set; }
        //public DbSet<SchemeDocumentLookup_MDL> SchemeDocumentLookup { get; set; }
        //public DbSet<SchemeEligibilityLookup_MDL> SchemeEligibilityLookup { get; set; }
        //public DbSet<SchemeHowToApplyAndQuickLinkLookup_MDL> SchemeHowToApplyAndQuickLinkLookup { get; set; }
        //public DbSet<SiteMap_MDL> SiteMap { get; set; }

        // Relationships or lookup models
        public DbSet<Recruitment_MDL> Recruitment { get; set; }
        public DbSet<RecruitmentHowToApplyAndQuickLinkLookup_MDL> RecruitmentHowToApplyAndQuickLinkLookup { get; set; }
        public DbSet<RecruitmentJobDesignationLookup_MDL> RecruitmentJobDesignationLookup { get; set; }
        public DbSet<RecruitmentQualificationLookup_MDL> RecruitmentQualificationLookup { get; set; }
        public DbSet<RecruitmentTags_MDL> RecruitmentTags { get; set; }
        public DbSet<RecruitmentDocumentLookup_MDL> RecruitmentDocumentLookups { get; set; }

        public DbSet<Paper_MDL> Papers { get; set; }
        public DbSet<PaperSubject_MDL> PaperSubject { get; set; }
        public DbSet<PaperTags_MDL>  PaperTags { get; set; }

        public DbSet<Note_MDL> Notes { get; set; }
        public DbSet<Note_Subject_MDL> NoteSubject { get; set; }
        public DbSet<NoteTags_MDL> NoteTags { get; set; }

        public  DbSet<Syllabus_MDL> Syllabus { get; set; }
        public  DbSet<Syllabus_Subject_MDL> SyllabusSubject { get; set; }
        public  DbSet<SyllabusTags_MDL> SyllabusTags { get; set; }
    }
}
