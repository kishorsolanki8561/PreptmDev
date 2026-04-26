using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class BlockContentsHowToApplyAndQuickLinkLookup_MDL
    {
        public int Id { get; set; }
        public int BlockContentId { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? LinkUrl { get; set; }
        public bool? IsQuickLink { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? IconClass { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionHindiJson { get; set; }
    }

}
