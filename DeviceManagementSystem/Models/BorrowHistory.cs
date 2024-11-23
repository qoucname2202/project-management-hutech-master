using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class BorrowHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string EmployeeID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string DeviceID { get; set; }

        [BsonElement("BorrowDate")]
        [Required]
        public DateTime BorrowDate { get; set; }

        [BsonElement("ExpectedReturnDate")]
        [Required]
        public DateTime ExpectedReturnDate { get; set; }

        [BsonElement("ReturnDate")]
        public DateTime? ReturnDate { get; set; }

        [BsonElement("ReturnCondition")]
        [MaxLength(100)]
        public string ReturnCondition { get; set; } = string.Empty;

        [BsonElement("Status")]
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        [BsonElement("Notes")]
        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;
    }
}
