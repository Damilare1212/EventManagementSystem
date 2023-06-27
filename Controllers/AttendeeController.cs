using EventApp.DTOs;
using EventApp.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventApp.Controllers
{
    public class AttendeeController : Controller
    {
       
        private readonly IAttendeeService _attendeeService;
        private readonly ICategoryService _categoryService;
        public AttendeeController(IAttendeeService attendeeService, ICategoryService categoryService)
        {
           
            _attendeeService = attendeeService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var attendees = await _attendeeService.GetAll();
            return View(attendees.Data);
        }

        [HttpGet]
        public IActionResult SignUpPage()
        {
            return View();
        }
        public IActionResult Error()
        {
            if(TempData.Keys.Contains("error"))
            {
                ViewBag.message = TempData["error"].ToString();
            }

            return View();
        }
        public async Task<IActionResult> Add()
        {
            
            var category = await _categoryService.GetAll();
            ViewData["Category"] = new SelectList(category.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateAttendeeRequestModel model)
        {
            var attendee = await _attendeeService.AddAttendee(model);
            TempData["error"] = attendee.Message;
            if(attendee.Status == false)
            {
                return RedirectToAction("error");
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var attendee = await _attendeeService.GetAttendee(id);
            if(attendee == null)
            {
                NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateAttendeeRequestModel model)
        {
            var attendee = await _attendeeService.UpdateAttendee(id, model);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        [Authorize(Roles= "Attendee")]
        public async Task<IActionResult> Details(int id)
        {
            var attendee = await _attendeeService.GetAttendee(id);
            TempData["error"] = attendee.Message;
            if(attendee.Status == false)
            {
                return RedirectToAction("Error");
            }
            return View(attendee.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var attendee = await _attendeeService.GetAttendee(id);
            if(attendee == null)
            {
                NotFound();
            }
            return View(attendee.Data);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var attendee = await _attendeeService.DeleteAttendee(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile()
        {
            var attendeeEmail = User.FindFirst(ClaimTypes.Email).Value;
            var attendees = await _attendeeService.GetAttendeeByEmail(attendeeEmail);
            return View(attendees.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAttendeesByEvent(int eventId)
        {
            var attendees = await _attendeeService.GetAttendeesByEvent(eventId);
            if(attendees == null)
            {
                NotFound();
            }
            return View(attendees.Data);
        }
    }
}
