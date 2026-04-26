using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class FrontUser_MDL : AuditableEntity
    {
        public long Id { get; set; }
        public string? UId { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Language { get; set; }
        public string? ProfileImg { get; set; }
        public int? StateId { get; set; }
        public string? AuthToken { get; set; }
        public string? Provider { get; set; }
        public string? FCMToken { get; set; }
        public string? Platform { get; set; }
        public bool? IsPushNotification { get; set; }
        public bool? IsVerified { get; set; }
    }
}
