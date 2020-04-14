﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using DTO;
using AutoMapper;
using DAL.Interfaces;
using TestClasses;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private readonly Tram _tram;
        //private readonly Sector _sector;
        //private readonly ILogger<HomeController> _logger;
        //private readonly IMapper _mapper;
        //private readonly Depot _depot;

        //public HomeController(ILogger<HomeController> logger, IMapper mapper, Tram tram, Sector sector, Depot depot)
        //{
        //    _logger = logger;
        //    _mapper = mapper;
        //    _tram = tram;
        //    _sector = sector;
        //    _depot = depot;
        //}

        public IActionResult Index()
        {
            Depot dp = new Depot();
            ViewBag.ShowTopBar = true;
            return View(dp);
        }

        public IActionResult Privacy()
        {
            ViewBag.ShowTopBar = false;
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.ShowTopBar = false;
            return View();
        }

        [HttpPost]

        public IActionResult InformationTramPopUp(int tramnumber)
        {
            return PartialView("InformationTramPopUp");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
