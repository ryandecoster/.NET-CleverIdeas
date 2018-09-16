using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleverIdeas.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CleverIdeas.Controllers
{
    
    public class UserController : Controller
    {
        private YourContext _context;
        public UserController(YourContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(ViewModel userReg)
        {
            if(_context.Users.Where(user => user.Email == userReg.RegisterUser.Email).FirstOrDefault() != null)
            {
                ModelState.AddModelError("RegisterUser.Email", "Email already in use");
            }
            PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();
            if(ModelState.IsValid)
            {
                User newUser = new User
                {
                    Name = userReg.RegisterUser.Name,
                    Alias = userReg.RegisterUser.Alias,
                    Email = userReg.RegisterUser.Email,
                    Password = hasher.HashPassword(userReg.RegisterUser, userReg.RegisterUser.Password),
                    Created_At = DateTime.Now,
                    Updated_At = DateTime.Now,          
                };
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("id", newUser.UserId);
                int? user_id = newUser.UserId;
                return RedirectToAction ("Ideas", "Home");
            };
            return View("Index", userReg);
        }
        
        [HttpPost]
        [Route("login")]
         public IActionResult LoginUser(ViewModel userLog)
        {
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            User loginUser = _context.Users.Where(user => user.Email == userLog.LoginUser.LogEmail).SingleOrDefault();
            if(loginUser == null)
            {
                ModelState.AddModelError("LoginUser.LogEmail", "Invalid Email/Password.");
            }
            else if(hasher.VerifyHashedPassword(userLog.LoginUser, loginUser.Password, userLog.LoginUser.LogPassword) == 0)
            {
                ModelState.AddModelError("LoginUser.LogEmail", "Invalid Email/Password.");
            }
            if(!ModelState.IsValid)
            {
                return View("Index", userLog);
            }
            HttpContext.Session.SetInt32("id", loginUser.UserId);
            return RedirectToAction ("Ideas", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["success"] = "You have successfully logged out!";
            return Redirect("/");
        }
    }
}

