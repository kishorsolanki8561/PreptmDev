using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class UserFeedback_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public string? Message { get; set; }
    }

}
