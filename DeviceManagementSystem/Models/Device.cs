using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string ModelID { get; set; } = string.Empty;

        [BsonElement("SerialNumber")]
        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        [BsonElement("Manufacturer")]
        [Required]
        public string Manufacturer { get; set; } = string.Empty;

        [BsonElement("PurchaseDate")]
        [Required]
        public DateTime PurchaseDate { get; set; }

        [BsonElement("WarrantyExpiry")]
        [Required]
        public DateTime WarrantyExpiry { get; set; }

        [BsonElement("Status")]
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        [BsonElement("Location")]
        public DeviceLocation Location { get; set; } = new DeviceLocation();

        [BsonElement("MaintenanceLogs")]
        public List<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
    }
}
