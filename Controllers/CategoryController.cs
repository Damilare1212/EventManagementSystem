using EventApp.DTOs;
using EventApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            return View(categories.Data);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestModel model)
        {
             await _categoryService.CreateCategory(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if(category == null)
            {
                NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequestModel model)
        {
            var category = await _categoryService.UpdateCategory(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if( category == null)
            {
                NotFound();
            }
            return View(category.Data);
        }

        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var category = await _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details (int id)
        {
            var category = await _categoryService.GetCategoryByEventId(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category.Data);
        }
    }
}
