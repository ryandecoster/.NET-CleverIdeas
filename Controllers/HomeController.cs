using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleverIdeas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CleverIdeas.Controllers
{
    public class HomeController : Controller
    {
        private YourContext _context;
        public HomeController(YourContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("clever_ideas")]
        public IActionResult Ideas()
        {
            if(HttpContext.Session.GetInt32("id") == null) {
                return RedirectToAction("Index", "User");
            }
            ViewModel view = new ViewModel()
            {
                Users = new User(),
                Ideas = new Idea(),
                Likes = new Like(),
                allIdeas = _context.Ideas
                            .Include(user => user.Creator)
                            .Include(idea => idea.LikesList)
                                .ThenInclude(like => like.LikedUser)
                            .OrderByDescending(like => like.LikesList.Count)
                            .ToList(),
                allLikes = _context.Likes.ToList()
            };
            ViewBag.currentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("id"));
            return View(view);
        }

        [HttpPost]
        [Route("PostIdea")]
        public IActionResult PostIdea(ViewModel FormData)
        {
            if(HttpContext.Session.GetInt32("id") == null) {
                return RedirectToAction("Index", "User");
            }
            User _currentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("id"));
            if (ModelState.IsValid) {
                Idea newIdea = new Idea()
                {
                    Content = FormData.Ideas.Content,
                    UserId = (int) HttpContext.Session.GetInt32("id"),
                    Creator = _currentUser
                };
                _context.Add(newIdea);
                _context.SaveChanges();
                return RedirectToAction("Ideas");
            }
            ViewBag.currentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("id"));
            return View("Ideas", FormData);
        }

        [HttpGet]
        [Route("Delete/{IdeaId}")]
        public IActionResult Delete(int IdeaId)
        {
            if(HttpContext.Session.GetInt32("id") == null) {
                return RedirectToAction("Index", "User");
            }
            Idea thisIdea = _context.Ideas
                            .Where(idea => idea.IdeaId == IdeaId).SingleOrDefault();
            _context.Ideas.Remove(thisIdea);
            _context.SaveChanges();
            return RedirectToAction("Ideas");
        }

        [HttpGet]
        [Route("Like/{IdeaId}")]
        public IActionResult Like(int IdeaId)
        {
            if(HttpContext.Session.GetInt32("id") == null) {
                return RedirectToAction("Index", "User");
            }
            User _currentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("id"));
            Idea thisIdea = _context.Ideas
                            .Where(idea => idea.IdeaId == IdeaId).SingleOrDefault();
            Like newLike = new Like
            {
                UserId = (int) HttpContext.Session.GetInt32("id"),
                LikedUser = _currentUser,
                IdeaId = thisIdea.IdeaId,
            };
            thisIdea.LikesList.Add(newLike);
            _context.SaveChanges();
            return RedirectToAction("Ideas");
        }

        [HttpGet]
        [Route("users/{UserId}")]
        public IActionResult UserProfile(int UserId)
        {
            if(HttpContext.Session.GetInt32("id") == null) {
                return RedirectToAction("Index", "User");
            }
            ViewModel view = new ViewModel()
            {
                Users = _context.Users
                            .Include(idea => idea.Ideas)
                            .Include(like => like.Likes)
                            .SingleOrDefault(user => user.UserId == UserId),
            };
            return View(view);
        }

        [HttpGet]
        [Route("clever_ideas/{IdeaId}")]
        public IActionResult ShowCleverIdea(int IdeaId)
        {
            if(HttpContext.Session.GetInt32("id") == null) {
                return RedirectToAction("Index", "User");
            }
            ViewModel view = new ViewModel()
            {
                Ideas = _context.Ideas
                            .Include(user => user.Creator)
                            .Include(idea => idea.LikesList)
                                .ThenInclude(like => like.LikedUser)
                            .SingleOrDefault(idea => idea.IdeaId == IdeaId)
            };
            return View(view);
        }
    }
}
