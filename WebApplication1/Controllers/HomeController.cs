using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using DTO;
using AutoMapper;
using Logic;
using WebApplication1.Repository;
using Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly Depot _depotLogic;
        private readonly Tram _tramLogic;
        private readonly Sector _sectorLogic;
        private readonly LoginRepository _repository;
        private readonly RepairService _repairService;
        private readonly CleaningService _cleaningService;


        public HomeController(ILogger<HomeController> logger, IMapper mapper, Tram tram, Sector sector, Depot depot, LoginRepository repository, RepairService repairService, CleaningService cleaningService)
        {
            _mapper = mapper;
            _depotLogic = depot;
            _tramLogic = tram;
            _sectorLogic = sector;
            _repository = repository;
            _repairService = repairService;
            _cleaningService = cleaningService;
        }

        public IActionResult Index()
        {
            ViewBag.ShowTopBar = true;
            ViewBag.CurrentPage = "Index";
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

        [HttpPost]
        public IActionResult RemoveTram(int tramnumber)
        {
            ViewBag.Tramnumber = tramnumber;
            return PartialView("PartialRemoveTram");
        }
        public IActionResult CleanSignUp(int tramnumber)
        {
            ViewBag.Tramnumber = tramnumber;
            return PartialView("PartialCleanSignUp");
        }

        public IActionResult CleanSignUpSend(CleaningServiceViewModel cleaningService)
        {
            int cleaning = Convert.ToInt32(HttpContext.Request.Form["service"]);
            _cleaningService.HasToBeCleaned(_tramLogic.GetTram(cleaningService.TramNumber), (ServiceType)cleaning);
            ViewBag.LatestMessage = "Sent Tram " + cleaningService.TramNumber + " to the cleaning section successfully.";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RepairSignUp(int tramnumber)
        {
            ViewBag.Tramnumber = tramnumber;
            return PartialView("PartialRepairSignUp");
        }

        public IActionResult RepairSignUpSend()
        {
            int repair = Convert.ToInt32(HttpContext.Request.Form["repairsize"]);
            _depotLogic.TransferTram(HttpContext.Request.Form["tramnumber"], true, false, HttpContext.Request.Form["repairreason"], _depotLogic.Read(1));
            ViewBag.LatestMessage = "Sent Tram to the repairing4 section successfully.";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RemoveTramSend()
        {
            _sectorLogic.RemoveTram(_sectorLogic.GetSector(_sectorLogic.GetSectorByTramNumber(HttpContext.Request.Form["tramnumber"])));
            ViewBag.LatestMessage = "Removed Tram Successfully";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ParkTram()
        {
            bool repair = HttpContext.Request.Form["repair"] == "repair";
            bool cleaning = HttpContext.Request.Form["clean"] == "clean";
            _depotLogic.ReceiveTram(HttpContext.Request.Form["tramnumber"], repair, cleaning, HttpContext.Request.Form["repairreason"], _depotLogic.Read(1));   
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ReserveTrack()
        {
            return Content(HttpContext.Request.Form["tramnumber"] + " " + HttpContext.Request.Form["tracknumber"]);
        }
        public IActionResult MoveTramTo()
        {
            return Content(HttpContext.Request.Form["tramnumber"] + " " + HttpContext.Request.Form["tracknumber"]);
        }
        public IActionResult InformationTramPopUp(string tramnumber, string tracknumber)
        {

            TramViewModel tramData = _mapper.Map<TramViewModel>(_tramLogic.GetTram(tramnumber));
            RepairServiceViewModel repairLogDataBig = _mapper.Map<RepairServiceViewModel>(_repairService.GetOccuredLog(_tramLogic.GetTram(tramnumber), ServiceType.Big));
            RepairServiceViewModel repairLogDataSmall = _mapper.Map<RepairServiceViewModel>(_repairService.GetOccuredLog(_tramLogic.GetTram(tramnumber), ServiceType.Small));

            if (repairLogDataBig == null)
            {
                repairLogDataBig = new RepairServiceViewModel();
                repairLogDataBig.RepairDate = default;
            }
            if (repairLogDataSmall == null)
            {
                repairLogDataSmall = new RepairServiceViewModel();
                repairLogDataSmall.RepairDate = default;
            }
            @ViewBag.Tramnumber = tramData.TramNumber;
            @ViewBag.Status = tramData.Status;
            @ViewBag.Track = tracknumber;
            @ViewBag.CleaningDateBigService = Daysago(tramData.CleaningDateBigService);
            @ViewBag.CleaningDateSmallService = Daysago(tramData.CleaningDateSmallService);
            @ViewBag.RepairDateBigService = Daysago(repairLogDataBig.RepairDate);
            @ViewBag.RepairDateSmallService = Daysago(repairLogDataSmall.RepairDate);
            @ViewBag.Type = tramData.Type;

            return PartialView("InformationTramPopUp", tramData);
        }

        public IActionResult PartialViewMoveTram(int tramnumber, int track)
        {
            ViewBag.Tramnumber = tramnumber;
            ViewBag.Track = track;
            return PartialView("PartialViewMoveTram");
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
            foreach (TrackDTO track in dto.DepotTracks)
            {
                returnDepot.DepotTracks.Add(_mapper.Map<TrackViewModel>(track));
            }
            return returnDepot;
        }
        public IActionResult SendTramToRepair(TramViewModel tram)
        {
            if (!ModelState.IsValid)
            {
                _repairService.DetermineRepairType(_mapper.Map<TramDTO>(tram));
            }
            else
            {
                ViewBag.LatestMessage = "Could not send tram to repair: required field(s) were left empty";
            }
            return RedirectToAction("Index", "Home");
        }

        private int Daysago(DateTime _day)
        {
            TimeSpan daysdifference = DateTime.Today - _day;
            return daysdifference.Days;
        }
    }
}
