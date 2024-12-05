using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System;

/**************************************************************************
 * File: Manufacturer.cs
 * Description: Manufacturer entities in the system.
 * Author: Duong Quoc Nam
 * Date Created: 2024-11-25
 * Last Modified By: 2024-11-28
 * ************************************************************************/
namespace DeviceManagementSystem.Models
{
    /// <summary>
    /// Represents a manufacturer entity stored in the MongoDB database
    /// This class maps directly to the "manufacturers" collection in MongoDB
    /// </summary>
    public class Manufacturer
    {
        /// <summary>
        /// The unique identifier for the manufacturer
        /// Stored as an ObjectId in MongoDB but represented as a string in the application
        /// </summary>
        [BsonId]  // Marks this property as the primary key in MongoDB
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        /// <summary>
        /// The name of the manufacturer
        /// Required and cannot exceed 100 characters
        /// </summary>
        [BsonElement("name")]
        [Required]
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;

        /// <summary>
        /// The country where the manufacturer is located
        /// Optional but limited to 20 characters
        /// </summary>
        [BsonElement("country")]
        [MaxLength(20)]
        public string country { get; set; } = string.Empty;

        /// <summary>
        /// The address of the manufacturer
        /// Optional but limited to 100 characters
        /// </summary>
        [BsonElement("address")]
        [MaxLength(100)]
        public string address { get; set; } = string.Empty;

        /// <summary>
        /// The postal code for the manufacturer's address
        /// Optional but limited to 6 characters
        /// </summary>
        [BsonElement("post_code")]
        [MaxLength(6)]
        public string post_code { get; set; } = string.Empty;

        /// <summary>
        /// The city where the manufacturer is located
        /// Optional but limited to 30 characters
        /// </summary>
        [BsonElement("city")]
        [MaxLength(30)]
        public string city { get; set; } = string.Empty;

        /// <summary>
        /// The contact information of the manufacturer, including email, hotline, and fax
        /// Stored as a nested document in MongoDB
        /// </summary>
        [BsonElement("contact_info")]
        public ManufacturerContactInfo contact_info { get; set; } = new ManufacturerContactInfo();

        /// <summary>
        /// The warranty terms offered by the manufacturer.
        /// Optional but limited to 500 characters.
        /// </summary>
        [BsonElement("warranty_terms")]
        [MaxLength(500)]
        public string warranty_terms { get; set; } = string.Empty;

        /// <summary>
        /// The date and time when the manufacturer record was created using UTC format
        /// </summary>
        [BsonElement("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date and time when the manufacturer record was last updated using UTC format
        /// Automatically updated whenever the record is modified
        /// </summary>
        [BsonElement("updated_at")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates whether the manufacturer has been soft-deleted.
        /// A value of true means the manufacturer is considered removed but not physically deleted from the database.
        /// </summary>
        [BsonElement("is_removed")]
        public bool is_removed { get; set; } = false;
    }
}
