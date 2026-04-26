using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class BlockType_MDL :AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? NameHindi { get; set; }
        public bool? ForRecruitment { get; set; }
        public DateTime? ModifiedSideMapDate { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? SlugUrl { set; get; }
    }
}
