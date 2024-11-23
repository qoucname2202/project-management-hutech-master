using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class MaintenanceLog
    {
        [BsonElement("Date")]
        [Required]
        public DateTime Date { get; set; }

        [BsonElement("Description")]
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [BsonElement("Technician")]
        [Required]
        [MaxLength(100)]
        public string Technician { get; set; } = string.Empty;
    }
}
