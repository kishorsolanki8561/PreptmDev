using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class AssetsMaster_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        public string? DirectoryName { get; set; }
        public string? Title { get; set; }
    }
}
