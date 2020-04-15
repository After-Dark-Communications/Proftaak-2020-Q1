using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using WebApplication1.Models;
using DAL.Interfaces;
using DTO;
using AutoMapper;
using Services;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        Tram _tramLogic;
        Depot _depotLogic;
        IMapper _mapper;
        Sector _sectorLogic;

        public TestController(ITramAccess ac, IMapper mapper, IDepotAccess dc)
        {
            _mapper = mapper;
            _tramLogic = new Tram(ac);
            _depotLogic = new Depot(dc);
           // _sectorLogic = new Sector();
            
        }

        public IActionResult Index()
        {
            var tram = _tramLogic.GetTram("2001");
            return View(tram);
        }
    }
}