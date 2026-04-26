using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class LookupType_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string Slug { get; set; }
    }

}
