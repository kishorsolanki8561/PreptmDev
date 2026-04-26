using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class SchemeDocumentLookup_MDL
    {
        public int Id { get; set; }
        public int SchemeId { get; set; }
        public int LookupId { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
    }
}
