using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class BannerMaster_MDL :AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? URL { get; set; }
        public string? AttachmentUrl { get; set; }
        public bool? IsAdvt { get; set; }
        public int DisplayOrder { get; set; }
    }
}
