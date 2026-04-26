using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class PageComponentPermission_MDL
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int PageId { get; set; }
        public int PageComponentId { get; set; }
        public int PageCompActionId { get; set; }
        public int? UserTypeCode { get; set; }
        public bool? IsAllowed { get; set; }
    }

}
