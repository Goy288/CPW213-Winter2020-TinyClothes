using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Helper class to manage users
    /// shopping cart data using cookies.
    /// </summary>
    public static class CartHelper
    {
        private const string CartCookie = "CartCookie";

        public static void Add(Clothing c, IHttpContextAccessor accessor)
        {
            List<Clothing> clothes = GetAllClothes(accessor);
            clothes.Add(c);

            string data = JsonConvert.SerializeObject(clothes);

            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true,
                Secure = true
            };

            accessor.HttpContext.Response.Cookies.Append(CartCookie, data, options);
        }

        public static int GetCount(IHttpContextAccessor accessor)
            => GetAllClothes(accessor).Count;

        /// <summary>
        /// Returns all clothing currently stored in the user's cookie.
        /// If no items are present, then an empty list is returned.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static List<Clothing> GetAllClothes(IHttpContextAccessor accessor)
        {
            string data = accessor.HttpContext.Request.Cookies[CartCookie];

            return string.IsNullOrWhiteSpace(data) ? 
                   new List<Clothing>() : 
                   JsonConvert.DeserializeObject<List<Clothing>>(data);
        }
    }
}
