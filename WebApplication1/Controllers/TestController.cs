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
        Track _trackLogic;

        public TestController(ITramAccess ac, IMapper mapper, IDepotAccess dc, ISectorAccess sc, ITrackAccess tc)
        {
            _mapper = mapper;
            _tramLogic = new Tram(ac);
            _depotLogic = new Depot(dc);
            _sectorLogic = new Sector(sc);
            _trackLogic = new Track(tc);
        }

        public IActionResult Index()
        {
            //TrackDTO track = new TrackDTO();
            //track.Id = 3;
            //track.TramType = TramType.TrainingTram;
            //_trackLogic.Update(track);
            DepotDTO depot = new DepotDTO();
            //string newDepotName = "Remise Havenstraat";
            depot = _depotLogic.Read(1);
            //depot.Location = newDepotName;
            //_depotLogic.Update(depot);
            return View(depot);
        }
    }
}