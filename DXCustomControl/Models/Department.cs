﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXCustomControl.Models
{
    public class Department
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string DepartmentName { get; set; }
        public int Budget { get; set; }
        public string Location { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
    }
}