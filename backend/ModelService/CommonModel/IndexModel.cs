using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.CommonModel
{
    public class IndexModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        [JsonIgnore]
        public string Search { get; set; }
        public string OrderBy { get; set; } = "ModifiedDate";
        public int OrderByAsc { get; set; }

        //public IDictionary<string, object> AdvanceSearchModel { get; set; }

        public IndexModel()
        {
            PageSize = 10;
            OrderByAsc = 1;
            OrderBy = "ModifiedDate";
            Search = "";
        }
    }
}
