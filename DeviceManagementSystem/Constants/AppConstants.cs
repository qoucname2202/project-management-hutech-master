using System.Collections.Generic;

namespace DeviceManagementSystem.Constants
{
    public class AppConstants
    {
        private Dictionary<int, string> root = new Dictionary<int, string>();
        public AppConstants()
        {
            root.Add(1, "text-primary");
            root.Add(2, "text-secondary");
            root.Add(3, "text-success");
            root.Add(4, "text-danger");
            root.Add(5, "text-warning");
            root.Add(6, "text-info");
            root.Add(7, "text-light");
            root.Add(8, "text-dar");
        }

        public string GetTextColor(int index)
        {
            return root[index];
        }
    }
}
