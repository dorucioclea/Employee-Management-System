﻿using EMS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebCore.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public IEnumerable<DepartmentModel> Departments { get; set; }
    }
}
