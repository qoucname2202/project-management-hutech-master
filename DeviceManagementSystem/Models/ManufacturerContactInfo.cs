using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class ManufacturerContactInfo
    {
        [BsonElement("email")]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        [BsonElement("hotline_first")]
        public string hotline_first { get; set; } = string.Empty;

        [BsonElement("hotline_second")]
        public string hotline_second { get; set; } = string.Empty;

        [BsonElement("fax")]
        public string fax { get; set; } = string.Empty;
    }
}
