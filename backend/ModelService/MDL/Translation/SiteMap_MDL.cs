using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class SiteMap_MDL
    {
        public int Id { get; set; }
        public string? SlugUrl { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModuleName { get; set; }
        public string? ModuleNameHindi { get; set; }
    }

}
