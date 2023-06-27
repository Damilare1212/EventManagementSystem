using EventApp.DTOs;
using EventApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRoles();
            return View(roles.Data);
        }


        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
            var role = await _roleService.CreateRole(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateRoleRequestModel model)
        {
            var role = await _roleService.UpdateRole(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if(role == null)
            {
                NotFound();
            }
            return View(role.Data);
        }

        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var role = await _roleService.DeleteRoleById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if(role == null)
            {
                NotFound();
            }
            return View(role.Data);
        }
    }
}
