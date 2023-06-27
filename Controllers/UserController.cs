using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            var user = await _userService.Login(model);
            ViewBag.error = user.Message;
            if (user.st != null)
            {

                var claims = new List<Claim>
                {
                    
                    new Claim(ClaimTypes.Email, user.Data.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                   // new Claim(ClaimTypes.NameIdentifier, user.Data.Email),
                   
                   
                };
                foreach(var role in  user.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                   
                }
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,authenticationProperties);

                if (user.Data.Roles.Select( x => x.Name).Contains("Admin"))
                {
                    return RedirectToAction("Profile", "Admin");
                }

                if (user.Data.Roles.Select(x => x.Name).Contains("Attendee"))
                {
                    return RedirectToAction("Profile", "Attendee");
                }

                if (user.Data.Roles.Select(x => x.Name).Contains("Organizer"))
                {
                    return RedirectToAction("Profile", "Organizer");
                }
            }

               
                return View();
               
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userService.GetUserById(id);
            if(user == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateUserRequestModel model)
        {
            var user = await _userService.UpdateUser(id, model);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserById(id);
           
            return View(user);
        }
       

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute ]int id)
        {
            var user = await _userService.GetUserById(id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm( int id)
        {
            var user = await _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
