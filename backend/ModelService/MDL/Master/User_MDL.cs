using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class User_MDL : AuditableEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool? IsAutoLoggedOut { get; set; }
        public string? Password { get; set; }
        public int UserTypeCode { get; set; }
    }

}
