using EventApp.DTOs;
using EventApp.Enums;
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
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ICategoryService _categoryService;
        private readonly ITicketService _ticketService;
        private readonly IAttendeeService _attendeeService;
        private readonly IOrganizerService _organizerService;

        public EventController(IEventService eventService, ICategoryService categoryService, ITicketService ticketService,
            IAttendeeService attendeeService, IOrganizerService organizerService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
            _ticketService = ticketService;
            _attendeeService = attendeeService;
            _organizerService = organizerService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAll();
            return View(events.Data);
        }

       
        public async Task<IActionResult> Create()
        {
            var category = await _categoryService.GetAll();
            ViewData["Category"] = new SelectList(category.Data, "Id", "Name");
            var organizer = await _organizerService.GetAllOrganizers();
            ViewData["Organizer"] = new SelectList(organizer.Data, "Id", "FirstName" );
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Create(CreateEventRequestModel model)
        {
            var events = await _eventService.CreateEvent(model);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var events = await _eventService.GetEventById(id);
            return View(events.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update (int id)
        {
            var events = await _eventService.GetEventById(id);
            if (events == null)
            {
                NotFound();
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Organizer")]
        public async Task<IActionResult> Update(int id, UpdateEventRequestModel model)
        {
            var events = await _eventService.UpdateEvent(id, model);
            return RedirectToAction("View");
        }
        
        [HttpGet]
        [Authorize(Roles ="Organizer")]
        public async Task<IActionResult> Delete(int id)
        {
            var events = await _eventService.GetEventById(id);
            if(events == null)
            {
                NotFound();
            }
            return View(events.Data);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles ="Organizer")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var events = await _eventService.DeleteEvent(id);
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> BookForEvent(int id)
        {
            var userMail = User.FindFirst(ClaimTypes.Email).Value;
            var attendee = await _attendeeService.GetAttendeeByEmail(userMail);

            var events = await _eventService.BookForEvent(attendee.Data.Id, id);
            return View(events.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AllBookedEvents()
        {
            var userMail = User.FindFirst(ClaimTypes.Email).Value;
            var attendee = await _attendeeService.GetAttendeeByEmail(userMail);

            var events = await _eventService.GetAllBookedEvents(attendee.Data.Id);
            return View(events.Data);
        }
        [HttpGet]
        public IActionResult SearchEvent()
        {
            return View();
        }
     
        [HttpPost]
        public async Task<IActionResult> SearchEvent(string searchText)
        {
            var search = await _eventService.SearchEvent(searchText);
            return View(search.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AttendeeAreaOfInterestEvents()
        {
            var userMail =  User.FindFirst(ClaimTypes.Email).Value;
            var attendee = await _attendeeService.GetAttendeeByEmail(userMail);

            var events = await _eventService.GetAttendeeAreaOfInterestEvents(attendee.Data.Id);
            return View(events.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AttendeeAllBookedEvents()
        {
            var userMail = User.FindFirst(ClaimTypes.Email).Value;
            var attendee = await _attendeeService.GetAttendeeByEmail(userMail);

            var events = await _eventService.GetAllBookedEvents(attendee.Data.Id);
            return View(events.Data);
        }


        [HttpGet]
        public async Task<IActionResult> CancelEvent(int id)
        {
            var events = await _eventService.GetEventById(id);
            if( events == null)
            {
                NotFound();
            }
            return View(events.Data);
        }

        [HttpPost, ActionName("CancelEvent")]
        public async Task<IActionResult> ConfirmCancelEvent(int id)
        {
            var userMail = User.FindFirst(ClaimTypes.Email).Value;
            var attendee = await _attendeeService.GetAttendeeByEmail(userMail);

            var events = await _eventService.CancelEvent(attendee.Data.Id, id);
            return RedirectToAction("AttendeeAllBookedEvents");
        }

        [HttpGet]
        public async Task<IActionResult> AttendeePastEvents()
        {
            var userMail = User.FindFirst(ClaimTypes.Email).Value;
            var attendee = await _attendeeService.GetAttendeeByEmail(userMail);

            var events = await _eventService.GetAllAttendeePastEvents(attendee.Data.Id);
            return View(events.Data);
        }
        
        [HttpGet]
        public async Task<IActionResult> OrganizerUpComingEvents()
        {
            var userMail = User.FindFirst(ClaimTypes.Email).Value;
            var organizer = await _organizerService.GetOrganizerByEmail(userMail);

            var events = await _eventService.GetAllOrganizerUpComingEvents(organizer.Data.Id);
            return View(events.Data);
        }

        [HttpGet]
        public async Task<IActionResult> OrganizerPastEvents()
        {
            var userMail = User.FindFirst(ClaimTypes.Email).Value;
            var organizer = await _organizerService.GetOrganizerByEmail(userMail);

            var events = await _eventService.GetAllOrganizerPastEvents(organizer.Data.Id);
            return View(events.Data);
        }
    }
}
