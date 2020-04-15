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
        Sector _sectorLogic;
        IMapper _mapper;

        public TestController(ITramAccess ac, IMapper mapper, ISectorAccess sectorAccess)
        {
            _mapper = mapper;
            _tramLogic = new Tram(ac);
            _sectorLogic = new Sector(sectorAccess);
        }

        public IActionResult Index()
        {
            var tram = _tramLogic.GetTram("2001");
            return View(tram);
        }
    }
}