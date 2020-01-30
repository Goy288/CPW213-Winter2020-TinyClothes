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
        public async Task<IActionResult> ShowAll(int? page)
        {
            const int PageSize = 2;
            // Null-coalescing operator ??
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            int pageNumber = page ?? 1;

            int maxPage = await GetMaxPage(PageSize);
            ViewData["MaxPage"] = maxPage;

            List<Clothing> clothes =
                await ClothingDB.GetClothingbyPage(context, pageNumber, PageSize);
            return View(clothes);
        }

        private async Task<int> GetMaxPage(int PageSize)
        {
            int numProducts = await ClothingDB.GetNumClothing(context);
            return Convert.ToInt32(
                   Math.Ceiling((double)numProducts / PageSize));
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