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
        public string id { get; set; }

        [BsonElement("name")]
        [Required(ErrorMessage = "The device type name field is required.")]
        [MaxLength(50, ErrorMessage = "The device type name cannot exceed 50 characters.")]
        public string name { get; set; } = string.Empty;

        [BsonElement("description")]
        [MaxLength(500, ErrorMessage = "The device type name cannot exceed 500 characters.")]
        public string description { get; set; } = string.Empty;

        [BsonElement("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [BsonElement("updated_at")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [BsonElement("is_removed")]
        public bool is_removed { get; set; } = false;
    }
}
