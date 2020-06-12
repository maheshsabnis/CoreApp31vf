using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp31.Models;
using CoreApp31.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp31.Controllers
{
   // [Authorize]
    public class CategoryController : Controller
    {

        private readonly IService<Category, int> catService;

        /// <summary>
        /// Inject the service using Ctor injection
        /// </summary>
        public CategoryController(IService<Category, int> catService)
        {
            this.catService = catService;
        }
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> Index()
        {
            var cats = await catService.GetAsync();
            return View(cats);
        }
        [Authorize(Policy = "writepolicy")]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            //try
            //{
                // validate the model
                if (ModelState.IsValid)
                {
                    if (category.BasePrice < 0) throw new Exception("Base Price cannot be -ve");
                    category = await catService.CreateAsync(category);
                    return RedirectToAction("Index");
                }
                return View(category); // stey on Same View with validation error messages
           // }
            //catch (Exception ex)
            //{
            //    // redirect to error view
            //    return View("Error", new ErrorViewModel()
            //    {
            //        ControllerName = this.RouteData.Values["controller"].ToString(),
            //        ActionName = this.RouteData.Values["action"].ToString(),
            //        Exception = ex
            //    }) ;
            //}
        }
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Edit(int id)
        {
            var cat = await catService.GetAsync(id);
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            try
            {
                // validate the model
                if (ModelState.IsValid)
                {
                    category = await catService.UpdateAsync(id,category);
                    return RedirectToAction("Index");
                }
                return View(category); // stey on Same View with validation error messages
            }
            catch (Exception ex)
            {
                // redirect to error view
                return View("Error");
            }
        }

    }
}
