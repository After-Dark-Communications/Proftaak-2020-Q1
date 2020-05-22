using System;
using System.Collections;
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
        //Depot _depotLogic;
        IMapper _mapper;
        Sector _sectorLogic;
        Track _trackLogic;
        RepairService _repairService;

        public TestController(ITramAccess ac, IMapper mapper, IDepotAccess dc, ISectorAccess sc, ITrackAccess tc, RepairService repairService)
        {
            _mapper = mapper;
            _repairService = repairService;
            _tramLogic = new Tram(ac);
            _sectorLogic = new Sector(sc, ac);
            _trackLogic = new Track(tc, _sectorLogic);
           // _depotLogic = new Depot(_trackLogic, _tramLogic, dc); 
        }

        public IActionResult Index()
        {
            //_repairService.RepairTram(new RepairLogDTO(new RepairServiceDTO(2, 2, "RMS"),new TramDTO("2001"), new UserDTO("admin"), DateTime.Now, 1, false, "" ));
            IEnumerable<RepairLogDTO>  repairLog =  _repairService.GetRepairHistory();
            return View(repairLog);
        }
    }
}