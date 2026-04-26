using ModelService.CommonModel;

namespace ModelService.MDL.Master
{
    public class Lookup_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public int LookupTypeId { get; set; }
        public string Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string Slug { get; set; }
    }

}
