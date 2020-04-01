using System;
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
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
