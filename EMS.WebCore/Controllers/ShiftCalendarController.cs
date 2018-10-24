﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.ApplicationCore.Interfaces.Services;
using EMS.ApplicationCore.Models;
using EMS.WebCore.Interfaces;
using EMS.WebCore.ViewModels.ShiftCalendar;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebCore.Controllers
{
    public class ShiftCalendarController : Controller
    {
        private readonly IShiftCalendarService _shiftCalendarService;
        private readonly IEmployeeDetailService _employeeDetailService;

        public ShiftCalendarController(
            IShiftCalendarService shiftCalendarService, 
            IEmployeeDetailService employeeDetailService)
        {
            _shiftCalendarService = shiftCalendarService;
            _employeeDetailService = employeeDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shiftCalendars = await _shiftCalendarService.GetAllAsync();

            if (shiftCalendars == null)
                return View();

            var viewModel = new ShiftCalendarViewModel
            {
                ShiftCalendars = shiftCalendars
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ShiftCalendarEditViewModel
            {
                Shifts = await _employeeDetailService.GetShifts()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShiftCalendarEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var shift = new ShiftCalendarModel
            {
                WorkDate = model.WorkDate,
                WorkType = model.WorkType,
                ShiftId = model.ShiftId
            };

            await _shiftCalendarService.AddAsync(shift);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string date)
        {
            var selectedDate = Convert.ToDateTime(date);

            var shiftCalendar = await _shiftCalendarService.GetByDateAsync(selectedDate);

            if (shiftCalendar == null)
                return NotFound();

            var editModel = new ShiftCalendarEditViewModel
            {
                WorkDate = shiftCalendar.WorkDate,
                WorkType = shiftCalendar.WorkType,
                ShiftId = shiftCalendar.ShiftId,
                Shifts = await _employeeDetailService.GetShifts()
            };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShiftCalendarEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var shift = new ShiftCalendarModel
            {
                WorkDate = model.WorkDate,
                WorkType = model.WorkType,
                ShiftId = model.ShiftId
            };

            await _shiftCalendarService.UpdateAsync(shift);
            return RedirectToAction(nameof(Index));
        }
    }
}