using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public static class SessionHelper
    {
        private const string IDKey = "ID";
        private const string UsernameKey = "Username";

        /// <summary>
        /// Used to create a user session.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="accessor"></param>
        public static void CreateUserSession
            (int id, string username, IHttpContextAccessor accessor)
        {
            accessor.HttpContext.Session.SetInt32(IDKey, id);
            accessor.HttpContext.Session.SetString(UsernameKey, username);
        }

        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static bool IsUserLoggedIn
            (IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.Session.GetInt32(IDKey).HasValue;
        }

        /// <summary>
        /// Used to log the user out.
        /// </summary>
        /// <param name="accessor"></param>
        public static void DeleteUserSession
            (IHttpContextAccessor accessor)
        {
            accessor.HttpContext.Session.Clear();
        }
    }
}
