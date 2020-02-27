using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreContext _context;
        private readonly IHttpContextAccessor _accessor;

        public CartController(StoreContext context, IHttpContextAccessor accessor)
        {
            this._context = context;
            this._accessor = accessor;
        }

        // Displate all products in cart
        public IActionResult Index()
        {
            return View();
        }

        // Add a single product to the shopping cart
        public async Task<IActionResult> Add(int id, string prevUrl)
        {
            Clothing c = await ClothingDB.GetClothingbyID(id, _context);

            if (c != null)
            {
                CartHelper.Add(c, _accessor);
            }

            return Redirect(prevUrl);
        }

        // Summary checkout
        public IActionResult Checkout()
        {
            return View();
        }
    }
}