using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public static class CommonFunction
    {
        public static string Errorstring(string fileName,string methodName)
        {
            return "File Name :- " + fileName + " Method Name :- " + methodName;    
        }
    }
}
