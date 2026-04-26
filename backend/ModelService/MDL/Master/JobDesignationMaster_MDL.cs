using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class JobDesignationMaster_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CreateBy { get; set; }
        public string? NameHindi { get; set; }
    }

}
