using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class GroupMaster_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }

        public string? SlugUrl { set; get; }
    }


}
