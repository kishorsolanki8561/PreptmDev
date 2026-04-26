using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class PageComponentAction_MDL
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int ComponentId { get; set; }
        public int Action { get; set; }
    }

}
