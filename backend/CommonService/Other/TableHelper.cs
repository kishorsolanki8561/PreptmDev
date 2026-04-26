using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public static class TableHelper
    {
        public static DataTable JSONArraryToDataTable(object obj)
        {
            return (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj), (typeof(DataTable)));
        }
    }
}
