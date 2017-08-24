using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using beltretake.Models;
using Microsoft.EntityFrameworkCore;

namespace beltretake.Controllers
{
    public class IdeasController : Controller
    {
         private BeltContext _context;

        public IdeasController(BeltContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        { 
            
            int? CurrentUserId = HttpContext.Session.GetInt32("LoggedInUser");
            if (CurrentUserId == null)
            {
                return RedirectToAction("Login", "User");
            }
            User CurrentUser = _context.User.SingleOrDefault(user => user.UserId == (int) CurrentUserId);
            ViewBag.CurrentUser = CurrentUser;

            var AllQuery = _context.Idea.Include(idea => idea.User).Include(user => user.Likes);
            List<Idea> OrderedQuery = AllQuery.OrderByDescending(idea => idea.Likes.Count).ToList();
            ViewBag.AllQuery = OrderedQuery;
            ViewBag.CurrentUserId = CurrentUserId;
            return View("Home");
        }

        [HttpPost]
        [Route("createidea")]
        public IActionResult CreateIdea(string newidea, int userId)
        {
            if (newidea == null)
            {
                ViewBag.NewIdeaError = new List<string>(){
                    "Ideas cannot be blank, please enter a valid text"
                };
                int? CurrentUserId = HttpContext.Session.GetInt32("LoggedInUser");
                User CurrentUser = _context.User.SingleOrDefault(user => user.UserId == (int) CurrentUserId);
                ViewBag.CurrentUser = CurrentUser;
                return View("Home");
            }

            Idea newIdea = new Idea(){
                Content = newidea,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Idea.Add(newIdea);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost]
        [Route("likeidea")]
        public IActionResult LikeIdea(int ideaId, int loggedInUserId)
        {
            
            Idea RetrievedIdea = _context.Idea.Include(likes => likes.Likes).ThenInclude(user => user.User).SingleOrDefault(idea => idea.IdeaId == ideaId);
            var ThisUser = _context.Like.Include(idea => idea.Idea).Where(user => user.UserId == loggedInUserId);

           
                 Like newLike = new Like(){
                        IdeaId = ideaId,
                        UserId = loggedInUserId,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    RetrievedIdea.Likes.Add(newLike);
                    _context.Like.Add(newLike);
                    _context.SaveChanges();  
            return RedirectToAction("Home");
        }
                

        [HttpGet]
        [Route("ideas/{ideaId}")]
        public IActionResult ShowOneIdea(int ideaId)
        {
           var IdeaQuery = _context.Like.Where(like => like.IdeaId == ideaId).Include(user => user.User);
           var ThisIdea = _context.Idea.Include(user => user.User).SingleOrDefault(idea => idea.IdeaId == ideaId);
           ViewBag.OneIdea = IdeaQuery;
           ViewBag.ThisIdea = ThisIdea;
            return View("Showone");
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult ShowOneUser(int userId)
        {
            var OneUser = _context.User.SingleOrDefault(user => user.UserId == userId);
            ViewBag.OneUser = OneUser;
            List<Idea> UserIdeas = _context.Idea.Where(idea => idea.UserId == userId).ToList();
            ViewBag.UserIdeas = UserIdeas;
            List<Like> Userlikes = _context.Like.Where(like => like.UserId == userId).ToList();
            ViewBag.Userlikes = Userlikes;
            return View("Userprofile");
        }

        [HttpGet]
        [Route("idea/delete/{deleteId}")]
        public IActionResult DeleteIdea(int deleteId)
        {
            System.Console.WriteLine($"Line 112*************************This is the idea id {deleteId}");
            var DeleteOne = _context.Idea.SingleOrDefault(idea => idea.IdeaId == deleteId);
            _context.Idea.Remove(DeleteOne);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

    }
}