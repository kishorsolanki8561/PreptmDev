using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.OtherModels
{
    public class DeeplinkModel
    {
        public string? slugUrl { get; set; }
        public string? moduleName { get; set; }
        public bool isRecruitment { get; set; }
        public int? id { get; set; }
        public string? thumbnail { get; set; }
        public string? title { get; set; }
        public string? type { get; set; }
    }
}
