using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string ManufacturerID { get; set; } = string.Empty;

        [BsonElement("Specifications")]
        public Specifications Specifications { get; set; } = new Specifications();

        [BsonRepresentation(BsonType.ObjectId)]
        public string TypeID { get; set; } = string.Empty;
    }
}
