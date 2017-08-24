using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using beltretake.Models;
using System.Linq;

namespace beltretake.Controllers
{
    public class UserController : Controller
    {
        private BeltContext _context;

        public UserController(BeltContext context)
        {
            _context = context;
        }
        // GET: /Home/
        
        [HttpGet]
        [Route("")]
         public IActionResult Index()
        { 
            ViewBag.Errors = new List<string>();
            ViewBag.LoginErrors = new List<string>();
            return View("Index");
        }
    

            [HttpPost]
            [Route("register")]
            public IActionResult Register(RegisterUser model)
            {
                TryValidateModel(model);
                if(ModelState.IsValid)
                {
                    User newUser = new User()
                    {
                        Name = model.Name,
                        Alias = model.Alias,
                        Email = model.Email,
                        Password = model.Password,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    _context.User.Add(newUser);
                    _context.SaveChanges();

                    var ReturnedUser = _context.User.SingleOrDefault(user => user.Email == newUser.Email);
                    System.Console.WriteLine($"Line 80*************************this is the newly registered user {ReturnedUser}");

                    HttpContext.Session.SetInt32("LoggedInUser", (int)ReturnedUser.UserId);
                    return RedirectToAction("Home", "Ideas");
                }
                // }
                ViewBag.Errors = ModelState.Values;
                return View("Index");
            }

            [HttpPost]
            [Route("login")]
            public IActionResult Login(string email, string password)
            {
                if(email != null && password != null)
                {
                    User ReturnedUser = _context.User.SingleOrDefault(user => user.Email == email); 
            
                        if(ReturnedUser != null)
                            {
                                if(ReturnedUser.Password == password)
                                {
                                    HttpContext.Session.SetInt32("LoggedInUser", ReturnedUser.UserId);
                                    return RedirectToAction("Home", "Ideas");   
                                }
                            }
                }

                ViewBag.LoginErrors = new List<string>(){"Username or password is incorrect"};
                ViewBag.Errors = new List<string>();
                return View("Index");
            }

            [HttpGet]
            [Route("logout")]
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                // System.Console.WriteLine("******************Line 87 user controller - logout route hit");
                return RedirectToAction("Index");
            }
    }
}
