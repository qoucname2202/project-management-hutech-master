using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

/**************************************************************************
 * File: ManufacturerContactInfo.cs
 * Description
 * Author: Duong Quoc Nam
 * Date Created: 2024-11-25
 * Last Modified By: 2024-11-28
 * ************************************************************************/
namespace DeviceManagementSystem.Models
{
    /// <summary>
    /// Represents the contact information of a manufacturer.
    /// This is used as a nested document within the Manufacturer entity in MongoDB.
    /// </summary>
    public class ManufacturerContactInfo
    {
        /// <summary>
        /// The primary email address for contacting the manufacturer
        /// Must be a valid email address format
        /// </summary>
        [BsonElement("email")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// The primary hotline or phone number for contacting the manufacturer
        /// Must be a valid phone number format
        /// </summary>

        [BsonElement("hotline_first")]
        [RegularExpression(@"^(?:\+84|0)(?:3|5|7|8|9)[0-9]{8}$|^(?:\+84|0)(?:24|28|2[0-9])[0-9]{7}$", ErrorMessage = "Invalid phone number format.")]
        public string hotline_first { get; set; } = string.Empty;

        /// <summary>
        /// The secondary hotline or phone number for contacting the manufacturer
        /// Optional, and can be null if not provided
        /// </summary>
        [BsonElement("hotline_second")]
        [RegularExpression(@"^(?:\+84|0)(?:3|5|7|8|9)[0-9]{8}$|^(?:\+84|0)(?:24|28|2[0-9])[0-9]{7}$", ErrorMessage = "Invalid phone number format.")]
        public string hotline_second { get; set; } = string.Empty;

        /// <summary>
        /// The fax number for the manufacturer
        /// Optional, as fax communication is less common in modern systems
        /// </summary>
        [BsonElement("fax")]
        public string fax { get; set; } = string.Empty;
    }
}
