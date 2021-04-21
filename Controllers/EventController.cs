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

    [Route("events")]
    public class EventController : Controller
    {   
        private User loggedInUser{
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));}
        }
        private MyContext dbContext;
        public EventController(MyContext context){
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index(){
            if(loggedInUser == null){
                return RedirectToAction("Index", "Home");
            }

            var activity = dbContext.Events
                    .Include(a => a.UserEvents)
                    .OrderBy(a => a.Date);


            ViewBag.UserId = loggedInUser.UserId;


            var responsed = activity.Where(a => a.UserEvents.Any(r => r.UserId == 1));

            return View(activity.ToList());
        }

        [HttpGet("{eventId}")]
        public IActionResult Show(int eventId){
            if(loggedInUser == null){
                return RedirectToAction("Index", "Home");
            }

            Event dojoModel = dbContext.Events
                .Include(a => a.Planner)
                .Include(a => a.UserEvents)
                    .ThenInclude(t => t.Participants)
                .FirstOrDefault(a => a.EventId == eventId);

            return View(dojoModel); 
        }
        
        [HttpGet("users")]
        public IActionResult Users(int eventId){
            if(loggedInUser == null){
                return RedirectToAction("Index", "Home");
            }

            Event dojoModel = dbContext.Events
                .Include(a => a.Planner)
                .Include(a => a.UserEvents)
                    .ThenInclude(t => t.Participants)
                .FirstOrDefault(a => a.EventId == eventId);

            return View(dojoModel); 
        }

        [HttpGet("new")]
        public IActionResult New(){
            if(loggedInUser == null){
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserId = loggedInUser.UserId;
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Event newEvent){
            if(ModelState.IsValid){
                newEvent.UserId = loggedInUser.UserId;
                dbContext.Events.Add(newEvent);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = loggedInUser.UserId;
            return View("New");
        }


        [HttpGet("search")]
        public IActionResult Search(string s){
            if(loggedInUser == null){
                RedirectToAction("Index", "Home");
            }

            ViewData["GetEvents"] = s;

            var search = from x in dbContext.Events select x;
            if(!String.IsNullOrEmpty(s)){
                search = search.Where(x => x.Events.Contains(s) || x.Location.Contains(s));
            }

            return View(search.AsNoTracking().ToList());
            
        }


        [HttpPost("Remove/{eventId}")]
        public IActionResult Remove(int eventId){
            if(loggedInUser == null){
                return RedirectToAction("Index", "Home");
            }

            Event toDelete = dbContext.Events.FirstOrDefault(a => a.EventId == eventId 
                                && a.UserId == loggedInUser.UserId);

            if(toDelete == null){
                return RedirectToAction("Index");
            }

            dbContext.Events.Remove(toDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");

        }


        [HttpPost("{eventId}/Join")]
        public IActionResult Add(int eventId){
            if(loggedInUser == null){
                return RedirectToAction("Index", "Home");
            }

            Event toAdd = dbContext.Events.FirstOrDefault(a => a.EventId == eventId 
                                && a.UserId == loggedInUser.UserId);

            if(toAdd == null){
                return RedirectToAction("Index");
            }

            dbContext.Events.Add(toAdd);
            dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}