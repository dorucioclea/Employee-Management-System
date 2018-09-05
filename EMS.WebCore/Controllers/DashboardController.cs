﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.ApplicationCore.Interfaces.Services;
using EMS.WebCore.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebCore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public DashboardController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today.ToString("yyyy/MM/dd");

            var totalEmployee = await _attendanceService.CountTotalEmployeeAsync();

            var totalActive = await _attendanceService.GetActiveAsync();
            var totalAbsent = await _attendanceService.GetAbsentAsync();

            var percentAbsent = Math.Round(((double)totalAbsent.Count / (double)totalEmployee) * 100, 2);

            var viewModel = new DashboardViewModel
            {
                CountTotalEmployee = totalEmployee,
                CountActiveWork = totalActive.Count,
                CountAbsent = totalAbsent.Count,
                PercentAbsent = $"{percentAbsent}%"
            };

            return View(viewModel);
        }
    }
}