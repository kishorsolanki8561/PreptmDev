using CommonService.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using ModelService.OtherModels;

namespace DBInfrastructure
{
    public class DBPreptmContext : DbContext, IDBPreptmContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTime _dateTime;
        private readonly JWTAuthManager _jWTAuthManager;

        public DBPreptmContext(DbContextOptions<DBPreptmContext> options) : base(options)
        {
        }
        public DBPreptmContext(DbContextOptions<DBPreptmContext> options, IDateTime dateTime, IHttpContextAccessor httpContextAccessor, JWTAuthManager jWTAuthManager) : base(options)
        {
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _jWTAuthManager = jWTAuthManager;
        }

        public DbContext DbContext => this;
        #region Operations
        public virtual DbSet<ArticleFaq_MDL> ArticleFaqs { get; set; }
        public virtual DbSet<Article_MDL> Article { get; set; }
        public virtual DbSet<ArticleTags_MDL> ArticleTags { get; set; }
        //public virtual DbSet<UserFeedback_MDL> UserFeedbacks { get; set; }
        //public virtual DbSet<FrontUser_MDL> FrontUsers { get; set; }
        //public virtual DbSet<GroupMaster_MDL> GroupMasters { get; set; }
        //public virtual DbSet<CategoryMaster_MDL> CategoryMasters { get; set; }
        //public virtual DbSet<Bookmark_MDL> Bookmarks { get; set; }
        //public virtual DbSet<BlockType_MDL> BlockTypes { get; set; }
        //public virtual DbSet<BannerMaster_MDL> BannerMaster { get; set; }
        //public virtual DbSet<AssetsMaster_MDL> AssetsMaster { get; set; }
        //public virtual DbSet<AdditionalPages_MDL> AdditionalPages { get; set; }
        //public virtual DbSet<State_MDL> State { get; set; }
        //public virtual DbSet<User_MDL> User { get; set; }
        //public virtual DbSet<UserType_MDL> UserType { get; set; }
        public virtual DbSet<FAQ_MDL> FAQ { get; set; }
        //public virtual DbSet<Lookup_MDL> Lookup { get; set; }
        //public virtual DbSet<LookupType_MDL> LookupType { get; set; }
        //public virtual DbSet<PopularSearch_MDL> PopularSearch { get; set; }
        //public virtual DbSet<QualificationMaster_MDL> QualificationMaster { get; set; }
        //public virtual DbSet<SubCategory_MDL> SubCategory { get; set; }
        //public virtual DbSet<Logs_MDL> Logs { get; set; }
        //public virtual DbSet<JobDesignationMaster_MDL> JobDesignationMaster { get; set; }
        //public virtual DbSet<PageMaster_MDL> PageMaster { get; set; }
        //public virtual DbSet<MenuMaster_MDL> MenuMaster { get; set; }
        //public virtual DbSet<MenuMasterMapping_MDL> MenuMasterMapping { get; set; }
        public virtual DbSet<BlockContents_MDL> BlockContents { get; set; }
        public virtual DbSet<BlockContentAttachmentLookup_MDL> BlockContentAttachmentLookup { get; set; }
        public virtual DbSet<BlockContentsHowToApplyAndQuickLinkLookup_MDL> BlockContentsHowToApplyAndQuickLinkLookup { get; set; }
        public virtual DbSet<BlockContentsTags_MDL> blockContentsTags { get; set; }
        //public virtual DbSet<PageComponent_MDL> PageComponent { get; set; }
        //public virtual DbSet<PageComponentAction_MDL> PageComponentAction { get; set; }
        //public virtual DbSet<PageComponentPermission_MDL> PageComponentPermission { get; set; }
        //public virtual DbSet<Scheme_MDL> Scheme { get; set; }
        //public virtual DbSet<SchemeAttchamentLookup_MDL> SchemeAttchamentLookup { get; set; }
        //public virtual DbSet<SchemeContactDetailsLookup_MDL> SchemeContactDetailsLookup { get; set; }
        //public virtual DbSet<SchemeDocumentLookup_MDL> SchemeDocumentLookup { get; set; }
        //public virtual DbSet<SchemeEligibilityLookup_MDL> SchemeEligibilityLookup { get; set; }
        //public virtual DbSet<SchemeHowToApplyAndQuickLinkLookup_MDL> SchemeHowToApplyAndQuickLinkLookup { get; set; }
        //public virtual DbSet<SiteMap_MDL> SiteMap { get; set; }
        public virtual DbSet<Recruitment_MDL> Recruitment { get; set; }
        public virtual DbSet<RecruitmentTags_MDL> RecruitmentTags { get; set; }
        public virtual DbSet<RecruitmentHowToApplyAndQuickLinkLookup_MDL> RecruitmentHowToApplyAndQuickLinkLookup { get; set; }
        public virtual DbSet<RecruitmentJobDesignationLookup_MDL> RecruitmentJobDesignationLookup { get; set; }
        public virtual DbSet<RecruitmentQualificationLookup_MDL> RecruitmentQualificationLookup { get; set; }
        public virtual DbSet<RecruitmentDocumentLookup_MDL> RecruitmentDocumentLookups { get; set; }
        public virtual DbSet<Paper_MDL> Papers { get; set; }
        public virtual DbSet<PaperSubject_MDL> PaperSubject { get; set; }
        public virtual DbSet<PaperTags_MDL> PaperTags { get; set; }
        public virtual DbSet<Note_MDL> Notes { get; set; }
        public virtual DbSet<Note_Subject_MDL> NoteSubject { get; set; }
        public virtual DbSet<NoteTags_MDL> NoteTags { get; set; }

        public virtual DbSet<Syllabus_MDL> Syllabus { get; set; }
        public virtual DbSet<Syllabus_Subject_MDL> SyllabusSubject { get; set; }
        public virtual DbSet<SyllabusTags_MDL> SyllabusTags { get; set; }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            string IpAddress = "", IpCity = "", IpCountry = "", ScreenName = "", Browser = "";
            if (_httpContextAccessor is not null && _httpContextAccessor.HttpContext is not null && _httpContextAccessor.HttpContext.Request is not null)
            {
                IpAddress = _httpContextAccessor.HttpContext.Request.Headers["IpAddress"];
                IpCity = _httpContextAccessor.HttpContext.Request.Headers["IpCity"];
                IpCountry = _httpContextAccessor.HttpContext.Request.Headers["IpCountry"];
                ScreenName = _httpContextAccessor.HttpContext.Request.Headers["ScreenName"];
                Browser = _httpContextAccessor.HttpContext.Request.Headers["Browser"];

            }
            //if(ChangeTracker.Entries<AuditableEntity>() is not null && ChangeTracker.Entries<AuditableEntity>().Count() >)
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entity in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        //entity.Entity.CreatedBy = _jWTAuthManager.User.Id;
                        entity.Entity.CreatedDate = DateTime.Now;
                        entity.Entity.IPAddress = IpAddress;
                        entity.Entity.IPCity = IpCity;
                        entity.Entity.IPCountry = IpCountry;
                        entity.Entity.Browser = Browser;
                        entity.Entity.ScreenName = ScreenName;
                        break;
                    case EntityState.Modified:
                        //entity.Entity.ModifiedBy = _jWTAuthManager.User.Id;
                        entity.Entity.ModifiedDate = DateTime.Now;
                        entity.Entity.IPAddress = IpAddress;
                        entity.Entity.IPCity = IpCity;
                        entity.Entity.IPCountry = IpCountry;
                        entity.Entity.Browser = Browser;
                        entity.Entity.ScreenName = ScreenName;
                        break;
                }
            }
            Task<int> res = base.SaveChangesAsync(cancellationToken);
            return res;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBPreptmContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
