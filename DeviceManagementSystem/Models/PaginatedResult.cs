using System.Collections.Generic;
using System;


/**************************************************************************
 * File: PaginatedResult.cs
 * Description: Pagiantion view model in the system.
 * Author: Duong Quoc Nam
 * Date Created:  2024-12-05
 * Last Modified By: 
 * ************************************************************************/
namespace DeviceManagementSystem.Models
{
    /// <summary>
    /// Represents a paginated response with metadata about the records and pagination.
    /// </summary>
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public long TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
