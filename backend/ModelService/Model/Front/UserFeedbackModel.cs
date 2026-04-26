using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Front
{
    public class UserFeedbackModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }      
        [JsonIgnore]
        public byte Status { get; set;  }
        public byte Type { get; set; }
        public string Message { get; set; }
    }
    public class UserFeedbackFilterModel : IndexModel
    {
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; }

    }

    public class UserFeedbackViewListModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }
}
