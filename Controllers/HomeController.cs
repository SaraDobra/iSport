using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iSportProjekti.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace iSportProjekti.Controllers 
{
    
    public class HomeController : Controller
    {   
        private MyContext dbContext;
        public HomeController(MyContext context){
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index(){
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(User user){
            if(ModelState.IsValid){
                if(dbContext.Users.Any(o => o.Email == user.Email)){
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }

                PasswordHasher<User> hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);

                var newUser = dbContext.Users.Add(user).Entity;
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("userId", newUser.UserId);

                return RedirectToAction("Index", "Event");
            }
            return View("Index");
        }

        [HttpPost("login")]
        public IActionResult Login(Login user){
            var test = dbContext.Users.ToList();
            if(ModelState.IsValid){

                User toLogin = dbContext.Users.FirstOrDefault(u => u.Email == user.EmailAttempt);
                if(toLogin == null){
                    ModelState.AddModelError("Email Attempt", "Invalid Email/Password");
                    return View("Index");
                }

                PasswordHasher<Login> hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(user, toLogin.Password, user.PasswordAttempt);
                
                if(result == PasswordVerificationResult.Failed){
                    ModelState.AddModelError("Password Attempt", "Invalid Email/Password");
                    return View("Index");
                }

                HttpContext.Session.SetInt32("userId", toLogin.UserId);
                return RedirectToAction("Index", "Event");
            }   

            return View("Index");
        }

        [HttpGet("logout")]
        public RedirectToActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}