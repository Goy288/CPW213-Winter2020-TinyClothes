﻿using System;
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
        private readonly StoreContext _context;

        public ClothesController(StoreContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAll(int? page)
        {
            const int PageSize = 2;
            // Null-coalescing operator ??
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            int pageNumber = page ?? 1;
            ViewData["CurrentPage"] = pageNumber;

            int maxPage = await GetMaxPage(PageSize);
            ViewData["MaxPage"] = maxPage;

            List<Clothing> clothes =
                await ClothingDB.GetClothingbyPage(_context, pageNumber, PageSize);
            return View(clothes);
        }

        private async Task<int> GetMaxPage(int PageSize)
        {
            int numProducts = await ClothingDB.GetNumClothing(_context);
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
                await ClothingDB.Add(c, _context);
                // TODO: Add a success message after redirent
                // redirect
                // TempData lasts for one redirect.
                TempData["Message"] = $"{c.Title} added successfully!";
                return RedirectToAction("ShowAll");
            }
            //Return same view with validation messages
            return View(c);
        }

        public async Task<IActionResult> Edit(int id)
        {
            //Return same view with validation messages
            Clothing c = await ClothingDB.GetClothingbyID(id, _context);

            if(c == null) // Clothing not in DB
            {
                // Returns a HTTP 404 - Not Found
                return NotFound();
            }

            return View(c);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDB.Edit(c, _context);

                ViewData["Message"] =
                    c.Title + " updated successfully!";
            }

            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Clothing c = await ClothingDB.GetClothingbyID(id, _context);

            return View(c);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            Clothing c = await ClothingDB.GetClothingbyID(id, _context);
            await ClothingDB.Delete(id, _context);
            TempData["Message"] = $"{c.Title} deleted successfully";
            return RedirectToAction(nameof(ShowAll));
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchCriteria search)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Prepare query - SELECT * FROM Clothes
            // Does not get sent to DB
            IQueryable<Clothing> allClothes = from c in _context.Clothing
                                              select c;

            // WHERE MinPrice < Price
            if (search.MinPrice.HasValue)
            {
                allClothes = from c in allClothes
                             where c.Price >= search.MinPrice
                             select c;
            }

            // WHERE Price < MaxPrice
            if (search.MaxPrice.HasValue)
            {
                allClothes = from c in allClothes
                             where c.Price <= search.MaxPrice
                             select c;
            }

            if(!string.IsNullOrWhiteSpace(search.Size))
            {
                allClothes = from c in allClothes
                             where c.Size == search.Size
                             select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Type))
            {
                allClothes = from c in allClothes
                             where c.Size == search.Type
                             select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                allClothes = from c in allClothes
                             where c.Title.Contains(search.Title)
                             select c;
            }
            
            search.Results = allClothes.ToList();
            return View(search);
        }
    }
}