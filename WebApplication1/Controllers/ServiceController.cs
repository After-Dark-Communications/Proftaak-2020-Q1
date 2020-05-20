using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logic;
using WebApplication1.Models;
using DTO;

namespace WebApplication1.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly Logic.RepairService _repairservice;
        private readonly Logic.CleaningService _cleaningservice;
        private readonly Tram _tramLogic;
        public ServiceController(IMapper mapper, RepairService repairservice, CleaningService cleaningservice, Tram tram)
        {
            _mapper = mapper;
            _repairservice = repairservice;
            _cleaningservice = cleaningservice;
            _tramLogic = tram;
        }

        public IActionResult Repairs()
        {
            var repairLogs = _repairservice.GetRepairHistory();
            foreach(RepairLogDTO repair in repairLogs)
            {
                _mapper.Map<ServiceViewModel>(repair);
            }
            return View(repairLogs);
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
            var cleaningLogs = _cleaningservice.GetCleaningLogs();
            foreach (CleaningLogDTO clean in cleaningLogs)
            {
                _mapper.Map<ServiceViewModel>(clean);
            }
            return View(cleaningLogs);
        }

        public IActionResult InfoRepairTramPopUp(string tramnumber)
        {

            TramViewModel tramData = _mapper.Map<TramViewModel>(_tramLogic.GetTram(tramnumber));
            @ViewBag.Tramnumber = tramData.TramNumber;
            @ViewBag.Status = tramData.Status;
            @ViewBag.Track = 38; //TODO data uit methode krijgen.
            @ViewBag.CleaningDateBigService = Daysago(tramData.CleaningDateBigService);
            @ViewBag.CleaningDateSmallService = Daysago(tramData.CleaningDateSmallService);
            @ViewBag.RepairDateBigService = Daysago(tramData.RepairDateBigService);
            @ViewBag.RepairDateSmallService = Daysago(tramData.RepairDateSmallService);
            @ViewBag.Type = tramData.Type;

            return PartialView("InfoRepairTramPopUp");

        }
        private int Daysago(DateTime _day)
        {
            TimeSpan daysdifference = DateTime.Today - _day;
            return daysdifference.Days;
        }
    }
}