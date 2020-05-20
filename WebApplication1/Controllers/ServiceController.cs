using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebApplication1.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IMapper _mapper;
        public ServiceController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Repairs()
        {
            return View();
        }

        public IActionResult CleaningDone(int tramnumber)
        {
            ViewBag.Tramnumber = tramnumber;
            return PartialView("PartialCleaningDone");
        }
        public IActionResult UpdateCleaingDoneStatus()
        {
            return Content(HttpContext.Request.Form["tramnumber"]);
        }
        public IActionResult Cleaning()
        {
            return View();
        }
    }
}