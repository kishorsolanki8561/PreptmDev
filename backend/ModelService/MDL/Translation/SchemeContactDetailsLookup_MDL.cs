using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class SchemeContactDetailsLookup_MDL
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int SchemeId { get; set; }
        public string? NodalOfficerName { get; set; }
        public string? NodalOfficerNameHindi { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
    }

}
