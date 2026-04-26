using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public class CommonEnum
    {
        public enum ProgressStatus
        {
            unapproved = 1,
            approved = 2,
            published = 3,
        }

        public enum ModuleKey
        {
            BlockContent =1,
            Recruitment =2,
            Scheme=3
        }
    }
}
