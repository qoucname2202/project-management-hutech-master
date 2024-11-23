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
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Country")]
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;

        [BsonElement("ContactInfo")]
        public ManufacturerContactInfo ContactInfo { get; set; } = new ManufacturerContactInfo();

        [BsonElement("WarrantyTerms")]
        [MaxLength(500)]
        public string WarrantyTerms { get; set; } = string.Empty;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
