using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Champ.App.Models;
using Champ.Models;
using Microsoft.AspNet.Identity;
using AutoMapper.QueryableExtensions;

namespace Champ.App.Controllers
{
    public class ContestController : BaseController
    {
        // GET: Contest
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddContest()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContest(ContestViewModel contest)
        {
            var loggedUserId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == loggedUserId);
            if (user != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(contest);
                }

                if (contest == null)
                {
                    return View(contest);
                }

                var newContest = new Contest()
                {
                    Title = contest.Title,
                    Description = contest.Description,
                    CreatorId = loggedUserId,
                    CreatenOn = DateTime.Now,
                    ClosesOn = contest.ClosesOn,
                    NumberOfAllowedParticipants = contest.NumberOfAllowedParticipants,
                    ParticipationStrategy = contest.ParticipationStrategy
                    
                };

                user.CreatedContests.Add(newContest);
                this.Data.SaveChanges();
            }

            TempData["contest"] = contest.Title;
            return View();
        }


        public ActionResult GetContest(int id)
        {
            var loggedUserId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == loggedUserId);
            var searchedContest = this.Data.Contests.All()
                .Where(c => c.Id == id)
                .ProjectTo<ContestViewModel>()
                .FirstOrDefault();

            if (searchedContest == null)
            {
                return View("Error");
            }

            if (user != null)
            {
                return View("GetContestToLoggedUser", searchedContest);
            }

            return View("GetContestToAnonymousUser", searchedContest);
        }

        public ActionResult ViewContests()
        {
            
            var loggedUserId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == loggedUserId);
            

            if (user == null)
            {
                var contests = this.Data.Contests.All()
                 .OrderByDescending(c => c.CreatenOn)
                 .Take(10)
                 .Select(c => new ContestViewModel()
                 {
                     Id = c.Id,
                     Title = c.Title,
                     Description = c.Description,
                    
                 }).ToList();
                
                return View("ViewAnonymousUser", contests);
            }
            else
            {
                var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.CreatenOn)
                .Take(10)
                .Select(c => new ContestViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CountOfParticipants = c.Participants.Count,
                    ClosesOn = c.ClosesOn,
                    NumberOfAllowedParticipants = c.NumberOfAllowedParticipants,
                    ParticipationStrategy = c.ParticipationStrategy,
                    HasParticipated = c.Participants.Any(p => p.Id == loggedUserId)
                }).ToList();
                return View("ViewLoggedUser", contests);
            }
        }

    }
}