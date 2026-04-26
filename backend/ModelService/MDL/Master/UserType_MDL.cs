using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class UserType_MDL
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

}
