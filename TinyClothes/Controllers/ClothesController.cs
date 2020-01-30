using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class ClothesController : Controller
    {
        private readonly StoreContext context;

        public ClothesController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {
            // Just a placeholder...
            List<Clothing> clothes =
                await ClothingDB.GetClothingbyPage(context, 1, 1);
            return View(clothes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDB.Add(c, context);
                // TODO: Add a success message after redirent
                // redirect
                // TempData lasts for one redirect.
                TempData["Message"] = $"{c.Title} added successfully!";
                return RedirectToAction("ShowAll");
            }
            //Return same view with validation messages
            return View(c);
        }
    }
}