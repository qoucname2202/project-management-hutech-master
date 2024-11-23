using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class ManufacturerContactInfo
    {
        [BsonElement("Email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Phone")]
        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}
