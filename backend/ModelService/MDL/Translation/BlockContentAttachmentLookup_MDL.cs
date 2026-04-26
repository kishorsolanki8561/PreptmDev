using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class BlockContentAttachmentLookup_MDL
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int BlockContentId { get; set; }
    }
}
