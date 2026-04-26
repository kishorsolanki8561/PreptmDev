using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class AdditionalPages_MDL
    {
        public int Id { get; set; }
        public byte PageType { get; set; }
        public string Content { get; set; }
        public string? ContentHindi { get; set; }
        public string? ContentJson { get; set; }
        public string? ContentHindiJson { get; set; }
    }
}
