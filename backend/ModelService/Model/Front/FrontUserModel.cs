using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Front
{
    public class FrontUserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Language { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImg { get; set; }
        public int? StateId { get; set; }
        [JsonIgnore]
        public string? AuthToken { get; set; }
        [JsonIgnore]
        public string? Provider { get; set; }
        [JsonIgnore]
        public string? FCMToken { get; set; }
        [JsonIgnore]
        public string? Platform { get; set; }
        [JsonIgnore]
        public string UId { get; set; }
    }
    public class FrontUserViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Language { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfileImg { get; set; }
        public int? StateId { get; set; }
        public string UId { get; set; }
        public string? Token { get; set;}
    }
    public class FrontUserLoginModel
    {
        public string UId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Language { get; set; }
        public string? MobileNumber { get; set; }
        [JsonIgnore]
        public string? AuthToken { get; set; }
        public string Provider { get; set; }
        public string Platform { get; set; }
        public string? ProfileImg { get; set; }

    }
    public class FrontUserListModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImg { get; set; }
        public string? StateName { get; set; }
        public string Provider { get; set; }
        public string Platform { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }
}
