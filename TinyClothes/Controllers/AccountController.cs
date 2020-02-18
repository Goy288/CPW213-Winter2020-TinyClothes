using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreContext _context;

        public AccountController(StoreContext context)
        {
            this._context = context;
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
                bool isMatch = 
                    await AccountDB.DoesUserMatch(login, _context);
                // TODO: Create session

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
