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
using Logic;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly Depot _depotLogic;
        private readonly Tram _tramLogic;
        private readonly Sector _sectorLogic;
        private readonly LoginRepository _repository;


        public HomeController(ILogger<HomeController> logger, IMapper mapper, Tram tram, Sector sector, Depot depot, LoginRepository repository)
        {
            _mapper = mapper;
            _depotLogic = depot;
            _tramLogic = tram;
            _sectorLogic = sector;
            _repository = repository;
        }

        

        public IActionResult Index()
        {
            ViewBag.ShowTopBar = true;
            var depot = MapDepotDTOToViewModel(_depotLogic.Read(1));

            if (_repository.GetLoginSession() == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(depot);
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

        private DepotDTO MapDepotViewModelToDTO(DepotViewModel depotViewModel)
        {
            DepotDTO returnDepot = new DepotDTO()
            {
                Id = depotViewModel.Id,
                Location = depotViewModel.Location,
                DepotTracks = new List<TrackDTO>()
            };
            foreach (TrackViewModel track in depotViewModel.DepotTracks)
            {
                returnDepot.DepotTracks.Add(_mapper.Map<TrackDTO>(track));
            }
            return returnDepot;
        }

        private DepotViewModel MapDepotDTOToViewModel(DepotDTO dto)
        {
            DepotViewModel returnDepot = new DepotViewModel()
            {
                Id = dto.Id,
                Location = dto.Location,
                DepotTracks = new List<TrackViewModel>()
            };
            foreach(TrackDTO track in dto.DepotTracks)
            {
                returnDepot.DepotTracks.Add(_mapper.Map<TrackViewModel>(track));
            }
            return returnDepot;
        }

    }
}
