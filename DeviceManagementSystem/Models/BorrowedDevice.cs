using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class BorrowedDevice
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string DeviceID { get; set; }

        [BsonElement("BorrowDate")]
        [Required]
        public DateTime BorrowDate { get; set; }
    }
}
