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
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService _organizerService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public OrganizerController(IOrganizerService organizerService, IRoleService roleService, IUserService userService)
        {
            _organizerService = organizerService;
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var organizers = await _organizerService.GetAllOrganizers();
            return View(organizers.Data);
        }

        public async Task<IActionResult> Add()
        {
            var roles = await _roleService.GetAllRoles();
            ViewData["Roles"] = new SelectList(roles.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CreateOrganizerRequestModel model)
        {
             await _organizerService.AddOrganizer(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var organizer = await _organizerService.GetOrganizerById(id);
            if(organizer == null)
            {
                return Content(organizer.Message);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateOrganizerRequestModel model)
        {
            var organizer = await _organizerService.UpdateOrganizer(id, model);
            return RedirectToAction("Details");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var organizer = await _organizerService.GetOrganizerById(id);
            return View(organizer.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var organizer = await _organizerService.GetOrganizerById(id);
            if(organizer == null)
            {
                NotFound();
            }
            return View(organizer.Data);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var organizer = await _organizerService.DeleteOrganizer(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile()
        {
            var organizerEmail = User.FindFirst(ClaimTypes.Email).Value;
            var organizer = await _organizerService.GetOrganizerByEmail(organizerEmail);
            return View(organizer.Data);
        }
    }
}
