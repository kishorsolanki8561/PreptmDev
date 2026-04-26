using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.CommonModel
{
    public class AuditableEntity
    {
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime? ModifiedDate { set; get; }
        public DateTime CreatedDate { set; get; }
        public int? ModifiedBy { get; set; }

        public int CreatedBy { get; set;}
        public string? IPAddress { set; get; }
        public string? IPCity { get; set; }
        public string? IPCountry { get; set; }
        public string? Browser { get; set; }
        public string? ScreenName { get; set; }
    }
}
