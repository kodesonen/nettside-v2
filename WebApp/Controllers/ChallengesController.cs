﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseHandler.Models;
using DatabaseHandler.Handlers;

namespace WebApp.Controllers
{
    public class ChallengesController : Controller
    {
        [HttpGet]
        [Route("Utfordringer")]
        public IActionResult Index()
        {
            //List<Challenge> ChallengesList = HandleChallenges.GetAllChallenges();
            //return View(ChallengesList);
            return View();
        }
    }
}
