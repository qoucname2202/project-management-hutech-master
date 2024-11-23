using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("name")]
        [Required(ErrorMessage = "The department name field is required.")]
        [MaxLength(100, ErrorMessage = "The department name cannot exceed 100 characters.")]
        public string name { get; set; } = string.Empty;

        [BsonElement("location")]
        [MaxLength(100, ErrorMessage = "The location cannot exceed 100 characters.")]
        public string location { get; set; } = string.Empty;

        [BsonElement("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [BsonElement("updated_at")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [BsonElement("is_removed")]
        public bool is_removed { get; set; } = false;
    }
}