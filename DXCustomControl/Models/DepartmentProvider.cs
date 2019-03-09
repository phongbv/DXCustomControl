using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace DXCustomControl.Models
{
    public static class DepartmentsProvider
    {
        private static List<Department> departments;
        public static List<Department> GetDepartments()
        {
            if (departments == null)
                departments = new List<Department>() {
                CreateDepartment(1, 0, "Corporate Headquarters", 1000000, "Monterey", "(408) 555-1234"),
                CreateDepartment(2, 1, "Sales and Marketing", 22000, "San Francisco", "(415) 555-1234"),
                CreateDepartment(3, 2, "Field Office: Canada", 500000, "Toronto", "(416) 677-1000", "(416) 555-1234"),
                CreateDepartment(4, 2, "Field Office: East Coast", 500000, "Boston", "(617) 555-4234", "(415) 555-1234"),
                CreateDepartment(5, 2, "Pacific Rim Headquarters", 600000, "Kuaui", "(808) 555-1234"),
                CreateDepartment(6, 5, "Field Office: Singapore", 300000, "Singapore", "(606) 555-5486", "(606) 555-5786"),
                CreateDepartment(7, 5, "Field Office: Japan", 500000, "Tokyo", "(707) 555-1526", "(707) 555-5432"),
                CreateDepartment(8, 2, "Marketing", 1500000, "San Francisco", "(415) 555-1234"),
                CreateDepartment(9, 1, "Finance", 40000, "Monterey", "(408) 555-1234"),
                CreateDepartment(10, 1, "Engineering", 1100000, "Monterey", "(408) 555-1234"),
                CreateDepartment(11, 10, "Consumer Electronics Div.", 1150000, "Burlington, VT", "(802) 555-1234"),
                CreateDepartment(12, 11, "Software Development", 40000, "Monterey", "(408) 555-1234"),
                CreateDepartment(13, 10, "Software Products Div.", 1200000, "Monterey", "(408) 555-1234"),
                CreateDepartment(14, 13, "Quality Assurance", 48000, "Monterey", "(408) 555-1234", "(408) 555-1234"),
                CreateDepartment(15, 13, "Customer Support", 38000, "Monterey", "(408) 555-1234"),
                CreateDepartment(16, 13, "Research and Development", 460000, "Burlington, VT", "(802) 555-1234"),
                CreateDepartment(17, 13, "Customer Services", 850000, "Burlington, VT", "(802) 555-1234")
            };
            return departments;
        }
        static Department CreateDepartment(int id, int parentID, string name, int budget, string location, string phone1, string phone2 = null)
        {
            return new Department
            {
                ID = id,
                ParentID = parentID,
                DepartmentName = name,
                Budget = budget,
                Location = location,
                Phone1 = phone1,
                Phone2 = string.IsNullOrEmpty(phone2) ? phone1 : phone2
            };
        }
    }
}