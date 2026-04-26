using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class AssetsMasterModel
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        public string DirectoryName { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class AssetsMasterViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? DirectoryName { get; set; }
        public string? ModifiedByName { get; set; }
        public string? Path { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }
    }

    public class AssetsMasterFilterModel : IndexModel
    {
        public string Title { get; set; }
        public string DirectoryName { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
