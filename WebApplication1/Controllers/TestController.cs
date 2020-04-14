using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using WebApplication1.Models;
using DAL.Interfaces;
using AutoMapper;

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
            TramViewModel tram = _mapper.Map<TramViewModel>(_tramLogic.GetTram(1));
            return View(tram);
        }
    }
}