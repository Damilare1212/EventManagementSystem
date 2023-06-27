using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AdminController(IAdminService adminService, IUserService userService, IRoleService roleService, IWebHostEnvironment webHostEnvironment)
        {
            _adminService = adminService;
            _userService = userService;
            _roleService = roleService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _adminService.GetAllAdmin();
            return View(admins.Data);
        }

        public async Task<IActionResult> Add()
        {
            var roles = await _roleService.GetAllRoles();
            ViewData["Roles"] = new SelectList( roles.Data, "Id", "Name");
            return View();
        }

        public IActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateAdminRequestModel model, IFormFile adminPhoto)
        {
            string adminPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "adminPhoto");
            Directory.CreateDirectory(adminPhotoPath);
            string contentType = adminPhoto.ContentType.Split('/')[1];
            string adminImage = $"ADM{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(adminPhotoPath, adminImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                
                adminPhoto.CopyTo(fileStream);  
            }

            model.AdminPhoto = adminImage;
            var response = await _adminService.AddAdmin(model);
            ViewBag.Message = response.Message;
            if (response.Status == false)
            {
                
                return View();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var admin = await _adminService.GetAdmin(id);
            if( admin == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateAdminRequestModel model)
        {
            await _adminService.UpdateAdmin(id, model);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var admin = await _adminService.GetAdmin(id);
            if(admin == null)
            {
                NotFound();
            }
            return View(admin.Data);
        }


        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Profile()
        {
            var adminEmail = User.FindFirst(ClaimTypes.Email).Value;
            var admin = await _adminService.GetAdminByEmail(adminEmail);
            return View(admin.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var admin = await _adminService.GetAdmin(id);
            if(admin == null)
            {
                return NotFound();
            }
            return View(admin.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _adminService.DeleteAdmin(id);
            return RedirectToAction("Index");
        }
        
    }
}
