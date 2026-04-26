using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class Note_Subject_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public int NoteId { get;set; }
        public string SubjectName { get; set; }
        public string SubjectNameHindi { get; set; }
        public int? YearId { get; set; }
        public string Path { get; set; }
        public Note_MDL Notes { get; set; }
    }
}
