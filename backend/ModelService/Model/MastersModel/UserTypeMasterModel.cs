using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class UserTypeMasterModel
    {
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
    public class UserTypeMasterViewModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }
}
