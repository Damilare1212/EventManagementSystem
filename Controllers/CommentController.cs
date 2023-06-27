using EventApp.DTOs;
using EventApp.Implementations.Services;
using EventApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IEventService _eventService;
        private readonly IRoleService _roleService;
        public CommentController(ICommentService commentService, IEventService eventService, IRoleService roleService)
        {
            _commentService = commentService;
            _eventService = eventService;
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            var comments = await _commentService.GetAllComments();
            return View(comments.Data);
        }

       // [HttpGet]
        public async Task<IActionResult> Create()
        {
            var events = await _eventService.GetAll();
            ViewData["Event"] = new SelectList(events.Data, "Id", "Title");
            var roles = await _roleService.GetAllRoles();
            ViewData["Roles"] = new SelectList(roles.Data, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentRequestModel model)
        {
            var comment = await _commentService.AddComment(model);
            return RedirectToAction("Index");
        } 

        [HttpGet]
        public async Task<IActionResult> Update (int id)
        {
            var comment = await _commentService.GetCommentById(id);
            if(comment == null)
            {
                NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateCommentRequestModel model)
        {
            var comment = await _commentService.UpdateComment(id,model);
            return RedirectToAction("View");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.GetCommentById(id);
            if(comment == null)
            {
                NotFound();
            }
            return View(comment.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var comment = await _commentService.DeleteComment(id);
            return RedirectToAction("Index");
        }
    }
}
