using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class MenuMaster_MDL
    {
        public long Id { get; set; }
        public string? MenuName { get; set; }
        public string? DisplayName { get; set; }
        public bool HasChild { get; set; }
        public long? ParentId { get; set; }
        public long Position { get; set; }
        public string? IconClass { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long CreatedBy { get; set; }
    }

}
