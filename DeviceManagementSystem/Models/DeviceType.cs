using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System;

namespace DeviceManagementSystem.Models
{
    public class DeviceType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Description")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
