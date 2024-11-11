﻿using MDTasks.Models;
using System.Collections.Generic;

namespace MDTasks.Services
{
    public class CommonServices
    {
        public string GetEmployeeID(Employee employee)
        {
            string result = string.Empty;
            string[] elements = employee.FullName.Split(" ");
            foreach (var ele in elements)
                result += ele.Trim().Substring(0, 1).ToUpper();

            result += employee.Birthday.ToString("ddMMyy");

            // Return
            return result;
        }
    }
}
