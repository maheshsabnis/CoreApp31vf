using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp31.Models;
using CoreApp31.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreApp31.Controllers
{
    /// <summary>
    /// Process the HTTP Request
    /// Accept the Request at Resource (aka Controller class and Action Method)
    /// Following Practices at controller level
    /// 1. Error Handling
    /// 2. Security Management
    /// 3. Role and Policies
    /// </summary>
    public class ProductController : Controller
    {

        private readonly IService<Product, int> prdService;
        private readonly IService<Category, int> catService;

        /// <summary>
        /// Inject the service using Ctor injection
        /// </summary>
        public ProductController(IService<Product, int> prdService, IService<Category, int> catService)
        {
            this.prdService = prdService;
            this.catService = catService;
        }
        public async Task<IActionResult> Index()
        {
            var prds = await prdService.GetAsync();
            return View(prds);
        }

        public async Task<IActionResult> Create()
        {
            // Define a ViewBag that will pass the List of Categories to the Create View
            ViewBag.CategoryRowId = new SelectList(await catService.GetAsync(), "CategoryRowId", "CategoryName");
            return View(new Product());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product Product)
        {
            try
            {
                // validate the model
                if (ModelState.IsValid)
                {
                    Product = await prdService.CreateAsync(Product);
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryRowId = new SelectList(await catService.GetAsync(), "CategoryRowId", "CategoryName");
                return View(Product); // stey on Same View with validation error messages
            }
            catch (Exception ex)
            {
                // redirect to error view
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prd = await prdService.GetAsync(id);
            return View(prd);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product Product)
        {
            try
            {
                // validate the model
                if (ModelState.IsValid)
                {
                    Product = await prdService.UpdateAsync(id,Product);
                    return RedirectToAction("Index");
                }
                return View(Product); // stey on Same View with validation error messages
            }
            catch (Exception ex)
            {
                // redirect to error view
                return View("Error");
            }
        }

    }
}
