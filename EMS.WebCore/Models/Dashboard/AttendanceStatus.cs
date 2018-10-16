﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebCore.Models.Dashboard
{
    public class AttendanceStatus
    {
        public string Department { get; set; }
        public string Section { get; set; }
        public string ShiftName { get; set; }
        public int TotalPerson { get; set; }
        public int ActivePerson { get; set; }
        public int AbsentPerson { get; set; }
    }
}
