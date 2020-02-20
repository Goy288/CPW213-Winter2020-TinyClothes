using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TinyClothes.Data;
using TinyClothes.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreContext _context;
        private readonly IHttpContextAccessor _accessor;

        public AccountController
            (StoreContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(RegistryViewModel reg)
        {
            if (ModelState.IsValid)
            {
                // Check username is not taken
                if (!await AccountDB.IsUsernameTaken(reg.Username, _context))
                {
                    Account acc = new Account()
                    {
                        Email = reg.Email,
                        FirstName = reg.FirstName,
                        LastName = reg.LastName,
                        Password = reg.Password,
                        Username = reg.Username
                    };

                    // Add Account to DB
                    await AccountDB.Register(_context, acc);

                    SessionHelper.CreateUserSession(acc.AccountID, acc.Username, _accessor);

                    // Create user session
                    HttpContext.Session.SetInt32("ID", acc.AccountID);
                    //Console.WriteLine(HttpContext.Session.GetInt32("ID"));
                    HttpContext.Session.SetString("Username", acc.Username);
                    //string username = HttpContext.Session.GetString("Username");
                    //Console.WriteLine(username);

                    return RedirectToAction("Index", "Home");
                }
                else // If username is taken, add error
                {
                    // Display error
                    ModelState.AddModelError(nameof(Account.Username), "Username is already taken!");
                }
            }

            return View(reg);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                Account acc = 
                    await AccountDB.DoesUserMatch(login, _context);
                
                if (acc != null)
                {
                    SessionHelper.CreateUserSession
                        (acc.AccountID, acc.Username, _accessor);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Credentials");
            }
            return View();
        }

        public IActionResult Logout()
        {
            SessionHelper.DeleteUserSession(_accessor);
            return RedirectToAction("Index", "Home");
        }
    }
}
