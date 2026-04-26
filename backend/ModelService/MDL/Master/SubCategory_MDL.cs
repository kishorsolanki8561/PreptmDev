using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class SubCategory_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? Icon { get; set; }
        public string? NameHindi { get; set; }
        public string? SlugUrl { set; get; }
    }

}
