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
        IMapper _mapper;

        public TestController(ITramAccess ac, IMapper mapper)
        {
            _mapper = mapper;
            _tramLogic = new Tram(ac);
        }

        public IActionResult Index()
        {
            var tram = _tramLogic.GetTram("2001");
            //StatusDTO stat = new StatusDTO();
            //stat.Status = TramStatus.Cleaning;
            //stat.Description = "Leipe shit";
            //tram.Status.Add(stat);
            //tram.TramNumber = (Convert.ToInt32(tram.TramNumber) - 1).ToString();
            //_tramLogic.Update(tram);
            //var trammmmm = _tramLogic.GetTram(1);
            return View(tram);
        }
    }
}