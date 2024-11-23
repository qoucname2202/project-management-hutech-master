using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class Specifications
    {
        [BsonElement("cpu")]
        [MaxLength(50)]
        public string cpu { get; set; } = string.Empty;

        [BsonElement("ram")]
        [MaxLength(50)]
        public string rAM { get; set; } = string.Empty;

        [BsonElement("storage")]
        [MaxLength(50)]
        public string storage { get; set; } = string.Empty;

        [BsonElement("size")]
        [MaxLength(50)]
        public string size { get; set; } = string.Empty;

        [BsonElement("color")]
        [MaxLength(50)]
        public string color { get; set; } = string.Empty;
    }
}
