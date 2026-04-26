using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class NoteTags_MDL
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int NoteId { get; set; }
        public Note_MDL Notes { get; set; }
    }
}
