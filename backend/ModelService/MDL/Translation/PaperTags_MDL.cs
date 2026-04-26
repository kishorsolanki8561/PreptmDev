using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class PaperTags_MDL
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int PaperId { get; set; }
        public Paper_MDL Papers { get; set; }
    }
}
