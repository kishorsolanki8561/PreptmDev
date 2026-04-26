using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class SchemeAttchamentLookup_MDL
    {
        public int Id { get; set; }
        public int SchemeId { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public int? Type { get; set; }
        public string? Path { get; set; }
    }

}
