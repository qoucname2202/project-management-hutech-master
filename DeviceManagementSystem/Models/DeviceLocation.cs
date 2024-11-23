using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class DeviceLocation
    {
        [BsonElement("Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Details")]
        [MaxLength(500)]
        public string Details { get; set; } = string.Empty;
    }
}
