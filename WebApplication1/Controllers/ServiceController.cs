using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logic;
using WebApplication1.Models;
using DTO;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly RepairService _repairservice;
        private readonly CleaningService _cleaningservice;
        private readonly Track _tracklogic;
        private readonly Tram _tramLogic;
        private readonly LoginRepository _login;
        public ServiceController(IMapper mapper, RepairService repairservice, CleaningService cleaningservice, Tram tram, Track tracklogic, LoginRepository login)
        {
            _mapper = mapper;
            _repairservice = repairservice;
            _cleaningservice = cleaningservice;
            _tramLogic = tram;
            _tracklogic = tracklogic;
            _login = login;
        }

        public IActionResult Repairs()
        {
            List<RepairServiceViewModel> rvms = new List<RepairServiceViewModel>();
            var repairLogs = _repairservice.GetRepairHistory().Where(x => x.Occured == false);
            foreach(RepairLogDTO log in repairLogs)
            {
                RepairServiceViewModel rvm = new RepairServiceViewModel();
                var track = _tracklogic.GetTrackByTramNumber(log.Tram.TramNumber);
                rvm.TrackNumber = track.TrackNumber;
                rvm.TramNumber = log.Tram.TramNumber;
                rvm.RepairType = log.ServiceType;
                rvm.RepairMessage = log.RepairMessage;
                rvms.Add(rvm);
            }   
            return View(rvms);
        }

        public IActionResult CleaningDone(int tramnumber)
        {
            ViewBag.Tramnumber = tramnumber;
            return PartialView("PartialCleaningDone");
        }
        public IActionResult RepairDone(int tramnumber)
        {
            ViewBag.Tramnumber = tramnumber;
            return PartialView("PartialRepairDone");
        }
        public IActionResult UpdateCleaningDoneStatus()
        {
            return Content(HttpContext.Request.Form["tramnumber"]);
        }
        public IActionResult UpdateRepairDoneStatus()
        {
            UserDTO user = new UserDTO(_login.GetLoginSession());
            _repairservice.ServiceRepair(_tramLogic.GetTram(_tramLogic.GetTramIdFromNumber(HttpContext.Request.Form["tramnumber"])),user); //Rick's schuld :-(
            return RedirectToAction("Repairs", "Service");
        }
        public IActionResult Cleaning()
        {
            var cleaningLogs = _cleaningservice.GetCleaningLogs();
            foreach (CleaningLogDTO clean in cleaningLogs)
            {
                
            }
            return View(cleaningLogs);
        }

        public IActionResult PartialRepairPopUp(string tramnumber, string tracknumber)
        {

            TramViewModel tramData = _mapper.Map<TramViewModel>(_tramLogic.GetTram(tramnumber));
            @ViewBag.Tramnumber = tramData.TramNumber;
            @ViewBag.Status = tramData.Status;
            @ViewBag.Track = tracknumber;
            @ViewBag.CleaningDateBigService = Daysago(tramData.CleaningDateBigService);
            @ViewBag.CleaningDateSmallService = Daysago(tramData.CleaningDateSmallService);
            @ViewBag.RepairDateBigService = Daysago(tramData.RepairDateBigService);
            @ViewBag.RepairDateSmallService = Daysago(tramData.RepairDateSmallService);
            @ViewBag.Type = tramData.Type;

            return PartialView("PartialRepairPopUp");
        }

        public IActionResult PartialDefectLog(string tramnummer)
        {
            return PartialView("PartialDefectLog");
        }

        private int Daysago(DateTime _day)
        {
            TimeSpan daysdifference = DateTime.Today - _day;
            return daysdifference.Days;
        }
    }
}