using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System;

namespace DeviceManagementSystem.Models
{
    public class Manufacturer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("name")]
        [Required]
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;

        [BsonElement("country")]
        [MaxLength(20)]
        public string country { get; set; } = string.Empty;

        [BsonElement("address")]
        [MaxLength(100)]
        public string address { get; set; } = string.Empty;

        [BsonElement("post_code")]
        [MaxLength(6)]
        public string post_code { get; set; } = string.Empty;

        [BsonElement("city")]
        [MaxLength(30)]
        public string city { get; set; } = string.Empty;

        [BsonElement("contact_info")]
        public ManufacturerContactInfo contact_info { get; set; } = new ManufacturerContactInfo();

        [BsonElement("warranty_terms")]
        [MaxLength(500)]
        public string warranty_terms { get; set; } = string.Empty;

        [BsonElement("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [BsonElement("updated_at")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [BsonElement("is_removed")]
        public bool is_removed { get; set; } = false;
    }
}
